namespace IO.Unity3D.Source.IOCUnity
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:11
    //******************************************
    public interface IUnityApplication
    {
        void OnApplicationQuit();
        void OnApplicationPause(bool pauseStatus);
        void OnApplicationFocus(bool hasFocus);
    }
}