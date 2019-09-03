using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CraftIt.Api.Models;
using CraftIt.Api.Services;
using CraftIt.Api.Helpers;
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
        private readonly IUserService _userService;
    

        public CommentController(IProductRepository productRepository, ICommentService commentService,IUserService userService)
        {
            _productRepository = productRepository;
            _commentService = commentService;
            _userService = userService;
        }


        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CommentCreationDto comment)
        {
            var user = _userService.GetById(int.Parse(User.Identity.Name));

            if(user == null) return Unauthorized();

            try 
            {
                _commentService.AddComment(comment, user);
                return Ok();
            } 
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody] CommentUpdateDto comment)
        {

            var commentInDb = _commentService.GetComment(comment.CommentId);

            if(commentInDb == null) return NotFound();

            if(commentInDb.User.Id != int.Parse(User.Identity.Name)) return Unauthorized();

            try 
            {
                _commentService.UpdateComment(comment);
                return Ok();
            } 
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpDelete]
        public IActionResult Delete(int id){

            var commentInDb = _commentService.GetComment(id);

            if(commentInDb == null) return NotFound();

            if(commentInDb.User.Id != int.Parse(User.Identity.Name)) return Unauthorized();

            try 
            {
                _commentService.DeleteComment(id);
                return Ok();
            } 
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
