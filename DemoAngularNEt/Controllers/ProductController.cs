using DemoAngularNEt.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAgainNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        // Dummy data for demonstration
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1500.00M },
            new Product { Id = 2, Name = "Smartphone", Price = 800.00M },
            new Product { Id = 3, Name = "Headphones", Price = 200.00M }
        };

        // GET: api/Product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            return Ok(Products);
        }

        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product newProduct)
        {
            if (newProduct == null || string.IsNullOrEmpty(newProduct.Name) || newProduct.Price <= 0)
                return BadRequest("Invalid product data.");

            newProduct.Id = Products.Max(p => p.Id) + 1;
            Products.Add(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { id = newProduct.Id }, newProduct);
        }

        // PUT: api/Product/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            if (updatedProduct == null || string.IsNullOrEmpty(updatedProduct.Name) || updatedProduct.Price <= 0)
                return BadRequest("Invalid product data.");

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return NoContent();
        }

        // DELETE: api/Product/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                return NotFound();

            Products.Remove(product);
            return NoContent();
        }
    }
}
