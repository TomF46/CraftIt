using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CraftIt.Api.Models;
using CraftIt.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CraftIt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestingController : Controller
    {

        private readonly IProductRepository _productRepository;

        public TestingController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // POST api/values
        [HttpGet]
        public IActionResult AddTestProduct()
        {
            //TODO generate ordinals in repo
            // var product = new ProductCreationDto{
            //     Name = "Test product",
            //     Description = "A Test product",
            //     TimeEstimate = "1 hour",
            //     Requirements = new Collection<string>{
            //         "Scissiors",
            //         "Paper",
            //         "Glue"
            //     },
            //     Instructions = new Collection<InstructionDto>{
            //         new Instruction {
            //            Description = "Cut up paper"
            //         },
            //         new Instruction {
            //             Description = "Stick it down",
            //         }
            //     }
            // };

            // _productRepository.AddProduct(product);

            // if (!_productRepository.Save()) throw new Exception("Failed to create product");

            return Ok();
        }
    }
}
