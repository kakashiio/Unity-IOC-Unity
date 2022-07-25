using System.Collections.Generic;
using System.Reflection;
using IO.Unity3D.Source.IOC;
using IO.Unity3D.Source.Reflection;
using UnityEngine;

namespace IO.Unity3D.Source.IOCUnity
{
    //******************************************
    // Help to create IOCContainer for unity.
    // The container will auto manager all the
    // implementation of unity's life cycle.
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:09
    //******************************************
    public class UnityIOCContainer
    {
        public IIOCContainer IOCContainer { get; private set; }
        
        public UnityIOCContainerMono UnityIOCContainerMono { get; private set; }
        public GameObject UnityIOCContainerObject { get; private set; }
        
        internal IReadOnlyList<IUnityFixedUpdate> UnityFixedUpdates { get; }
        internal IReadOnlyList<IUnityUpdate> UnityUpdates { get; }
        internal IReadOnlyList<IUnityLateUpdate> UnityLateUpdates { get; }
        internal IReadOnlyList<IUnityApplication> UnityApplications { get;  }
        internal IReadOnlyList<IUnityEditor> UnityEditors { get; }
        internal IReadOnlyList<IUnityGUI> UnityGUIs { get; }

        public UnityIOCContainer(ITypeContainer typeContainer, IOCContainerConfiguration configuration = null)
        {
            IOCContainer = new IOCContainerBuilder(typeContainer)
                .SetConfiguration(configuration)
                .Build();
            
            UnityUpdates = _FindAndSort<IUnityUpdate>(IOCContainer);
            UnityLateUpdates = _FindAndSort<IUnityLateUpdate>(IOCContainer);
            UnityFixedUpdates = _FindAndSort<IUnityFixedUpdate>(IOCContainer);
            UnityApplications = _FindAndSort<IUnityApplication>(IOCContainer);
            UnityEditors = _FindAndSort<IUnityEditor>(IOCContainer);
            UnityGUIs = _FindAndSort<IUnityGUI>(IOCContainer);
            
            var gameObject = new GameObject("UnityIOCContainer");
            gameObject.SetActive(false);
            UnityIOCContainerMono unityIOCContainerMono = gameObject.AddComponent<UnityIOCContainerMono>();
            unityIOCContainerMono.Init(this);
            gameObject.SetActive(true);
            
            UnityIOCContainerObject = gameObject;
            UnityIOCContainerMono = unityIOCContainerMono;
        }

        public void Destroy()
        {
            GameObject.Destroy(UnityIOCContainerObject);
            UnityIOCContainerObject = null;
            UnityIOCContainerMono = null;
        }

        public void Pause(PauseOrResume pauseOrResume = PauseOrResume.All)
        {
            if (UnityIOCContainerMono == null)
            {
                return;
            }
            UnityIOCContainerMono.Pause(pauseOrResume);
        }

        public void Resume(PauseOrResume pauseOrResume = PauseOrResume.All)
        {
            if (UnityIOCContainerMono == null)
            {
                return;
            }
            UnityIOCContainerMono.Resume(pauseOrResume);
        }

        private IReadOnlyList<T> _FindAndSort<T>(IIOCContainer iocContainer) where T : class
        {
            var list = new List<T>(iocContainer.FindObjectsOfType<T>());
            list.Sort((t1, t2) =>
            {
                var order1 = t1.GetType().GetCustomAttribute(typeof(DefaultExecutionOrder)) as DefaultExecutionOrder;
                var order2 = t2.GetType().GetCustomAttribute(typeof(DefaultExecutionOrder)) as DefaultExecutionOrder;
                var index1 = order1 == null ? 0 : order1.order;
                var index2 = order2 == null ? 0 : order2.order;
                return index1.CompareTo(index2);
            });
            return list;
        }
    }
}