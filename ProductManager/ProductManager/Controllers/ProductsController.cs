using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductManager.Core.Data;
using ProductManager.Core.Domain;
using ProductManager.Core.Services;

namespace ProductManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductsController(ProductDbContext context)
        {
            _service = new ProductService(context);
        }

        /// <summary>
        /// Lists all products
        /// </summary>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _service.GetAllProducts();
        }

        /// <summary>
        /// Gets a specific product
        /// </summary>
        /// <param name="id">Product ID</param>
        [HttpGet("{id}", Name = "Get")]
        public Product Get(int id)
        {
            return _service.Get(id);
        }

        /// <summary>
        /// Saves a new product
        /// </summary>
        /// <param name="value">Name and category of the product</param>
        /// <returns>The created product</returns>
        [HttpPost]
        public Product Post([FromBody] Product value)
        {
            return _service.Save(value);
        }

        /// <summary>
        /// Saves a new product
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="value">New name and category of the product</param>
        /// <returns>The updated product</returns>
        [HttpPut("{id}")]
        public Product Put(int id, [FromBody] Product value)
        {
            return _service.Update(id, value);

        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">ID of the product to be deleted</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _service.Delete(id);
        }
    }
}
