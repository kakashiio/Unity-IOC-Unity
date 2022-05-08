using System.Collections.Generic;
using System.Reflection;
using IO.Unity3D.Source.IOC;
using IO.Unity3D.Source.Reflection;
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
    public class BasicDemo : MonoBehaviour
    {
        void Start()
        {
            ITypeContainer typeContainer = new TypeContainerCollection(new List<ITypeContainer>
            {
                new TypeContainer(Assembly.GetExecutingAssembly()),
                new TypeContainer(typeof(UnityIOCContainer).Assembly),
                new TypeContainer(typeof(IOCContainer).Assembly)
            });
            new UnityIOCContainer(typeContainer);
        }
    }

}
