using System;
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
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentController : Controller
    {

        private readonly IProductRepository _productRepository;
        private readonly ICommentService _commentService;
    

        public CommentController(IProductRepository productRepository, ICommentService commentService)
        {
            _productRepository = productRepository;
            _commentService = commentService;
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CommentCreationDto comment)
        {
            _commentService.AddComment(comment);

            if(!_commentService.Save()) throw new Exception("Failed to post new comment");

            return Ok();
        }

        [HttpPut]
        public IActionResult Put([FromBody] CommentUpdateDto comment)
        {
            _commentService.UpdateComment(comment);

            if(!_commentService.Save()) throw new Exception("Failed to update comment");

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id){

            _commentService.DeleteComment(id);
            if(!_commentService.Save()) throw new Exception("Failed to delete comment");

            return Ok();
        }
    }
}
