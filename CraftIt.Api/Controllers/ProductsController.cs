﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftIt.Api.Models;
using CraftIt.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CraftIt.Api.Controllers
{
    /// <summary>Class <c>ProductsController</c> API controller for commands related to Products</summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductsController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly IUserService _userService;

        public ProductsController(IProductRepository productRepository, IUserService userService)
        {
            _productRepository = productRepository;
            _userService = userService;
        }

        /// <summary>Returns a list of all products.</summary>
        [HttpGet]
        public ActionResult Get()
        {

            var name = int.Parse(User.Identity.Name);

            var products = _productRepository.GetAllProducts();

            var productsDto = products.Select(x => new ProductDto{
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                TimeEstimate = x.TimeEstimate,
                ProductImage = x.ProductImage != null ? Convert.ToBase64String(x.ProductImage) : null
            });

            return Ok(productsDto);
        }

        /// <summary>Takes an product id and returns matching product if it exists else return error message</summary>
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var product = _productRepository.GetProduct(id);

            if(product == null) return NotFound();

            var productDto = new ProductDetailDto{
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                TimeEstimate = product.TimeEstimate,
                Requirements = JsonConvert.DeserializeObject<String[]>(product.Requirements),
                Instructions = convertInstructions(product.Instructions),
                Comments = convertComments(product.Comments),
                ProductImage = product.ProductImage != null ? Convert.ToBase64String(product.ProductImage) : null
            };

            return Ok(productDto);
        }

        private ICollection<CommentDto> convertComments(ICollection<Comment> comments)
        {
            var commentDtos = new List<CommentDto>();

            comments.ToList().ForEach(x => {
                commentDtos.Add(new CommentDto{
                    Id = x.Id,
                    Username = x.User.Username,
                    Message = x.Message,
                    CreatedAt = x.CreatedAt
                });
            });

            return commentDtos;
        }

        private ICollection<InstructionDto> convertInstructions(ICollection<Instruction> instructions)
        {
            var instructionDtos = new List<InstructionDto>();

            instructions.ToList().ForEach(x => {
                instructionDtos.Add(new InstructionDto{
                    Id = x.Id,
                    Ordinal = x.Ordinal,
                    Description = x.Description,
                    Image =  x.Image != null ? Convert.ToBase64String(x.Image): null
                });
            });

            return instructionDtos;
        }

        /// <summary>Takes a product and add to database if its valid else return error message</summary>
        [HttpPost]
        public IActionResult Post([FromBody] ProductCreationDto product)
        {
            var user = _userService.GetById(int.Parse(User.Identity.Name));

            if(user == null) return Unauthorized();

            _productRepository.AddProduct(product, user);

            if (!_productRepository.Save()) throw new Exception("Failed to create product");

            return Ok();
        }

        /// <summary>Takes an edited product and update the product in the database if its valid else return error message</summary>
        [HttpPut]
        public IActionResult Put([FromBody] ProductUpdateDto product)
        {
            var productToUpdate = _productRepository.GetProduct(product.Id);

            if(productToUpdate == null) return NotFound();

            if(productToUpdate.AddedBy.Id != int.Parse(User.Identity.Name)) return Unauthorized();

            _productRepository.UpdateProduct(product);

            if (!_productRepository.Save()) throw new Exception("Failed to update product");

            return Ok();
        }

        /// <summary>Takes a product id and remove the product with that id from the database if its valid return 200 else return error message</summary>
        [HttpDelete]
        public IActionResult Delete(int id){
            var productToDelete = _productRepository.GetProduct(id);

            if(productToDelete == null) return NotFound();

            if(productToDelete.AddedBy.Id != int.Parse(User.Identity.Name)) return Unauthorized();

            _productRepository.DeleteProduct(productToDelete);

            if (!_productRepository.Save()) throw new Exception("Failed to delete product");

            return Ok();
        }
    }
}
