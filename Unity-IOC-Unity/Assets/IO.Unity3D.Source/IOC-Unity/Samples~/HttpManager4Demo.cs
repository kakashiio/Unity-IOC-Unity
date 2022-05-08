using System;
using System.Collections;
using IO.Unity3D.Source.IOC;
using UnityEngine;
using UnityEngine.Networking;

namespace IO.Unity3D.Source.IOCUnity.Samples
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 03:04
    //******************************************
    [IOCComponent]
    public class HttpManager4Demo : IUnityGUI
    {
        [Autowired]
        private CoroutineManager _CoroutineManager;

        public void Get(string url, Action<string> onGet)
        {
            _CoroutineManager.StartCoroutine(_StartGet(url, onGet));
        }

        private IEnumerator _StartGet(string url, Action<string> onGet, Action<string> onError = null)
        {
            UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result == UnityWebRequest.Result.Success)
            {
                onGet?.Invoke(unityWebRequest.downloadHandler.text);    
            }
            else
            {
                onError?.Invoke(unityWebRequest.error);                
            }
        }

        public void OnGUI()
        {
            if (GUILayout.Button("Get"))
            {
                Get("https://unity3d.io", (msg) => Debug.Log(msg));
            }
        }
    }
}