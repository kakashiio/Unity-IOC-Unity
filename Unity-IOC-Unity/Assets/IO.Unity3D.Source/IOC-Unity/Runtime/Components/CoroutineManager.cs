using System.Collections;
using IO.Unity3D.Source.IOC;
using UnityEngine;

namespace IO.Unity3D.Source.IOCUnity
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:47
    //******************************************
    [IOCComponent]
    public class CoroutineManager
    {
        private CoroutineRunner _CoroutineRunner;
        
        public CoroutineManager()
        {
            var go = new GameObject("CoroutineRunner");
            _CoroutineRunner = go.AddComponent<CoroutineRunner>();
            GameObject.DontDestroyOnLoad(go);
        }

        public Coroutine StartCoroutine(IEnumerator enumerator)
        {
            return _CoroutineRunner.StartCoroutine(enumerator);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            _CoroutineRunner.StopCoroutine(coroutine);
        }

        public void StopAllCoroutines()
        {
            _CoroutineRunner.StopAllCoroutines();
        }
    }

    internal class CoroutineRunner : MonoBehaviour
    {
    }
}