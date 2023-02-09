using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        [HttpGet]
        public IEnumerable<Order> GetAllProducts(int businessId)
        {
            OrderProvider orderProvider = new OrderProvider();
            var result = orderProvider.GetAllOrders(businessId);
            return result;
        }

        [HttpGet]
        public Order GetOrderById(string orderId)
        {
            OrderProvider orderProvider = new OrderProvider();
            var result = orderProvider.GetOrderById(orderId);
            return result;
        }

        [HttpPost]
        public object AddNewOrder([FromBody] Order order)
        {
            OrderProvider orderProvider = new OrderProvider();
            var result = orderProvider.AddNewOrder(order);
            return result;
        }


    }
}
