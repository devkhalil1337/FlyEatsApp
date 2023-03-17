using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using FlyEatsApp.Functions;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentGatewaysController : Controller
    {
        //Client Side
        [HttpGet]
        public IEnumerable<PaymentGatewayKeys> GetPaymentGatewaysKeysByBusinessId()
        {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            PaymentGatewayProvider paymentGatewayProvider = new PaymentGatewayProvider();
            var result = paymentGatewayProvider.GetPaymentGatewaysKeys(businessId);
            return result;
        }
        [HttpGet]
        public IEnumerable<PaymentGateway> GetPaymentGatewaysByBusinessId()
        {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            PaymentGatewayProvider paymentGatewayProvider = new PaymentGatewayProvider();
            var result = paymentGatewayProvider.GetAllPaymentGateways(businessId);
            return result;
        }

        [HttpPost]
        public IActionResult InsertPaymentGateway([FromBody] PaymentGateway paymentGateway)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input.");
            }

            PaymentGatewayProvider paymentGatewayProvider = new PaymentGatewayProvider();
            bool success = paymentGatewayProvider.AddPaymentGateway(paymentGateway);

            if (!success)
            {
                return StatusCode(500, "Error inserting payment gateway.");
            }

            return Ok("Payment gateway inserted successfully.");
        }

        [HttpPut]
        public bool UpdatePaymentGateway(PaymentGateway paymentGateway)
        {
            PaymentGatewayProvider provider = new PaymentGatewayProvider();
            bool success = provider.UpdatePaymentGateway(paymentGateway);
            return success;
        }



    }
}
