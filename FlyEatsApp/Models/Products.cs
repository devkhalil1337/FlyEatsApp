using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Products: BaseFilter
    {
        public const string PRODUCT_ID_COLUMN = "ProductId";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string CATEGORY_ID_COLUMN = "CategoryId";
        public const string CATEGORY_NAME_COLUMN = "CategoryName";
        public const string PRODUCT_NAME_COLUMN = "ProductName";
        public const string PRODUCT_DESCRIPTION_COLUMN = "ProductDescription";
        public const string PRODUCT_IMAGE_COLUMN = "ProductImage";
        public const string PRODUCT_SORT_ORDER_COLUMN = "ProductSortOrder";
        public const string PRODUCT_QUANTITY_COLUMN = "ProductQuantity";
        public const string IS_TABLE_PRODUCT_COLUMN = "IsTableProduct";
        public const string TABLE_PRICE_COLUMN = "TablePrice";
        public const string TABLE_VAT_COLUMN = "TableVat";
        public const string IS_PICKUP_PRODUCT_COLUMN = "IsPickupProduct";
        public const string PICKUP_PRICE_COLUMN = "PickupPrice";
        public const string PICKUP_VAT_COLUMN = "PickupVat";
        public const string IS_DELIVERY_PRODUCT_COLUMN = "IsDeliveryProduct";
        public const string DELIVERY_PRICE_COLUMN = "DeliveryPrice";
        public const string DELIVERY_VAT_COLUMN = "DeliveryVat";
        public const string HAS_VARIATIONS_COLUMN = "HasVariations";
        public const string FEATURED_COLUMN = "Featured";
        public const string CREATION_DATE_COLUMN = "CreationDate";
        public const string MODIFIED_DATE_COLUMN = "ModifiedDate";
        public const string IS_DELETED_COLUMN = "IsDeleted";
        public const string ACTIVE_COLUMN = "Active";
        public const string IS_POPULAR_COLUMN = "IsPopular";

        public int? ProductId { get; set; }
        public int BusinessId { get; set; }
        public int CategoryId { get; set; }
        public int[]? selectionId { get; set; }
        public string? CategoryName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImage { get; set; }
        public int ProductSortOrder { get; set; }
        public int ProductQuantity { get; set; }
        public bool IsTableProduct { get; set; }
        public decimal TablePrice { get; set; }
        public decimal TableVat { get; set; }
        public bool IsPickupProduct { get; set; }
        public decimal PickupPrice { get; set; }
        public decimal PickupVat { get; set; }
        public bool IsDeliveryProduct { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal DeliveryVat { get; set; }
        public bool HasVariations { get; set; }
        public bool Featured { get; set; }
        public int? Quantity { get; set; }
        public bool IsPopular { get; set; }

        public decimal? productPrice { get; set; }
        public List<ProductVariants>? productVariants { get; set; }

       

        public static Products ExtractObject(DataRow dataRow)
        {
            var newObject = new Products();
            newObject.ProductId = Convert.ToInt32(dataRow[PRODUCT_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            newObject.CategoryId = Convert.ToInt32(dataRow[CATEGORY_ID_COLUMN]);
            newObject.CategoryName = Convert.ToString(dataRow[CATEGORY_NAME_COLUMN]);
            newObject.ProductName = Convert.ToString(dataRow[PRODUCT_NAME_COLUMN]);
            newObject.ProductDescription = Convert.ToString(dataRow[PRODUCT_DESCRIPTION_COLUMN]);
            newObject.ProductImage = Convert.ToString(dataRow[PRODUCT_IMAGE_COLUMN]);
            newObject.ProductSortOrder = Convert.ToInt32(dataRow[PRODUCT_SORT_ORDER_COLUMN]);
            newObject.ProductQuantity = Convert.ToInt32(dataRow[PRODUCT_QUANTITY_COLUMN]);
            newObject.IsTableProduct = Convert.ToBoolean(dataRow[IS_TABLE_PRODUCT_COLUMN]);
            newObject.TablePrice = Convert.ToDecimal(dataRow[TABLE_PRICE_COLUMN]);
            newObject.TableVat = Convert.ToDecimal(dataRow[TABLE_VAT_COLUMN]);
            newObject.IsPickupProduct = Convert.ToBoolean(dataRow[IS_PICKUP_PRODUCT_COLUMN]);
            newObject.PickupPrice = Convert.ToDecimal(dataRow[PICKUP_PRICE_COLUMN]);
            newObject.PickupVat = Convert.ToDecimal(dataRow[PICKUP_VAT_COLUMN]);
            newObject.IsDeliveryProduct = Convert.ToBoolean(dataRow[IS_DELIVERY_PRODUCT_COLUMN]);
            newObject.DeliveryPrice = Convert.ToDecimal(dataRow[DELIVERY_PRICE_COLUMN]);
            newObject.DeliveryVat = Convert.ToDecimal(dataRow[DELIVERY_VAT_COLUMN]);
            newObject.HasVariations = Convert.ToBoolean(dataRow[HAS_VARIATIONS_COLUMN]);
            newObject.Featured = Convert.ToBoolean(dataRow[FEATURED_COLUMN]);
            newObject.CreateDate = dataRow[CREATION_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[CREATION_DATE_COLUMN]);
            newObject.ModifyDate = dataRow[MODIFIED_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[MODIFIED_DATE_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[IS_DELETED_COLUMN]);
            newObject.Active = Convert.ToBoolean(dataRow[ACTIVE_COLUMN]);
            newObject.IsPopular = Convert.ToBoolean(dataRow[ACTIVE_COLUMN]);
            newObject.Quantity = 1;
            return newObject;
        }
    }   
}       