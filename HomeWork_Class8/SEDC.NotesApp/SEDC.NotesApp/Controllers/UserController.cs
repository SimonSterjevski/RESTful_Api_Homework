using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.NotesApp.Helpers;
using SEDC.NotesApp.Models;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Enums;
using SEDC.NotesApp.Shared.Exceptions;


namespace SEDC.NotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
       
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] Models.RegisterModel registerModel)
        {
            try
            {
                _userService.RegisterUser(registerModel);
                return Ok("User registered!");
            }
            catch (UserException ex)
            {
                return BadRequest(ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured!");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] Models.LoginModel loginModel)
        {
            try
            {
                return Ok(_userService.LoginUser(loginModel));
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "The user was not identified!");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occured!");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<UserModel>> Get()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetAllUsers());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserModel> GetById(int id)
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, _userService.GetUserById(id));
            }
            catch (BadRequestException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel userModel)
        {
            try
            {
                _userService.AddUser(userModel);
                return StatusCode(StatusCodes.Status201Created, "User Created!");
            }
            catch (BadRequestException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (NoteException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] UserModel userModel)
        {
            try
            {
                string userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                if (userId == userModel.Id.ToString())
                {
                    _userService.UpdateUser(userModel);
                    return StatusCode(StatusCodes.Status204NoContent, "User updated!");
                }
                return StatusCode(StatusCodes.Status401Unauthorized, "Action forbidden!");
            }
            catch (BadRequestException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch (NoteException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return StatusCode(StatusCodes.Status204NoContent, "User deleted!");
            }
            catch (BadRequestException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (NotFoundException ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
            }
        }

        //[HttpGet("mostusedtag")]
        //public ActionResult<string> GetMostUsedTag()
        //{
        //    try
        //    {
        //        return StatusCode(StatusCodes.Status200OK, _userService.GetMostUsedTag());
        //    }
        //    catch
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!");
        //    }
        //}
    }
}
