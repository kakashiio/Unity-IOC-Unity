using System;
using System.Collections.Generic;
using UnityEngine;

namespace IO.Unity3D.Source.IOCUnity
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:12
    //******************************************
    public class UnityIOCContainerMono : MonoBehaviour
    {

        private UnityIOCContainer _UnityIOCContainer;
        
        private IReadOnlyList<IUnityUpdate> _UnityUpdates= new List<IUnityUpdate>(0);
        private IReadOnlyList<IUnityLateUpdate> _UnityLateUpdates = new List<IUnityLateUpdate>(0);
        private IReadOnlyList<IUnityFixedUpdate> _UnityFixedUpdates= new List<IUnityFixedUpdate>(0);
        private IReadOnlyList<IUnityApplication> _UnityApplications= new List<IUnityApplication>(0);
        private IReadOnlyList<IUnityEditor> _UnityEditors= new List<IUnityEditor>(0);
        private IReadOnlyList<IUnityGUI> _UnityGUIs= new List<IUnityGUI>(0);

        private PauseOrResume _Pause = PauseOrResume.None;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void Init(UnityIOCContainer unityIOCContainer)
        {
            _UnityIOCContainer = unityIOCContainer;
            
            _UnityLateUpdates = unityIOCContainer.UnityLateUpdates;
            _UnityFixedUpdates = unityIOCContainer.UnityFixedUpdates;
            _UnityUpdates = unityIOCContainer.UnityUpdates;
            _UnityApplications = unityIOCContainer.UnityApplications;
            _UnityEditors = unityIOCContainer.UnityEditors;
            _UnityGUIs = unityIOCContainer.UnityGUIs;
        }

        private void Update()
        {
            if ((_Pause & PauseOrResume.Update) != 0)
            {
                return;
            }

            for (int i = 0; i < _UnityUpdates.Count; i++)
            {
                _UnityUpdates[i].Update();
            }
        }

        private void LateUpdate()
        {
            if ((_Pause & PauseOrResume.LateUpdate) != 0)
            {
                return;
            }
            for (int i = 0; i < _UnityLateUpdates.Count; i++)
            {
                _UnityLateUpdates[i].LateUpdate();
            }
        }
        
        private void FixedUpdate()
        {
            if ((_Pause & PauseOrResume.FixedUpdate) != 0)
            {
                return;
            }
            for (int i = 0; i < _UnityFixedUpdates.Count; i++)
            {
                _UnityFixedUpdates[i].FixedUpdate();
            }
        }

        private void OnGUI()
        {
            for (int i = 0; i < _UnityGUIs.Count; i++)
            {
                _UnityGUIs[i].OnGUI();
            }
        }

        private void OnApplicationQuit()
        {
            for (int i = 0; i < _UnityApplications.Count; i++)
            {
                _UnityApplications[i].OnApplicationQuit();
            }
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            for (int i = 0; i < _UnityApplications.Count; i++)
            {
                _UnityApplications[i].OnApplicationPause(pauseStatus);
            }
        }

        private void OnValidate()
        {
            for (int i = 0; i < _UnityEditors.Count; i++)
            {
                _UnityEditors[i].OnValidate();
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < _UnityEditors.Count; i++)
            {
                _UnityEditors[i].OnDrawGizmos();
            }
        }

        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < _UnityEditors.Count; i++)
            {
                _UnityEditors[i].OnDrawGizmosSelected();
            }
        }

        private void OnDestroy()
        {
            _UnityIOCContainer.IOCContainer.Destroy();
        }
        
        public void Pause(PauseOrResume pauseOrResume = PauseOrResume.All)
        {
            _Pause |= pauseOrResume;
        }

        public void Resume(PauseOrResume pauseOrResume = PauseOrResume.All)
        {
            _Pause = (_Pause & (~pauseOrResume));
        }
    }
    
    [Flags]
    public enum PauseOrResume
    {
        None        = 0,
        Update      = 1 << 0,
        LateUpdate  = 1 << 1,
        FixedUpdate = 1 << 2,
        All         = Update | LateUpdate | FixedUpdate,
    }
}