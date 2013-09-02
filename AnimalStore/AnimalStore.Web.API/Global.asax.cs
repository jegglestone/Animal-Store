﻿using AnimalStore.Data.DataContext;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Web.API.App_Start;
using AnimalStore.Web.API.Controllers;
using AnimalStore.Web.API.DependencyResolution;
using Microsoft.Practices.Unity;
using System.Data.Entity;
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
            //config.DependencyResolver = new SimpleContainer();  //home-made simple IoC container

            var unity = new UnityContainer();
            unity.RegisterType<AnimalsController>();
            unity.RegisterType<IRepository<Animal>, AnimalsRepository>(
                new HierarchicalLifetimeManager());
            unity.RegisterType<DbContext, DataContext>(
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
        }
    }
}