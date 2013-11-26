using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using AnimalStore.Model;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;
using AnimalStore.Common.Logging;
using System.Runtime.Serialization.Json;

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
            container.RegisterType<ISearchRepository, HttpSearchRepository>(
                new HierarchicalLifetimeManager());
            container.RegisterType<SearchViewModel>(
                new HierarchicalLifetimeManager());
            container.RegisterType<LogManager>(
                new HierarchicalLifetimeManager());
            container.RegisterType<ContactInformation>(
                new HierarchicalLifetimeManager());
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
                    )
                );

            container.RegisterType<IList<Breed>, List<Breed>>(
                new InjectionFactory(x =>
                                     new List<Breed>()
                    )
                );

            container.RegisterType<DataContractJsonSerializer>(
                new InjectionFactory(x =>
                                     new DataContractJsonSerializer(typeof (List<Breed>))
                    )
                );
        }
    }
}