using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Contrado.Services
{
    public abstract class BaseFactory<T>
    {
        internal IUnityContainer Container { get; private set; }
        public T InstantiateService()
        {
            return InstantiateService(new UnityContainer());
        }
        public T InstantiateService(IUnityContainer container)
        {
            if (container == null)
            {
                container = new UnityContainer();
            }
            Container = container;
            RegisterInterfaces();
            return Container.Resolve<T>();
        }
        internal abstract void RegisterInterfaces();
    }
}
