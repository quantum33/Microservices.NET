using Microphone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Core.ServiceDiscovery
{
    internal static class Utility
    {
        //private static IReadOnlyCollection<ServiceData> GetServiceInstances(string serviceName)
        //{
        //    var instances = Cluster.Client.GetServiceInstances(serviceName);
        //    return instances.ToServiceData(serviceName);
        //}

        //public static string HttpGet(string serviceName, string route)
        //{
        //    var serviceInstances = GetServiceInstances(serviceName);
        //    if (serviceInstances.Count > 0)
        //        return HttpGetAsync(serviceInstances.First(), route).Result;
        //    return string.Empty;
        //}
        internal static async Task<string> HttpGetAsync(ServiceData service, string route)
        {
            using (var http = new HttpClient { BaseAddress = UriExtensions.ConcatWith(service.BaseAddress, route) })
            {
                var res = await http.GetStringAsync("");
                return res;
            }
        }
    }
}
