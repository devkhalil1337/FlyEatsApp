using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherController : Controller
    {
        // Add a new voucher
        [HttpPost]
        public object AddNewVoucher([FromBody] Voucher voucher)
        {
            VoucherProvider voucherProvider = new VoucherProvider();
            var result = voucherProvider.AddVoucher(voucher);
            return result;
        }

        
        // Get a voucher by its ID
        [HttpGet]
        public object GetVoucherById(int voucherId)
        {
            VoucherProvider voucherProvider = new VoucherProvider();
            var voucher = voucherProvider.GetVoucherById(voucherId);
            return voucher;
        }

        // Get a voucher by its ID
        [HttpGet]
        public object CheckVoucherRedemptionEligibility(string voucherCode, int userId,decimal billAmount)
        {
            VoucherProvider voucherProvider = new VoucherProvider();
            int voucherId = voucherProvider.CheckVoucherRedemptionEligibility(voucherCode, userId,billAmount);
            if (voucherId == 2)
            {
                return voucherProvider.GetVoucherIdByCode(voucherCode);
            }else if(voucherId == 1)
            {
                NotFound("Amount is less than expected amount");
            }
            else
            {
                NotFound("You are not eligible to use this voucher");
            }
            return NotFound("Invalid Voucher");
        }

        // Get a voucher by its ID
        [HttpGet("{businessId}")]
        public object GetAllVouchersByBusinessId(int businessId)
        {
            VoucherProvider voucherProvider = new VoucherProvider();
            var voucher = voucherProvider.GetAllVouchersByBusinessId(businessId);
            return voucher;
        }

        // Update an existing voucher
        [HttpPut]
        public object UpdateVoucher([FromBody] Voucher voucher)
        {
            VoucherProvider voucherProvider = new VoucherProvider();
            var result = voucherProvider.UpdateVoucher(voucher);
            return result;
        }

        // Delete a voucher by its ID
        [HttpDelete]
        public object DeleteVoucher(int voucherId)
        {
            VoucherProvider voucherProvider = new VoucherProvider();
            var result = voucherProvider.DeleteVoucher(voucherId);
            return result;
        }

        // Delete a voucher by its ID
        [HttpGet]
        public void GetVoucherIdByCode(string voucherId)
        {
            VoucherProvider voucherProvider = new VoucherProvider();
            voucherProvider.GetVoucherIdByCode(voucherId);
           
        }
    }
}
