# IOC-Unity Library in Source Framework for Unity3D

Help to use IOCContainer more convenience.

# Tutorial

[中文向导](https://unity3d.io/2022/05/09/Unity-IOC-Unity/)

## Add dependencies

You can add package from git url through the Package Manager.

All the following package should be added.

|Package|Description|
|--|--|
|[https://github.com/kakashiio/Unity-Reflection.git#1.0.0](https://github.com/kakashiio/Unity-Reflection.git#1.0.0)|Reflection Library|
|[https://github.com/kakashiio/Unity-IOC.git#1.0.0](https://github.com/kakashiio/Unity-IOC.git#1.0.0)|IOC Library|
|[https://github.com/kakashiio/Unity-IOC-Unity.git#1.0.0](https://github.com/kakashiio/Unity-IOC-Unity.git#1.0.0)|IOC-Unity Library|

## Import Samples From Package Manager and run `Basic.unity` scene

The basic usage of `UnityIOCContainer` is in the sample script `BasicDemo.cs`

## Create a `UnityIOCContainer` 

```csharp
ITypeContainer typeContainer = new TypeContainerCollection(new List<ITypeContainer>
    {
        new TypeContainer(Assembly.GetExecutingAssembly()),
        new TypeContainer(typeof(IOCComponent).Assembly)
    });
    new UnityIOCContainer(typeContainer);
```

Add the code above at the first your application is startup.

## Implement Unity Life Cycle interface as you need

* IUnityUpdate
* IUnityLateUpdate
* IUnityFixedUpdate
* IUnityGUI
* IUnityApplication
* IUnityEditor

Assume that you want to let something invoke per frame. The traditional way to achieve this requirement is define a MonoBehaviour, write the `per frame code` in that MonoBehaviour's `Update` method. With the `IOC` library and this `IOC-Unity` library. You can simply do it like the following code:

```csharp
[IOCComponent]
public class NormalClassButNotMonoBehaviour : IUnityUpdate
{
    public void Update()
    {
        // Your per frame code here
    }
}
```

> This way will be more high performance than the traditional way.

## Other Components

### CoroutineManager

Help to execute coroutine more convenience. If you want to execute some coroutine. You could simple write the following code:


```csharp
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
```

# Future

Will increase more Unity API and Component for game development requirement. Looking forward to your suggestion if you have some requirements.