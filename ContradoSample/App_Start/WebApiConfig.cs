using Contrado.DA;
using Contrado.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Mvc5;

namespace ContradoSample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            //var container = IocContainer.Instance; // Or any other way to fetch your container.
            //config.DependencyResolver = new UnityDependencyResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
    public class UnityConfiguration
    {
        public IUnityContainer Config()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductAttributeRepository, ProductAttributeRepository>();
            container.RegisterType<IProductAttributeLookupRepository, ProductAttributeLookupRepository>();

            container.RegisterType<IProductCategoryService, ProductCategoryService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductAttributeLookupService, ProductAttributeLookupService>();
            container.RegisterType<IProductAttributeService, ProductAttributeService>();

            // return the container so it can be used for the dependencyresolver.  
            return container;
        }
    }
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            container.Dispose();
        }
    }
}