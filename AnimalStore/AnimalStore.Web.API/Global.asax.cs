using System;
using AnimalStore.Data.DataContext;
using AnimalStore.Data.Repositories;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Web.API.App_Start;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Web.API.DependencyResolution;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AnimalStore.Web.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        static void ConfigureApi(HttpConfiguration config)
        {
            var unity = new UnityContainer();
            unity.RegisterType<DogsController>();
            unity.RegisterType<BreedsController>();
            unity.RegisterType<IUnitOfWork, UnitOfWork<AnimalsDataContext>>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IContext, AnimalsDataContext>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IRepository<Dog>, DogsRepository>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IRepository<Breed>, BreedsRepository>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IAnimalsDataContext, AnimalsDataContext>(
                new HierarchicalLifetimeManager());
            config.DependencyResolver = new IoCContainer(unity);
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            ConfigureApi(GlobalConfiguration.Configuration);

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var dataContext = new AnimalsDataContext();
            dataContext.Database.Initialize(true);

            var logManager = new Common.Logging.LogManager();
            var log = logManager.GetLogger((typeof(WebApiApplication)));

            log.Info(Common.Constants.SiteNames.DOGSTORE_SITE_NAME + " Service API is starting up and database initialisation is complete");
        }
    }
}