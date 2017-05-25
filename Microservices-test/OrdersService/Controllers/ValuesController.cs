using Core.ServiceDiscovery;
using Microservices.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        public ValuesController(IServiceDiscovery serviceDiscovery)
        {
            _serviceDiscovery = serviceDiscovery ?? throw new ArgumentNullException(nameof(serviceDiscovery), "Check the service configuration");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var instances = _serviceDiscovery.GetServiceInstances("products");

            if (instances.Count > 0)
            {
                var res = HttpUtility.HttpGetAsync($"http://{instances.First().Host}:{instances.First().Port}/api/products").Result;
                return new string[] { res };
            }
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
