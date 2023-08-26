using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using FlyEatsApp.Functions;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
        [HttpGet]
        public IActionResult GetMenusByBusinessId()
        {
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            MenusProvider menusProvider = new MenusProvider();
            var results = menusProvider.GetAllMenus(businessId);
            return Ok(results);
        }

        [HttpPost]
        public IActionResult AddNewMenu([FromBody] Menus menu)
        {
            MenusProvider menusProvider = new MenusProvider();
            var result = menusProvider.AddNewMenu(menu);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateMenu([FromBody] Menus menu)
        {
            MenusProvider menusProvider = new MenusProvider();
            var result = menusProvider.UpdateMenu(menu);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateBulkMenus([FromBody] List<Menus> menus)
        {
            MenusProvider menusProvider = new MenusProvider();
            var results = menusProvider.UpdateBulkMenus(menus);
            return Ok(results);
        }


        [HttpGet("{id}")]
        public IActionResult GetMenuById(int id)
        {
            MenusProvider menusProvider = new MenusProvider();
            var result = menusProvider.GetMenuById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMenu(int id)
        {
            MenusProvider menusProvider = new MenusProvider();
            var result = menusProvider.DeleteMenuById(id);
            if (result == null)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
