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
        public object AddNewBusinessUnit(BusinessInfo businessInfo)
        {
  //          BusinessHoursProvider businessHoursProvider = new BusinessHoursProvider();
            BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
            var result = businessInfoProvider.AddNewBusinessUnit(businessInfo);
//            businessHoursProvider.AddDefaultBusinessDays((int)businessInfo.BusinessId);
            if (!result.success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

       [HttpPost]
        public object UpdateBusinessUnit(BusinessInfo businessInfo)
        {
            BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
            var result = businessInfoProvider.UpdateBusinessUnit(businessInfo);
            if (!result.success)
            {
                return BadRequest(result);
            }
            return Ok(result);
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

        [HttpGet]
        public IEnumerable<BusinessInfo> GetAllBusinesses()
        {
            BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
            var result = businessInfoProvider.GetAllBusinesUnits();
            return result;
        }

        [HttpDelete]
       public object DeleteBusinessUnit(long BusinessId)
       {
           BusinessInfoProvider businessInfoProvider = new BusinessInfoProvider();
           var result = businessInfoProvider.DeleteBusinessUnit(BusinessId);
            if (!result.success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
