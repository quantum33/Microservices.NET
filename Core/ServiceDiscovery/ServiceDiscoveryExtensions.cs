using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;


namespace Core.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static ServiceDiscoveryBuilder AddServiceDiscovery(this IServiceCollection services, IConfiguration configuration)
        {
            var type = ExtractServiceDiscoveryImplementation(configuration);
            if (type == typeof(ConsulDiscoveryImpl))
                services.AddSingleton<IServiceDiscovery, ConsulDiscoveryImpl>();

            services.AddOptions();
            services.Configure<ServiceDiscoveryOptions>(configuration.GetSection("serviceDiscoveryOptions"));
            return new ServiceDiscoveryBuilder(services);

            Type ExtractServiceDiscoveryImplementation(IConfiguration conf)
            {
                var discoveryOptions = conf.ToServiceDiscoveryOptions();
                return discoveryOptions.GetImplementation();
            }
        }

        public static ServiceDiscoveryBuilder AddServiceDiscovery<TServiceDiscovery>(this IServiceCollection services/*, TServiceDiscovery instance*/)
            where TServiceDiscovery : class, IServiceDiscovery
        {
            services.AddOptions();
            //services.AddSingleton<IServiceDiscovery>(instance);
            services.AddSingleton<IServiceDiscovery, TServiceDiscovery>();
            //services.AddSingleton<IServiceDiscovery>(new ConsulDiscoveryImpl(loggerName, options));

            return new ServiceDiscoveryBuilder(services);
        }

        public static IApplicationBuilder UseServiceDiscovery(this IApplicationBuilder self, Uri serviceUri, string serviceName, string version, params string[] tags)
        {
            var loggingFactory = self.ApplicationServices.GetRequiredService<ILoggerFactory>();
            var logger = loggingFactory.CreateLogger("ServiceDiscovery Framework");
            var serviceDiscovery = self.ApplicationServices.GetRequiredService<IServiceDiscovery>();
            try
            {
                serviceDiscovery.RegisterService(serviceUri, serviceName, version, tags);
            }
            catch (Exception x)
            {
                logger.LogCritical(x.ToString());
                throw;
            }
            return self;
        }
    }
}
