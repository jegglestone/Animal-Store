using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;
using System.Net.Http;
using System.Configuration;

namespace AnimalStore.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      RegisterTypes(container);

      return container;
    }


    /// Register all your components with the container here -
    /// it is NOT necessary to register your controllers  
    public static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<ISearchRepository, HttpSearchRepository>(
            new HierarchicalLifetimeManager());
        container.RegisterType<SearchViewModel>(
            new HierarchicalLifetimeManager());

        //Unity tries to create the HttpClient using the HttpClient(HttpMessageHandler) or HttpClient(HttpMessageHandler, Boolean) constructor. HttpMessageHandler is an abstract class, so it can't create an instance of it
        //use the factory method to register it instead
        container.RegisterType<HttpClient>(
            new InjectionFactory(x =>
                new HttpClient {  }
            )
        ); 
    }
  }
}