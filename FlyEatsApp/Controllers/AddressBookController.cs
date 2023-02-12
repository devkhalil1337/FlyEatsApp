using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AddressBookController : Controller
    {
        [HttpGet]
        public object GetAddressesByUserId(int userId)
        {
            AddressProvider addressProvider = new AddressProvider();
            var results = addressProvider.GetAddressesByUserId(userId);
            return results;
        }

        [HttpPost]
        public object AddNewAddress([FromBody] AddressBook address)
        {
            AddressProvider addressProvider = new AddressProvider();
            var results = addressProvider.AddAddress(address);
            return results;
        }


        [HttpPut]
        public object UpdateAddress([FromBody] AddressBook address)
        {
            AddressProvider addressProvider = new AddressProvider();
            var results = addressProvider.UpdateAddress(address);
            return results;
        }

        [HttpGet("{id}")]
        public IActionResult GetAddressById(int id)
        {
            AddressProvider addressProvider = new AddressProvider();
            var result = addressProvider.GetAddressById(id);
            if(result == null || result.Active == null)
            {
                return NotFound(false);
            } 
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public void DeleteAddress(int id)
        {
            AddressProvider addressProvider = new AddressProvider();
            addressProvider.DeleteAddress(id);
        }



    }
}
