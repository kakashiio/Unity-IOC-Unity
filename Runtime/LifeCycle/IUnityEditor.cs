namespace IO.Unity3D.Source.IOCUnity
{
    //******************************************
    //  
    //
    // @Author: Kakashi
    // @Email: john.cha@qq.com
    // @Date: 2022-05-09 01:31
    //******************************************
    public interface IUnityEditor
    {
        void OnDrawGizmos();
        void OnValidate();
        void OnDrawGizmosSelected();
    }
}