using System.Web.Mvc;
using System.Web.Routing;

namespace AnimalStore.Web.API
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
        name: "SpecificBreedDogSearch",
        url: "Dogs/Breed",
        defaults: new {Controller = "Dogs", action = "GetPaged", breedName = UrlParameter.Optional}
        );

      routes.MapRoute(
        name: "Default",
        url: "{controller}/{action}/{id}",
        defaults: new {controller = "Default", action = "Index", id = UrlParameter.Optional}
        );
    }
  }
}