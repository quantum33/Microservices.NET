using Microsoft.Extensions.DependencyInjection;

namespace Core.ServiceDiscovery
{
    public class ServiceDiscoveryBuilder
    {
        private IServiceCollection _services;

        public ServiceDiscoveryBuilder(IServiceCollection services)
        {
            this._services = services;
        }
    }
}
