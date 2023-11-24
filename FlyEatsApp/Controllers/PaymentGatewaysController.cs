using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using FlyEatsApp.Functions;
using Stripe;
using log4net;
using log4net.Core;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentGatewaysController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(PaymentGatewaysController));


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


        [HttpPost]
        public IActionResult CreatePaymentIntent([FromBody] PaymentCharge paymentCharge)
        {
            try
            {
                PaymentGateway stripeGateway = GetPaymentGatewaysByBusinessId().FirstOrDefault(pg => pg.GatewayName == "stripe");
                if (stripeGateway == null)
                {
                    return BadRequest(new { success = false, message = "No Payment configurations found" });
                }
                StripeConfiguration.ApiKey = stripeGateway.ApiSecret;

                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)(paymentCharge.amount * 100), // Stripe uses smallest currency unit, e.g. cents for USD
                    Currency = "gbp",
                };

                var service = new PaymentIntentService();
                var paymentIntent = service.Create(options);

                return Json(new { client_secret = paymentIntent.ClientSecret, success = true });
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(PaymentGatewaysController), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to create payment intent : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                return BadRequest(new { success = false, message = "An error occurred while processing the payment." });
            }
        }




    }
}
