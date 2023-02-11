using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class OrderDetails : BaseFilter
    {
        public const string ORDER_DETAILS_ID_COLUMN = "OrderDetailsId";
        public const string ORDER_ID_COLUMN = "OrderId";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string CATEGORY_ID_COLUMN = "CategoryId";
        public const string PRODUCT_ID_COLUMN = "ProductId";
        public const string VARIANT_ID_COLUMN = "VariantId";
        public const string PRODUCT_NAME_COLUMN = "ProductName";
        public const string PRODUCT_QUANTITY_COLUMN = "ProductQuantity";
        public const string PRODUCT_PRICE_COLUMN = "ProductPrice";
        public const string PRODUCT_COMMENTS_COLUMN = "ProductComments";
        public const string PRODUCT_HAVE_SELECTION_COLUMN = "ProductHaveSelection";

        public int OrderDetailsId { get; set; }
        public string OrderId { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public int VariantId { get; set; }

        public string ProductName { get; set; }

        public int ProductQuantity { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductComments { get; set; }

        public bool ProductHaveSelection { get; set; }

        public List<OrderDetailSelectionRelation>? productVariants { get;set;}

        public static OrderDetails ExtractObject(DataRow dataRow)
        {
            var order = new OrderDetails();
            order.OrderDetailsId = Convert.ToInt32(dataRow[ORDER_DETAILS_ID_COLUMN]);
            order.OrderId = Convert.ToString(dataRow[ORDER_ID_COLUMN]);
            order.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            order.CategoryId = Convert.ToInt32(dataRow[CATEGORY_ID_COLUMN]);
            order.ProductId = Convert.ToInt32(dataRow[PRODUCT_ID_COLUMN]);
            order.VariantId = Convert.ToInt32(dataRow[VARIANT_ID_COLUMN]);
            order.ProductName = Convert.ToString(dataRow[PRODUCT_NAME_COLUMN]);
            order.ProductQuantity = Convert.ToInt32(dataRow[PRODUCT_QUANTITY_COLUMN]);
            order.ProductPrice = Convert.ToDecimal(dataRow[PRODUCT_PRICE_COLUMN]);
            order.ProductComments = Convert.ToString(dataRow[PRODUCT_COMMENTS_COLUMN]);
            order.ProductHaveSelection = Convert.ToBoolean(dataRow[PRODUCT_HAVE_SELECTION_COLUMN]);

            return order;
        }


    }
}
