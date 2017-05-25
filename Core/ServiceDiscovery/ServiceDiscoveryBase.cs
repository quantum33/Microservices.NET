using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Core.ServiceDiscovery
{
    public abstract class ServiceDiscoveryBase
    {
        protected ILoggerFactory LoggerFactory { get; }
        protected IOptions<ServiceDiscoveryOptions> DiscoveryOptions { get; }
        protected string LoggerCategoryName { get; }

        protected ServiceDiscoveryBase(ILoggerFactory loggerFactory, string loggerCategoryName, IOptions<ServiceDiscoveryOptions> options)
        {
            LoggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            DiscoveryOptions = options ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrEmpty(loggerCategoryName))
                throw new ArgumentNullException(nameof(loggerCategoryName));
            LoggerCategoryName = loggerCategoryName;
        }
    }
}
