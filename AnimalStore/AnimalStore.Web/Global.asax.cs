using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AnimalStore.Web.App_Start;
using AnimalStore.Web.DependencyResolution;

using AnimalStore.Web.Controllers;
using AnimalStore.Web.Repository;
using Microsoft.Practices.Unity;
using AnimalStore.Web.ViewModels;
using System.Net.Http;

namespace AnimalStore.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            Bootstrapper.Initialise();
        }
    }
}