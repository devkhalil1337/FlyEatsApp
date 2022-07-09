using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectionsController : ControllerBase
    {

        [HttpPost]
        public object AddNewSelections([FromBody] Selections selections)
        {
            SelectionsProvider selectionsProvider = new SelectionsProvider();
            var result = selectionsProvider.AddNewSelections(selections);
            return result;
        }

        [HttpPut]
        public object UpdateSelections([FromBody]  Selections selections)
        {
           SelectionsProvider selectionsProvider = new SelectionsProvider();
            var result = selectionsProvider.UpdateSelections(selections);
            return result;
        }

        [HttpGet]
        public IEnumerable<Selections> GetSelectionsById(int selectionId)
        {
           SelectionsProvider selectionsProvider = new SelectionsProvider();
            var result = selectionsProvider.GetSelectionsById(selectionId);
            return result;
        }

        [HttpGet]
        public IEnumerable<Selections> GetAllSelections(int businessId)
        {
           SelectionsProvider selectionsProvider = new SelectionsProvider();
            var result = selectionsProvider.GetAllSelections(businessId);
            return result;
        }

        [HttpPost]
        public object DeleteSelectionsBy(long selectionId)
        {
           SelectionsProvider selectionsProvider = new SelectionsProvider();
            var result = selectionsProvider.DeleteSelectionsBy(selectionId);
            return result;
        }
    }
}
