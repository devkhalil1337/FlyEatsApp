using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;
using Stripe;
using FlyEatsApp.Functions;
using FlyEatsApp.Payloads;
using log4net;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : Controller
    {
        BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();

        [HttpPost]
        public IEnumerable<Order> GetAllOrders(OrderPayload orderPayload)
        {
            OrderProvider orderProvider = new OrderProvider();
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            orderPayload.BusinessId = businessId;
            var result = orderProvider.GetAllOrders(orderPayload);
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
            order.BusinessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
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
            var result = orderProvider.UpdateOrderStatus(orderNumber, orderStatus);
            if (result.success)
                return Ok(result);
            return BadRequest(result);

        }



        /* Front-end and Manager app API*/

        [HttpGet]
        public object GetOrderDetailedByOrderId(string orderId)
        {
            OrderProvider orderProvider = new OrderProvider();
            var result = orderProvider.GetOrderDetailedByOrderId(orderId);
            return result;
        }

        [HttpPost]
        public int SyncOrdersCount(OrderPayload orderPayload)
        {
            OrderProvider orderProvider = new OrderProvider();
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            orderPayload.BusinessId = businessId;
            var result = orderProvider.SyncOrdersCount(orderPayload);
            return result;
        }


    }
}
