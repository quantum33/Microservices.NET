using System;

namespace Core.ServiceDiscovery
{
    internal static class UriExtensions
    {
        public static Uri ConcatWith(string baseAddress, string route)
        {
            if (route.StartsWith("/"))
                return new Uri(baseAddress + route);
            return new Uri($"{baseAddress}/{route}");
        }
    }
}
