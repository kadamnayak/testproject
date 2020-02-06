using Contrado.DA;
using Contrado.Services;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace ContradoSample
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            container.RegisterType<IProductCategoryRepository, ProductCategoryRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IProductAttributeRepository, ProductAttributeRepository>();
            container.RegisterType<IProductAttributeLookupRepository, ProductAttributeLookupRepository>();

            container.RegisterType<IProductCategoryService, ProductCategoryService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IProductAttributeLookupService, ProductAttributeLookupService>();
            container.RegisterType<IProductAttributeService, ProductAttributeService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.AspNet.WebApi.UnityDependencyResolver(container);

        }
    }
}