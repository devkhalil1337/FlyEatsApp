using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Voucher
    {
        public const string VOUCHER_ID_COLUMN = "VoucherId";
        public const string VOUCHER_CODE_COLUMN = "VoucherCode";
        public const string MIN_VALUE_COLUMN = "MinValue";
        public const string MAX_VALUE_COLUMN = "MaxValue";
        public const string START_DATE_COLUMN = "StartDate";
        public const string END_DATE_COLUMN = "EndDate";
        public const string CREATED_ON_COLUMN = "CreatedOn";
        public const string CREATED_BY_COLUMN = "CreatedBy";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string IS_ACTIVE_COLUMN = "IsActive";
        public const string REDEEM_COUNT_COLUMN = "RedeemCount";

        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int BusinessId { get; set; }
        public bool IsActive { get; set; }
        public int RedeemCount { get; set; }

        public static Voucher ExtractObject(DataRow dataRow)
        {
            var voucher = new Voucher();

            voucher.VoucherId = Convert.ToInt32(dataRow[VOUCHER_ID_COLUMN]);
            voucher.VoucherCode = dataRow[VOUCHER_CODE_COLUMN].ToString();
            voucher.MinValue = Convert.ToDecimal(dataRow[MIN_VALUE_COLUMN]);
            voucher.MaxValue = Convert.ToDecimal(dataRow[MAX_VALUE_COLUMN]);
            voucher.StartDate = Convert.ToDateTime(dataRow[START_DATE_COLUMN]);
            voucher.EndDate = Convert.ToDateTime(dataRow[END_DATE_COLUMN]);
            voucher.CreatedOn = Convert.ToDateTime(dataRow[CREATED_ON_COLUMN]);
            voucher.CreatedBy = Convert.ToInt32(dataRow[CREATED_BY_COLUMN]);
            voucher.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            voucher.IsActive = Convert.ToBoolean(dataRow[IS_ACTIVE_COLUMN]);
            voucher.RedeemCount = Convert.ToInt32(dataRow[REDEEM_COUNT_COLUMN]);

            return voucher;
        }

    }

}
