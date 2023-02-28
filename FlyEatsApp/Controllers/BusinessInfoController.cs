using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using FlyEatsApp.Functions;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BusinessInfoController : ControllerBase
    {

        [HttpPost]
        public long AddNewBusinessUnit(BusinessInfo businessInfo)
        {
            BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
            var result = businessInfoProvider.AddNewBusinessUnit(businessInfo);
            return result;
        }

       [HttpPost]
        public object UpdateBusinessUnit(BusinessInfo businessInfo)
        {
            BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
            var result = businessInfoProvider.UpdateBusinessUnit(businessInfo);
            return result;
        }

        [HttpGet]
       public IEnumerable<BusinessInfo> GetBusinessUnitById()
       {
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
           var result = businessInfoProvider.GetBusinessUnitById(businessId);
           return result;
       }

       [HttpDelete]
       public Boolean DeleteBusinessUnit(long BusinessId)
       {
           BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
           var result = businessInfoProvider.DeleteBusinessUnit(BusinessId);
           return result;
       }
    }
}
