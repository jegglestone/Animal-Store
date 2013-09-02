using AnimalStore.Data.DataContext;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using AnimalStore.Web.API.Controllers;

namespace AnimalStore.Web.API.DependencyResolution
{
    // A simple implementation of IDependencyResolver
    public class SimpleContainer : IDependencyResolver
    {
        static readonly IRepository<Animal> repository = new GenericRepository<Animal>(new DataContext());

        public IDependencyScope BeginScope()
        {
            // Does not support child scopes, so we simply return 'this'.
            return this;
        }

        public object GetService(Type serviceType)
        {
            return serviceType == typeof(AnimalsController) ? new AnimalsController(repository) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
            // When BeginScope returns 'this', the Dispose method must be a no-op.
        }
    }
}