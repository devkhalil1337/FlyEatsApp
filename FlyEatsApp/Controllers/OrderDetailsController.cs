using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderDetailsController : Controller
    {
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
            var result = orderProvider.AddNewOrderDetails(order);
            return result;
        }
    }
}
