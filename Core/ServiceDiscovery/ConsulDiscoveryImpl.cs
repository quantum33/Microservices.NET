using Microphone;
using Microphone.Consul;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.ServiceDiscovery
{
    public class ConsulDiscoveryImpl : ServiceDiscoveryBase, IServiceDiscovery
    {
        internal readonly Lazy<ILogger> _logger;
        private readonly Lazy<ConsulProvider> _provider;

        public ConsulDiscoveryImpl(ILoggerFactory loggerFactory, IOptions<ServiceDiscoveryOptions> options)
        :base(loggerFactory, "consulDiscoverLogger", options)
        {
            _provider = new Lazy<ConsulProvider>(() => new ConsulProvider(loggerFactory, Options.Create(DiscoveryOptions.Value.ToConsulOptions())));
            _logger = new Lazy<ILogger>(() => loggerFactory.CreateLogger(LoggerCategoryName));
        }


        public void RegisterService(Uri uri, string serviceName, string version, params string[] tags)
        {
            _logger.Value.LogInformation($"Try to register service {serviceName}");
            Cluster.RegisterService(uri, _provider.Value, serviceName, version, _logger.Value, tags);
            _logger.Value.LogInformation($"End register service {serviceName}");
        }

        public string CallService(string serviceName, string route)
        {
            return CallServiceAsync(serviceName, route).Result;
        }

        public async Task<string> CallServiceAsync(string serviceName, string route)
        {
            var serviceInstances = GetServiceInstances(serviceName);
            if (serviceInstances.Count > 0)
            {
                var result = await Utility.HttpGetAsync(serviceInstances.First(), route);
                return result;
            }
            //return await new Task<string>(() => string.Empty);
            return string.Empty;
        }

        public IReadOnlyCollection<ServiceData> GetServiceInstances(string serviceName)
        {
            var instances = Cluster.Client.GetServiceInstances("products");
            return instances.ToServiceData(serviceName);
        }
    }
}
