using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;
using FlyEatsApp.Functions;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailsController : Controller
    {
        BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();

        [HttpGet]
        public IEnumerable<OrderDetails> GetProductsById(string OrderId)
        {
            OrderDetailsProvider productsProvider = new OrderDetailsProvider();
            var result = productsProvider.GetOrderById(OrderId);
            return result;
        }

        [HttpPost]
        public object AddNewOrderDetails([FromBody] OrderDetails[] order)
        {
            OrderDetailsProvider orderProvider = new OrderDetailsProvider();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            var result = orderProvider.AddNewOrderDetails(order, businessId);
            return result;
        }
    }
}
