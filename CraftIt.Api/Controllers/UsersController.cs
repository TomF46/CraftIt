using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using CraftIt.Api.Models;
using CraftIt.Api.Helpers;
using CraftIt.Api.Services;

namespace WebApi.Controllers
{
    /// <summary>Class <c>UserController</c> API controller for commands related to user management</summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        /// <summary>Returns a list of all users</summary>
        [HttpGet]
        public IActionResult GetAll()
        {
            var users =  _userService.GetAll();
            var userDtos = _mapper.Map<IList<UserDto>>(users);
            return Ok(userDtos);
        }

        /// <summary>Returns the user that is currently logged in.</summary>
        [HttpGet] 
        [Route("Me")]
        public IActionResult Me(){
            var me = int.Parse(User.Identity.Name);
            var user =  _userService.GetById(me);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        /// <summary>Takes a user id and return that user if it exists else return error message</summary>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var user =  _userService.GetById(id);
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        
        /// <summary>Takes a edited user and updates the matching user in the databse if valid else return error.</summary>
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]UserUpdateDto userUpdateDto)
        {
            if(id != int.Parse(User.Identity.Name)) return Unauthorized();
            
            var user = _mapper.Map<User>(userUpdateDto);
            user.Id = id;

            try 
            {
                _userService.Update(user, userUpdateDto.Password);
                return Ok();
            } 
            catch(AppException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        /// <summary>Takes an user id and removes the user with that id from the databse if it exists else return error, user id must match logged in user id.</summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {

            if(id != int.Parse(User.Identity.Name)) return Unauthorized();

            _userService.Delete(id);
            return Ok();
        }
    }
}