using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AnimalStore.Web.App_Start;
using AnimalStore.Web.DependencyResolution;

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

            var logManager = new Common.Logging.LogManager();
            var log = logManager.GetLogger((typeof(MvcApplication)));

            if (log.IsInfoEnabled)
                log.Info(Common.Constants.SiteNames.DOGSTORE_SITE_NAME + " Web application is starting up");
        }
    }
}