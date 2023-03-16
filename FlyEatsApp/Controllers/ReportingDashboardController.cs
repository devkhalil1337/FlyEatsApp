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
        [HttpPost]
        public object[] GetNumberOfOrders([FromBody] ReportingDashboardFilter reportingDashboardFilter)
        {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            ReportingDashboardProvider reportingDashboardProvider = new ReportingDashboardProvider();
            var result = reportingDashboardProvider.GetNumberOfOrders(businessId, reportingDashboardFilter.orderStatus, reportingDashboardFilter.startDate, reportingDashboardFilter.endDate);
            return result;
        }

        [HttpPost]
        public List<object> GetGrossSalesByDay([FromBody] ReportingDashboardFilter reportingDashboardFilter)
        {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            ReportingDashboardProvider reportingDashboardProvider = new ReportingDashboardProvider();
            var result = reportingDashboardProvider.GetGrossSalesByDay(businessId, reportingDashboardFilter.startDate, reportingDashboardFilter.endDate);
            return result;
        }
    }
}
