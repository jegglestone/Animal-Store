using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using AnimalStore.Web.Repository;
using AnimalStore.Web.ViewModels;

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

      RegisterTypes(container);

      return container;
    }

    /// Register all your components with the container here -
    /// it is NOT necessary to register your controllers  
    private static void RegisterTypes(IUnityContainer container)
    {
        container.RegisterType<ISearchRepository, HttpSearchRepository>(
            new HierarchicalLifetimeManager());
        container.RegisterType<SearchViewModel>(
            new HierarchicalLifetimeManager());
    }
  }
}