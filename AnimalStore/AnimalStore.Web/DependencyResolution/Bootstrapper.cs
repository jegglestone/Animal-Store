using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using AnimalStore.Common.Helpers;
using AnimalStore.Model;
using AnimalStore.Web.Wrappers;
using AnimalStore.Web.Wrappers.Interfaces;
using AnimalStore.Web.Helpers;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;
using AnimalStore.Common.Logging;

namespace AnimalStore.Web.DependencyResolution
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            RegisterSimpleTypes(container);

            RegisterFactoryTypes(container);

            return container;
        }

        /// <summary>
        ///  Register all components except controllers and abstract types  
        /// </summary>
        /// <param name="container">IUnityContainer container</param>
        private static void RegisterSimpleTypes(IUnityContainer container)
        {
            container.RegisterType<ISearchAPIFacade, SearchAPIFacade>(
                new TransientLifetimeManager());
            container.RegisterType<SearchViewModel>(
                new TransientLifetimeManager());
            container.RegisterType<LogManager>(
                new TransientLifetimeManager());
            container.RegisterType<IExceptionHelper, ExceptionHelper>(
                new TransientLifetimeManager());
            container.RegisterType<IConfiguration, Configuration>(
                new TransientLifetimeManager());
            container.RegisterType<IDataContractJsonSerializerWrapper, DataContractJsonSerializerWrapper>(
                new TransientLifetimeManager());
            container.RegisterType<ContactInformation>(
                new TransientLifetimeManager());
            container.RegisterType<IWebAPIRequestWrapper, WebAPIRequestWrapper>(
                new TransientLifetimeManager());
            container.RegisterType<IResponseStreamHelper, ResponseStreamHelper>(
                new TransientLifetimeManager());
            container.RegisterType<ICustomHttpRequestWrapper, CustomHttpRequestWrapper>(
                );
        }

        /// <summary>
        /// Register abstract or complex types using factory method
        /// </summary>
        /// <param name="container">Ioc container</param>
        private static void RegisterFactoryTypes(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            container.RegisterType<HttpClient>(
                new InjectionFactory(x =>
                                     new HttpClient()
                    ));

            container.RegisterType<IList<Breed>, List<Breed>>(
                new InjectionFactory(x =>
                                     new List<Breed>()
                    ));

            container.RegisterType<HttpSessionState>(
                new InjectionFactory(x =>
                    HttpContext.Current.Session));

            container.RegisterType<HttpRequest>(
                new InjectionFactory(x =>
                    HttpContext.Current.Request));
        }
    }
}