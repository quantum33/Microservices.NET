using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.ServiceDiscovery
{
    public interface IServiceDiscovery
    {
        void RegisterService(Uri uri, string serviceName, string version, params string[] tags);
        string CallService(string serviceName, string route);
        Task<string> CallServiceAsync(string serviceName, string route);
        IReadOnlyCollection<ServiceData> GetServiceInstances(string serviceName);
    }
}
