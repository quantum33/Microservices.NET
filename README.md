# Microservices.NET
.NETCore microservice (right now, it's just a service discovery wrapper using Microphone - Consul)

# How to:
Local:
* Install Consul https://www.consul.io/
* terminal -> consul agent -dev
* Visual Studio -> launch the 2 webservices (orders, products)
* localhost:8500 -> you must see "consul", "orders" and "products" services
The "orders" service magically calls the products services via the "Core.ServiceDiscovery" (Consul implementation)