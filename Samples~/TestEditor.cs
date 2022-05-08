using IO.Unity3D.Source.IOC;
using UnityEngine;

namespace IO.Unity3D.Source.IOCUnity.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:53
    //******************************************
    [IOCComponent]
    public class TestEditor : IUnityEditor
    {
        public void OnDrawGizmos()
        {
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);
        }

        public void OnValidate()
        {
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);
        }
    }
}