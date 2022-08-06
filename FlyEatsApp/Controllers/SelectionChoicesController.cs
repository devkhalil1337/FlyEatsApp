using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SelectionChoicesController : ControllerBase
    {
/*
        [HttpPost]
        public object AddNewSelectionChoices([FromBody] SelectionChoices selectionChoices)
        {
            SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
            var result = selectionChoicesProvider.AddNewSelectionChoices(selectionChoices);
            return result;
        }
*/
       /* [HttpPut]
        public object UpdateSelectionChoices([FromBody] SelectionChoices selectionChoices)
        {
           SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
            var result = selectionChoicesProvider.UpdateSelectionChoices(selectionChoices);
            return result;
        }


        [HttpGet]
        public IEnumerable<SelectionChoices> GetAllSelectionChoices(int selectionId)
        {
           SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
            var result = selectionChoicesProvider.GetAllSelectionChoices(selectionId);
            return result;
        }

        [HttpPost]
        public object DeleteSelectionChoicesById(long choicesId)
        {
           SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
            var result = selectionChoicesProvider.DeleteSelectionChoicesById(choicesId);
            return result;
        }*/
    }
}
