using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using System.Net.Http.Headers;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VoucherModelController : Controller
    {
        [HttpGet]
        public ActionResult<VoucherModel> GetVoucherById(int voucherId)
        {
            VoucherModelProvider voucherProvider = new VoucherModelProvider();
            VoucherModel voucher = voucherProvider.GetVoucherById(voucherId);

            if (voucher == null)
            {
                return NotFound();
            }

            return Ok(voucher);
        }

        [HttpGet]
        public ActionResult<IList<VoucherModel>> GetVouchersByBusinessId(int businessId)
        {
            VoucherModelProvider voucherProvider = new VoucherModelProvider();
            IList<VoucherModel> vouchers = voucherProvider.GetVouchers(businessId);

            if (vouchers == null || vouchers.Count == 0)
            {
                return NotFound();
            }

            return Ok(vouchers);
        }

        [HttpPost]
        public ActionResult<VoucherModel> CreateVoucher([FromBody] VoucherModel voucher)
        {
            VoucherModelProvider voucherProvider = new VoucherModelProvider();
            VoucherModel newVoucher = voucherProvider.AddVoucher(voucher);

            if (newVoucher == null)
            {
                return BadRequest();
            }

            return Ok(newVoucher);
        }

        [HttpPut]
        public ActionResult<VoucherModel> UpdateVoucher([FromBody] VoucherModel voucher)
        {
            VoucherModelProvider voucherProvider = new VoucherModelProvider();
            VoucherModel updatedVoucher = voucherProvider.UpdateVoucher(voucher);

            if (updatedVoucher == null)
            {
                return NotFound();
            }

            return Ok(updatedVoucher);
        }

        [HttpDelete]
        public ActionResult DeleteVoucher(int voucherId)
        { VoucherModelProvider voucherProvider = new VoucherModelProvider();
            bool deleted = voucherProvider.DeleteVoucher(voucherId);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
