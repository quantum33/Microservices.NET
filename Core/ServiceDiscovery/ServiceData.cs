using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.ServiceDiscovery
{
    public class ServiceData
    {
        public string Host { get; }
        public int Port { get; }
        public string Name { get; }
        private readonly Lazy<string> _baseUri;

        public ServiceData(string name, string host, int port)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));
            if (string.IsNullOrEmpty(host)) throw new ArgumentNullException(nameof(host));

            Name = name;
            Host = host;
            Port = port;
            _baseUri = new Lazy<string>(() => $"http://{Host}:{Port}");
        }
        public string BaseAddress { get { return _baseUri.Value; } }
    }
}
