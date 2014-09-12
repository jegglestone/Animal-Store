using System.Configuration;
using System.Data.SqlClient;
using AnimalStore.Common.Configuration;
using AnimalStore.Data.DataContext;
using AnimalStore.Data.Repositories.Animals;
using AnimalStore.Data.Repositories.Places;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Model.Settings;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Web.API.DependencyResolution;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Strategies;
using AnimalStore.Web.API.Utilities;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MongoDB.Driver;
using AnimalStore.Web.API.Services;
using Configuration = AnimalStore.Common.Configuration.Configuration;

namespace AnimalStore.Web.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private const int _defaultConnectionTimeout = 20;
        private const int _extendedConnectionTimeoutForBulkDataSeeding = 1000;

        static void ConfigureApi(HttpConfiguration config)
        {
            var unity = new UnityContainer();

            unity.RegisterType<BreedsController>();
            unity.RegisterType<DogsController>();
            unity.RegisterType<PlacesController>();

            unity.RegisterType<IUnitOfWork, UnitOfWork<AnimalsDataContext>>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IContext, AnimalsDataContext>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IAnimalsDataContext, AnimalsDataContext>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IRepository<Breed>, BreedsRepository>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IConfiguration, Configuration>(
                new TransientLifetimeManager());
            unity.RegisterType<IDogBreedFilterStrategy, DogBreedFilter>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDogCategoryFilterStrategy, DogCategoryFilter>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDogCategoryService, DogCategoryService>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDogLocationFilterStrategy, DogLocationFilter>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IRepository<Dog>, DogsRepository>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDogSearchManager, DogSearchManager>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IPlacesRepository, PlacesRepository>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<MongoClient>(
                new InjectionFactory(
                  x => new MongoClient(
                    ConfigurationManager.AppSettings[AppSettingKeys.MongoServer])
            ));
        
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

            var logManager = new Common.Logging.LogManager();
            var log = logManager.GetLogger((typeof(WebApiApplication)));

            try
            {
                var dataContext = new AnimalsDataContext();
                dataContext.Database.CommandTimeout = _extendedConnectionTimeoutForBulkDataSeeding;
                dataContext.Database.Initialize(true);
                dataContext.Database.CommandTimeout = _defaultConnectionTimeout;
            }
            catch (SqlException e)
            {
                log.Error("SQL Exception - mostly likely the database is in use and can't be dropped and initialised", e);
            }

            log.Info(Common.Constants.SiteNames.DOGSTORE_SITE_NAME + " Service API is starting up and database initialisation is complete");
        }
    }
}