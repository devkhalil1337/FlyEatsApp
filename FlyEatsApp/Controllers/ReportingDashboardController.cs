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
    public class ReportingDashboardController : Controller
    {
        [HttpGet]
        public int GetNumberOfOrders(string orderStatus, string Datefrom, string Dateto)
        {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            ReportingDashboardProvider reportingDashboardProvider = new ReportingDashboardProvider();
            var result = reportingDashboardProvider.GetNumberOfOrders(businessId,orderStatus, Datefrom, Dateto);
            return result;
        }
    }
}
