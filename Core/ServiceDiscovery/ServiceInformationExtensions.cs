using Microphone;
using System.Collections.Generic;

namespace Core.ServiceDiscovery
{
    internal static class ServiceInformationExtensions
    {
        public static ServiceData ToServiceData(this ServiceInformation serviceInformation, string serviceName)
        {
            if (serviceInformation == null)
                return null;
            return new ServiceData(serviceName, serviceInformation.Host, serviceInformation.Port);
        }
        public static IReadOnlyCollection<ServiceData> ToServiceData(this ServiceInformation[] serviceInformations, string serviceName)
        {
            if (serviceInformations == null || serviceInformations.Length == 0)
                return new List<ServiceData>(0);

            var result = new List<ServiceData>(serviceInformations.Length);
            for (int i = 0; i < serviceInformations.Length; i++)
                result.Add(serviceInformations[i].ToServiceData(serviceName));
            return result;
        }
    }
}
