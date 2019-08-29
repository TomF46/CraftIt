using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftIt.Api.Models;
using CraftIt.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CraftIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET api/values
        [HttpGet]
        public ActionResult Get()
        {
            var products = _productRepository.GetAllProducts();

            var productsDto = products.Select(x => new ProductDto{
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                TimeEstimate = x.TimeEstimate,
                Requirements = JsonConvert.DeserializeObject<String[]>(x.Requirements),
                Instructions = x.Instructions
            });

            return Ok(productsDto);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var product = _productRepository.GetProduct(id);

            if(product == null) return NotFound();

            var productDto = new ProductDto{
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                TimeEstimate = product.TimeEstimate,
                Requirements = JsonConvert.DeserializeObject<String[]>(product.Requirements),
                Instructions = product.Instructions
            };

            return Ok(productDto);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] ProductCreationDto product)
        {
            _productRepository.AddProduct(product);

            if (!_productRepository.Save()) throw new Exception("Failed to create product");

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProductCreationDto product, int id)
        {
            _productRepository.UpdateProduct(id, product);

            if (!_productRepository.Save()) throw new Exception("Failed to update product");

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id){
            var productToDelete = _productRepository.GetProduct(id);

            if(productToDelete == null) return NotFound();

            _productRepository.DeleteProduct(productToDelete);

            if (!_productRepository.Save()) throw new Exception("Failed to delete product");

            return Ok();
        }
    }
}
