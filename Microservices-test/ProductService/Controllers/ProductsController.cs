using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        // GET api/products
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "product1", "product2" };
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/products
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
