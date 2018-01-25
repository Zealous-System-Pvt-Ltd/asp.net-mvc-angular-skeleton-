using Demo.Business.Contracts;
using Demo.Business.Manager;
using Demo.Data;
using Demo.Data.Contracts;
using Demo.Web.CachingProviders;
using FluentValidation;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;
using WebApi.OutputCache.Core.Cache;

namespace Demo.Web
{
    public static class UnityConfig
    {
        /// <summary>
        /// Register components with Unity IoC.
        /// </summary>
        public static void RegisterComponents(HttpConfiguration config)
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // this registration is required if you are injecting dependencies in your validator objects.
            container.RegisterType<IValidatorFactory, UnityValidatorFactory>(new ContainerControlledLifetimeManager());

            // singleton registration
            container.RegisterType<IApiOutputCache, DemoMemoryCache>(new ContainerControlledLifetimeManager());
            container.RegisterType<ICacheManager, CacheManager>(new ContainerControlledLifetimeManager());

            // transient registration , when ever resolve is requested a new instance is returned.

            container.RegisterType<IDemoUnitOfWork, DemoUnitOfWork>(new TransientLifetimeManager());
            container.RegisterType<IEmployeeManager, EmployeeManager>();
            container.RegisterType<ISecurityManager, SecurityManager>();
            container.RegisterType<IDesignationManager, DesignationManager>();
            var provider = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => provider);
            config.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}