using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Contrado.Services
{
    public class Extensions
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            // Register your middle layer types here
            //container.RegisterType<IDummy, Dummy>();
            //Data Layer dependency mapping as extension 
            container.AddNewExtension<DependencyInjectionExtension>();
        }
    }
}
