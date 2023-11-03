using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using FlyEatsApp.Functions;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectionsController : ControllerBase
    {
        BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();

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

        [HttpPost]
        public IEnumerable<Selections> GetMultipleSelectionsById([FromBody]  int[] selectionId)
        {
            SelectionsProvider selectionsProvider = new SelectionsProvider();
            var result = selectionsProvider.GetMultipleSelectionsById(selectionId);
            return result;
        }

        [HttpGet]
        public IEnumerable<Selections> GetAllSelections()
        {
           SelectionsProvider selectionsProvider = new SelectionsProvider();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
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
