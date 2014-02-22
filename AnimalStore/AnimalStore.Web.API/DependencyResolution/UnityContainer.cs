using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace AnimalStore.Web.API.DependencyResolution
{
    public class ScopeContainer : IDependencyScope
    {
        protected readonly IUnityContainer Container;

        internal ScopeContainer(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            Container = container;
        }

        public object GetService(Type serviceType)
        {
            return Container.IsRegistered(serviceType) ? 
                Container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.IsRegistered(serviceType) ? 
                Container.ResolveAll(serviceType) : new List<object>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }

    class IoCContainer : ScopeContainer, IDependencyResolver
    {
        public IoCContainer(IUnityContainer container)
            : base(container)
        {
        }

        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new ScopeContainer(child);
        }
    }
}