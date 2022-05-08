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
    public class TestUpdate : IUnityUpdate, IUnityLateUpdate
    {
        public void Update()
        {
            Debug.LogError(nameof(TestUpdate) + " Update");
        }

        public void LateUpdate()
        {
            Debug.LogError(nameof(TestUpdate) + " LateUpdate");
        }
    }
}