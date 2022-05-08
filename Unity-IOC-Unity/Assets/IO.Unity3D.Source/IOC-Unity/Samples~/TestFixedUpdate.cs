using IO.Unity3D.Source.IOC;
using UnityEngine;

namespace IO.Unity3D.Source.IOCUnity.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:52
    //******************************************
    [IOCComponent]
    public class TestFixedUpdate : IUnityFixedUpdate
    {
        public void FixedUpdate()
        {
            Debug.LogError("FixedUpdate");
        }
    }
}