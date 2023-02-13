
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;

namespace FlyEatsApp.Controllers
{
    using System;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            UserProvider userProvider = new UserProvider();
            var result = userProvider.GetUserById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddNewUser([FromBody] User user)
        {
            UserProvider userProvider = new UserProvider();
            userProvider.InsertUser(user);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] User user)
        {
            UserProvider userProvider = new UserProvider();
            var result = userProvider.UpdateUser(user);

            if (result != null)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }

        [HttpPost]
        public object LoginUser([FromBody] User user)
        {
            UserProvider userProvider = new UserProvider();
            var results = userProvider.AuthenticateUser(user.Email, user.Password);
            if (results == null)
            {
                return BadRequest(false);
            }
            return results;
        }




        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            UserProvider userProvider = new UserProvider();
            var result = userProvider.DeleteUser(id);
            if (result != null)
            {
                return Ok(true);
            }
            else
            {
                return BadRequest(false);
            }
        }
    }

}
