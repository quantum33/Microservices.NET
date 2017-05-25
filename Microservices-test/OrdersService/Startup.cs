using Core.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace OrdersService
{
    public class Startup
    {
        private ServiceData _consulServer = new ServiceData("ConsulServer", "localhost", 8500);
        private ServiceData _webService = new ServiceData("orders", "localhost", 62042);

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("discoverySettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            //services.AddServiceDiscovery(
            //    new ConsulDiscoveryImpl(new ServiceDiscoveryOptions
            //    {
            //        Host = _consulServer.Host,
            //        Port = _consulServer.Port
            //    }));

            // 1.
            // ok
            /*            services.AddServiceDiscovery();
                        services.ConfigureServiceDiscovery(new ServiceDiscoveryOptions
                        {
                            Name = "consul",
                            Host = _consulServer.Host,
                            Port = _consulServer.Port
                        });
             */           // fin ok

            //services.AddServiceDiscovery<ConsulDiscoveryImpl>();

            /*
            services.AddOptions();
            services.Configure<ServiceDiscoveryOptions>(o =>
            {
                o.Name = _consulServer.Name;
                o.Host = _consulServer.Host;
                o.Port = _consulServer.Port;
            });*/

            // 2.
            //services.AddOptions();
            //services.Configure<ServiceDiscoveryOptions>(Configuration.GetSection("serviceDiscoveryOptions"));

            // 3.
            services.AddServiceDiscovery(Configuration);
            //services.AddSingleton<IConfiguration>(Configuration);
        }
        internal IOptions<ServiceDiscoveryOptions> _svcOptions;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseMvc();
            app.UseServiceDiscovery(new Uri(_webService.BaseAddress), _webService.Name, "v1", new [] { "orders" });
        }
    }
}
