using IO.Unity3D.Source.IOC;
using UnityEngine;

namespace IO.Unity3D.Source.IOCUnity.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:50
    //******************************************
    [IOCComponent]
    [DefaultExecutionOrder(-2)]
    public class TestUpdate2 : IUnityUpdate, IUnityLateUpdate
    {
        public void Update()
        {
            Debug.LogError(nameof(TestUpdate2) + " Update");
        }

        public void LateUpdate()
        {
            Debug.LogError(nameof(TestUpdate2) + " LateUpdate");
        }
    }
}