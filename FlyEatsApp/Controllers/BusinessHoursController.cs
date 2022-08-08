using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BusinessHoursController : Controller
    {
        [HttpGet]
        public IEnumerable<BusinessHours> GetBusinessHours(int businessId)
        {
            BusinessHoursProvider businessHoursProvider = new BusinessHoursProvider();
            var result = businessHoursProvider.GetBusinessHours(businessId);
            return result;
        }

        [HttpPost]
        public object UpdateBusinessHours(List<BusinessHours> businessHours)
        {
            BusinessHoursProvider businessHoursProvider = new BusinessHoursProvider();
            var result = businessHoursProvider.UpdateBusinessHours(businessHours);
            return result;
        }
    }
}
