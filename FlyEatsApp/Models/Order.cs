using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Order : BaseFilter
    {
        
        public const string ORDER_ID_COLUMN = "OrderId";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string CUSTOMER_ID_COLUMN = "CustomerId";
        public const string ORDER_INVOICE_NUMBER_COLUMN = "OrderInvoiceNumber";
        public const string ORDER_TYPE_COLUMN = "OrderType";
        public const string ORDER_TABLE_ID_COLUMN = "OrderTableId";
        public const string ORDER_STATUS_COLUMN = "OrderStatus";
        public const string SERVICE_CHARGE_AMOUNT_COLUMN = "ServiceChargeAmount";
        public const string DISCOUNT_AMOUNT_COLUMN = "DiscountAmount";
        public const string VOUCHER_ID_COLUMN = "VoucherId";
        public const string VOUCHER_DISCOUNT_AMOUNT_COLUMN = "VoucherDiscountAmount";
        public const string TOTAL_AMOUNT_COLUMN = "TotalAmount";
        public const string VAT_AMOUNT_COLUMN = "VatAmount";
        public const string VAT_PERCENTAGE_COLUMN = "VatPercentage";
        public const string VAT_TYPE_COLUMN = "VatType";
        public const string PAYMENT_STATUS_COLUMN = "PaymentStatus";
        public const string PAYMENT_METHOD_COLUMN = "PaymentMethod";
        public const string AVERAGE_PREPARATION_TIME_COLUMN = "AveragePreparationTime";
        public const string COMMENTS_COLUMN = "Comments";
        public const string DELIVERY_TIME_COLUMN = "DeliveryTime";
        public const string CUSTOMER_DELIVERY_ID_COLUMN = "CustomerDeliveryId";
        public const string COMPLETED_BY_COLUMN = "CompletedBy";
        public const string DELIVERY_CHARGES_COLUMN = "DeliveryCharges";
        public const string CARD_PAYMENT_ID_COLUMN = "CardPaymentId";
        public const string CREATED_DATE_COLUMN = "CreatedDate";
        public const string MODIFIED_DATE_COLUMN = "ModifiedDate";
        public const string IS_DELETED_COLUMN = "IsDeleted";


        public long OrderId { get; set; }
        public int BusinessId { get; set; }
        public int CustomerId { get; set; }
        public string OrderInvoiceNumber { get; set; }
        public string OrderType { get; set; }
        public int OrderTableId { get; set; }
        public string OrderStatus { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public decimal ServiceChargeAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public int VoucherId { get; set; }
        public decimal VoucherDiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal VatPercentage { get; set; }
        public string VatType { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public int AveragePreparationTime { get; set; }
        public string Comments { get; set; }
        public int DeliveryTime { get; set; }
        public int CustomerDeliveryId { get; set; }
        public string CompletedBy { get; set; }
        public decimal DeliveryCharges { get; set; }
        public string CardPaymentId { get; set; }
        public string? CreatedDate { get; set; }
        public string? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }


        public static Order ExtractObject(DataRow dataRow)
        {
            var newObject = new Order();
            newObject.OrderId = Convert.ToInt64(dataRow[ORDER_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            newObject.CustomerId = Convert.ToInt32(dataRow[CUSTOMER_ID_COLUMN]);
            newObject.OrderInvoiceNumber = Convert.ToString(dataRow[ORDER_INVOICE_NUMBER_COLUMN]);
            newObject.OrderType = Convert.ToString(dataRow[ORDER_TYPE_COLUMN]);
            newObject.OrderTableId = Convert.ToInt32(dataRow[ORDER_TABLE_ID_COLUMN]);
            newObject.OrderStatus = Convert.ToString(dataRow[ORDER_STATUS_COLUMN]);
            newObject.ServiceChargeAmount = Convert.ToDecimal(dataRow[SERVICE_CHARGE_AMOUNT_COLUMN]);
            newObject.DiscountAmount = Convert.ToDecimal(dataRow[DISCOUNT_AMOUNT_COLUMN]);
            newObject.VoucherId = Convert.ToInt32(dataRow[VOUCHER_ID_COLUMN]);
            newObject.VoucherDiscountAmount = Convert.ToDecimal(dataRow[VOUCHER_DISCOUNT_AMOUNT_COLUMN]);
            newObject.TotalAmount = Convert.ToDecimal(dataRow[TOTAL_AMOUNT_COLUMN]);
            newObject.VatAmount = Convert.ToDecimal(dataRow[VAT_AMOUNT_COLUMN]);
            newObject.VatPercentage = Convert.ToDecimal(dataRow[VAT_PERCENTAGE_COLUMN]);
            newObject.VatType = Convert.ToString(dataRow[VAT_TYPE_COLUMN]);
            newObject.PaymentStatus = Convert.ToString(dataRow[PAYMENT_STATUS_COLUMN]);
            newObject.PaymentMethod = Convert.ToString(dataRow[PAYMENT_METHOD_COLUMN]);
            newObject.AveragePreparationTime = Convert.ToInt32(dataRow[AVERAGE_PREPARATION_TIME_COLUMN]);
            newObject.Comments = Convert.ToString(dataRow[COMMENTS_COLUMN]);
            newObject.DeliveryTime = Convert.ToInt32(dataRow[DELIVERY_TIME_COLUMN]);
            newObject.CustomerDeliveryId = Convert.ToInt32(dataRow[CUSTOMER_DELIVERY_ID_COLUMN]);
            newObject.CompletedBy = Convert.ToString(dataRow[COMPLETED_BY_COLUMN]);
            newObject.DeliveryCharges = Convert.ToDecimal(dataRow[DELIVERY_CHARGES_COLUMN]);
            newObject.CardPaymentId = Convert.ToString(dataRow[CARD_PAYMENT_ID_COLUMN]);
            newObject.CreatedDate = Convert.ToString(dataRow[CREATED_DATE_COLUMN]);
            newObject.ModifiedDate = Convert.ToString(dataRow[MODIFIED_DATE_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[IS_DELETED_COLUMN]);
            return newObject;
        }




    }
}