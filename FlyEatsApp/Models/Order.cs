using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Order : BaseFilter
    {
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string CUSTOMER_ID_COLUMN = "CustomerId";
        public const string ORDER_INVOICE_NUMBER_COLUMN = "OrderInvoiceNumber";
        public const string ORDER_TYPE_COLUMN = "OrderType";
        public const string ORDER_TABLE_ID_COLUMN = "OrderTableId";
        public const string ORDER_STATUS_COLUMN = "OrderStatus";
        public const string ORDER_SERVICE_CHARGES_COLUMN = "OrderServiceCharges";
        public const string ORDER_DISCOUNT_COLUMN = "OrderDiscount";
        public const string ORDER_VOUCHER_ID_COLUMN = "OrderVoucherId";
        public const string ORDER_VOUCHER_DISCOUNT_AMOUNT_COLUMN = "OrderVoucherDiscountAmount";
        public const string ORDER_TOTAL_AMOUNT_COLUMN = "OrderTotalAmount";
        public const string ORDER_VAT_AMOUNT_COLUMN = "OrderVatAmount";
        public const string ORDER_VAT_PERCENTAGE_COLUMN = "OrderVatPercentage";
        public const string VAT_TYPE_COLUMN = "VatType";
        public const string ORDER_PAYMENT_STATUS_COLUMN = "OrderPaymentStatus";
        public const string ORDER_PAYMENT_METHOD_COLUMN = "OrderPaymentMethod";
        public const string AVERAGE_ORDER_PREPRATION_TIME_COLUMN = "AverageOrderPreprationTime";
        public const string ORDER_COMMENTS_COLUMN = "OrderComments";
        public const string ORDER_DELIVERY_TIME_COLUMN = "OrderDeliveryTime";
        public const string CUSTOMER_DELIVERY_ID_COLUMN = "CustomerDeliveryId";
        public const string ORDER_COMPLETED_BY_COLUMN = "OrderCompletedBy";
        public const string CREATION_DATE_COLUMN = "CreationDate";
        public const string UPDATE_DATE_COLUMN = "UpdateDate";
        public const string IS_DELETED_COLUMN = "IsDeleted";


        public int? CustomerId { get; set; }
        public string OrderInvoiceNumber { get; set; }
        public string OrderType { get; set; }
        public int? OrderTableId { get; set; }
        public string OrderStatus { get; set; }
        public decimal OrderServiceCharges { get; set; }
        public decimal OrderDiscount { get; set; }
        public int? OrderVoucherId { get; set; }
        public decimal OrderVoucherDiscountAmount { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public decimal OrderVatAmount { get; set; }
        public decimal OrderVatPercentage { get; set; }
        public string VatType { get; set; }
        public string OrderPaymentStatus { get; set; }
        public string OrderPaymentMethod { get; set; }
        public int? AverageOrderPreprationTime { get; set; }
        public string OrderComments { get; set; }
        public int? OrderDeliveryTime { get; set; }
        public int? CustomerDeliveryId { get; set; }
        public string OrderCompletedBy { get; set; }
        public bool IsDeleted { get; set; }



        public static Order ExtractObject(DataRow dataRow)
        {
            var newObject = new Order();
            newObject.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            newObject.CustomerId = Convert.ToInt32(dataRow[CUSTOMER_ID_COLUMN]);
            newObject.OrderInvoiceNumber = Convert.ToString(dataRow[ORDER_INVOICE_NUMBER_COLUMN]);
            newObject.OrderType = Convert.ToString(dataRow[ORDER_TYPE_COLUMN]);
            newObject.OrderTableId = Convert.ToInt32(dataRow[ORDER_TABLE_ID_COLUMN]);
            newObject.OrderStatus = Convert.ToString(dataRow[ORDER_STATUS_COLUMN]);
            newObject.OrderServiceCharges = Convert.ToDecimal(dataRow[ORDER_SERVICE_CHARGES_COLUMN]);
            newObject.OrderDiscount = Convert.ToDecimal(dataRow[ORDER_DISCOUNT_COLUMN]);
            newObject.OrderVoucherId = Convert.ToInt32(dataRow[ORDER_VOUCHER_ID_COLUMN]);
            newObject.OrderVoucherDiscountAmount = Convert.ToDecimal(dataRow[ORDER_VOUCHER_DISCOUNT_AMOUNT_COLUMN]);
            newObject.OrderTotalAmount = Convert.ToDecimal(dataRow[ORDER_TOTAL_AMOUNT_COLUMN]);
            newObject.OrderVatAmount = Convert.ToDecimal(dataRow[ORDER_VAT_AMOUNT_COLUMN]);
            newObject.OrderVatPercentage = Convert.ToDecimal(dataRow[ORDER_VAT_PERCENTAGE_COLUMN]);
            newObject.VatType = Convert.ToString(dataRow[VAT_TYPE_COLUMN]);
            newObject.OrderPaymentStatus = Convert.ToString(dataRow[ORDER_PAYMENT_STATUS_COLUMN]);
            newObject.OrderPaymentMethod = Convert.ToString(dataRow[ORDER_PAYMENT_METHOD_COLUMN]);
            newObject.AverageOrderPreprationTime = Convert.ToInt32(dataRow[AVERAGE_ORDER_PREPRATION_TIME_COLUMN]);
            newObject.OrderComments = Convert.ToString(dataRow[ORDER_COMMENTS_COLUMN]);
            newObject.OrderDeliveryTime = Convert.ToInt32(dataRow[ORDER_DELIVERY_TIME_COLUMN]);
            newObject.CustomerDeliveryId = Convert.ToInt32(dataRow[CUSTOMER_DELIVERY_ID_COLUMN]);
            newObject.OrderCompletedBy = Convert.ToString(dataRow[ORDER_COMPLETED_BY_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[IS_DELETED_COLUMN]);
            return newObject;

        }
    }
}