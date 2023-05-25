
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
                return Ok(new { success = false, message = "" });
            }
        }

        [HttpPost]
        public object LoginUser([FromBody] User user)
        {
            UserProvider userProvider = new UserProvider();
            var results = userProvider.AuthenticateUser(user.Email, user.Password);
            if (results == null)
            {
                return Ok(new { success = false, message = "Sorry, we couldn't verify your credentials. Please check your username and password and try again. If you need help, click on 'Forgot Password' or 'Sign Up' to create a new account." });
            }
            return Ok(results);
        }




        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            UserProvider userProvider = new UserProvider();
            var result = userProvider.DeleteUser(id);
            if (result != null)
            {
                return Ok(new { success = true, message = "" });
            }
            else
            {
                return BadRequest(false);
            }
        }
    }

}
