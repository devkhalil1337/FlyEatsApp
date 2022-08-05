using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductSelectionController : ControllerBase
    {
        /*
        [HttpPost]
        public object AddNewProductSelection( [FromBody] ProductSelection productSelections)
        {
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            var result = productSelection.AddNewProductSelection(productSelections);
            return result;
        }
        */

       /* [HttpPut]
        public object UpdateProductSelection([FromBody] ProductSelection productSelections)
        {
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            var result = productSelection.UpdateProductSelection(productSelections);
            return result;
        }*/


        [HttpGet]
        public IEnumerable<ProductSelection> GetAllProductSelection(int productId)
        {
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            var result = productSelection.GetAllProductSelection(productId);
            return result;
        }

        [HttpPost]
        public object DeleteProductSelectionBy(long productSelectionId)
        {
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            var result = productSelection.DeleteProductSelectionBy(productSelectionId);
            return result;
        }
    }
}
