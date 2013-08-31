using AnimalStore.Data;
using AnimalStore.Data.Repositories;
using AnimalStore.Model;
using AnimalStore.Services.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace AnimalStore.Web.API.DependencyResolution
{
    // A simple implementation of IDependencyResolver, for example purposes.
    public class SimpleContainer : IDependencyResolver
    {
        static readonly IRepository<Animal> repository = new GenericRepository<Animal>(new DataContext());

        public IDependencyScope BeginScope()
        {
            // This example does not support child scopes, so we simply return 'this'.
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(AnimalsController))
            {
                return new AnimalsController(repository);
            }
            else
            {
                return null;
            }
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