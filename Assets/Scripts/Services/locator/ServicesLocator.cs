using System;
using System.Collections.Generic;

namespace Services.locator
{
    public class ServicesLocator : IServicesLocator
    {
        private Dictionary<Type, object> _services = new();

        public ServicesLocator Registration<T>(T service)
        {
            Type serviceType = typeof(T);

            _services.TryAdd(serviceType, service);
            return this;
        }

        public T GetServices<T>()
        {
            if (_services.TryGetValue(typeof(T), out object service))
            {
                return (T)service;
            }
            else
            {
                throw new Exception("Service: " + typeof(T).Name + " is not found!");
            }
        }
    }
}