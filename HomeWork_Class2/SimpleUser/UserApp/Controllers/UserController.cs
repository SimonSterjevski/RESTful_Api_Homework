using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserApp.Models;

namespace UserApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, StaticDb.Users);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! Something went wrong!");
            }
        }

        [HttpGet ("{id}")]
        public ActionResult<User> GetAllUsersNames(int id)
        {
            try
            {
                if (id > StaticDb.Users.Count())
                {
                    return StatusCode(StatusCodes.Status404NotFound, "No such user!");
                }
                User user = StaticDb.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "User doesn't exist");
                }
                return StatusCode(StatusCodes.Status200OK, user);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! Something went wrong!");
            }
        }

        [HttpGet("names")]
        public ActionResult<List<string>> GetAllUsersNames()
        {
            try
            {
                return StatusCode(StatusCodes.Status200OK, StaticDb.UsersNames);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! Something went wrong!");
            }
        }

        [HttpPost]
        public IActionResult AddUser()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Request.Body))
                {
                    var user = JsonConvert.DeserializeObject<User>(sr.ReadToEnd());
                    if (user is User)
                    {
                        user.Id = StaticDb.IdUser++;
                        StaticDb.Users.Add(user);
                        return StatusCode(StatusCodes.Status201Created, "User successfully added!");
                    }
                   
                    return StatusCode(StatusCodes.Status400BadRequest, "Try again!");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! Something went wrong!");
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Request.Body))
                {
                    bool isNum = int.TryParse(sr.ReadToEnd(), out int id);
                    if (!isNum)
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Try again!");
                    }
                    User user = StaticDb.Users.FirstOrDefault(x => x.Id == id);
                    if (user == null)
                    {
                        return StatusCode(StatusCodes.Status404NotFound, "User doesn't exist");
                    }
                    StaticDb.Users.Remove(user);
                    return StatusCode(StatusCodes.Status204NoContent, "User deleted");
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry! Something went wrong!");
            }
        }
    }
}
