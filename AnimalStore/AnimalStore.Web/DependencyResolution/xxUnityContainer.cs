using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace AnimalStore.Web.DependencyResolution
{
    public class ScopeContainer : IDependencyScope
    {
        protected readonly IUnityContainer _container;

        internal ScopeContainer(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return _container.IsRegistered(serviceType) ?
                _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.IsRegistered(serviceType) ?
                _container.ResolveAll(serviceType) : new List<object>();
        }

        public void Dispose()
        {
            _container.Dispose();
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
            var child = _container.CreateChildContainer();
            return new ScopeContainer(child);
        }
    }
}