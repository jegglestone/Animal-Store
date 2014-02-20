using System.Data.SqlClient;
using AnimalStore.Data.DataContext;
using AnimalStore.Data.Repositories;
using AnimalStore.Data.UnitsOfWork;
using AnimalStore.Model;
using AnimalStore.Web.API.App_Start;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Web.API.DependencyResolution;
using AnimalStore.Web.API.Helpers;
using AnimalStore.Web.API.Strategies;
using AnimalStore.Web.API.Wrappers;
using Microsoft.Practices.Unity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AnimalStore.Web.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private const int _defaultConnectionTimeout = 20;
        private const int _extendedConnectionTimeoutForBulkDataSeeding = 100;

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
            unity.RegisterType<IPlacesRepository, PlacesRepository>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDogBreedFilterStrategy, DogBreedFilter>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDogCategoryFilterStrategy, DogCategoryFilter>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDoglocationFilterStrategy, DogLocationFilter>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IAnimalsDataContext, AnimalsDataContext>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IDogSearchHelper, DogSearchHelper>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<IConfiguration, Configuration>(
                new TransientLifetimeManager());

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
                //TODO: move time values to constants
                var dataContext = new AnimalsDataContext();
                dataContext.Database.Initialize(true);

                var placesContext = new PlacesDataContext();
                placesContext.Database.CommandTimeout = _extendedConnectionTimeoutForBulkDataSeeding;
                placesContext.Database.Initialize(true);
                placesContext.Database.CommandTimeout = _defaultConnectionTimeout;
            }
            catch (SqlException e)
            {
                log.Error("SQL Exception - mostly likely the database is in use and can't be dropped and initialised", e);
            }

            log.Info(Common.Constants.SiteNames.DOGSTORE_SITE_NAME + " Service API is starting up and database initialisation is complete");
        }
    }
}