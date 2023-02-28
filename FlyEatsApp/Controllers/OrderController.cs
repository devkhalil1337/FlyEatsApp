using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;
using Stripe;
using FlyEatsApp.Functions;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        [HttpGet]
        public IEnumerable<Order> GetAllOrders()
        {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
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
        public List<string> GetOrderStatusByIds(string[] orderId)
        {
            OrderProvider orderProvider = new OrderProvider();
            var result = orderProvider.GetOrderStatusById(orderId);
            return result;
        }



        [HttpPost]
        public object AddNewOrder([FromBody] Order order)
        {
            OrderProvider orderProvider = new OrderProvider();
            var result = orderProvider.AddNewOrder(order);
            return result;
        }


        [HttpGet]
        public IActionResult GetOrdersByCustomerId(int customerId)
        {
            OrderProvider ordersProvider = new OrderProvider();
            var orders = ordersProvider.GetOrdersByCustomerId(customerId);

            if (orders == null)
            {
                return NotFound(new List<Order>());
            }

            return Ok(orders);
        }

        [HttpPut]
        public object updateOrderStatus(string orderNumber, string orderStatus)
        {
            OrderProvider orderProvider = new OrderProvider();
            Boolean isOrderUpdated = orderProvider.UpdateOrderStatus(orderNumber, orderStatus);
            if (isOrderUpdated)
             return  Ok(true);
            return Ok(false);

        }

        [HttpPost]
        public IActionResult CreatePaymentIntent(decimal amount)
        {
            StripeConfiguration.ApiKey = "sk_test_H97mbpzzO8WxQaLwHjGy9IrE";

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(amount * 100), // Stripe uses smallest currency unit, e.g. cents for USD
                Currency = "usd",
            };

            var service = new PaymentIntentService();
            var paymentIntent = service.Create(options);

            return Json(new { client_secret = paymentIntent.ClientSecret });
        }



    }
}
