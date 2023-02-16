using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace FlyEatsApp.Models
{
    public class VoucherModel
    {
        public const string TABLE_NAME = "Voucher";
        public const string VOUCHER_ID_COLUMN = "VoucherId";
        public const string VOUCHER_CODE_COLUMN = "VoucherCode";
        public const string VOUCHER_TYPE_COLUMN = "VoucherType";
        public const string DISCOUNT_AMOUNT_COLUMN = "DiscountAmount";
        public const string MAX_REDEMPTION_COLUMN = "MaxRedemption";
        public const string EXPIRY_DATE_COLUMN = "ExpiryDate";
        public const string BUSINESS_ID_COLUMN = "BusinessId";

        public int VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public string VoucherType { get; set; }
        public decimal DiscountAmount { get; set; }
        public int MaxRedemption { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int BusinessId { get; set; }

        public static VoucherModel ExtractObject(DataRow dataRow)
        {
            VoucherModel voucher = new VoucherModel();
            voucher.VoucherId = Convert.ToInt32(dataRow[VOUCHER_ID_COLUMN]);
            voucher.VoucherCode = dataRow[VOUCHER_CODE_COLUMN].ToString();
            voucher.VoucherType = dataRow[VOUCHER_TYPE_COLUMN].ToString();
            voucher.DiscountAmount = Convert.ToDecimal(dataRow[DISCOUNT_AMOUNT_COLUMN]);
            voucher.MaxRedemption = Convert.ToInt32(dataRow[MAX_REDEMPTION_COLUMN]);
            voucher.ExpiryDate = Convert.ToDateTime(dataRow[EXPIRY_DATE_COLUMN]);
            voucher.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            return voucher;
        }


    }
}
