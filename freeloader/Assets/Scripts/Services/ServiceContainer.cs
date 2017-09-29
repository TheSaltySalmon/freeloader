using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FreeLoader.Services
{
    public class ServiceContainer
    {
        public IObjectPool ObjectPool;
        public IEventManager EventManager;

        public ServiceContainer()
        {
            StartServices();
        }

        private void StartServices()
        {
            ObjectPool = new ObjectPool();
            EventManager = new EventManager();
        }
    }
}
