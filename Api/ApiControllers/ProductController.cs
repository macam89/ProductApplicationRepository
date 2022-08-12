using System.Collections.Generic;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace Api.ApiControllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _products;

        public ProductController(IProductService products)
        {
            _products = products;
        }


        [Route("ErrorRoute")]
        [HttpGet]
        public Task<string> ErrorExample()
        {
            return null;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var allProducts = _products.GetAllProducts();

            if (allProducts == null)
                return NotFound("Products not found.");

            return Ok(allProducts);
        }


        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = _products.GetProduct(id);
            if (product == null)
                return NotFound("Product not found.");

            return Ok(product);
        }


        [HttpPost]
        public ActionResult<Product> AddNewProduct([FromBody] Product product)
        {
            var resultDictionary = _products.AddNewProduct(product);

            if (resultDictionary.ContainsKey("NameAlreadyExist"))
            {
                return BadRequest("Name already exist.");
            }
            else
            {
                Product newProduct = resultDictionary.GetValueOrDefault("Created");
                return CreatedAtAction("GetProductById", new { id = newProduct.ProductID }, newProduct);
            }
        }


        [HttpPut("{id}")]
        public ActionResult<Product> UpdateProduct(int id, Product product)
        {
            var resultDictionary = _products.UpdateProduct(id, product);

            if (resultDictionary.ContainsKey("NotFound"))
            {
                return NotFound("Product not found.");
            }
            else if (resultDictionary.ContainsKey("NameAlreadyExist"))
            {
                return BadRequest("That product name aleady exist.");
            }
            else
            {
                return NoContent();
            }
        }


        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            bool deleted = _products.DeleteProduct(id);

            if (deleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Product not exist.");
            }
        }

    }
}
