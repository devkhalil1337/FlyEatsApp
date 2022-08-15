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
        public const string PRODUCT_CATEGORY_ID_COLUMN = "CategoryId";
        public const string PRODUCT_BUSINESS_ID_COLUMN = "BusinessId";
        public const string CATEGORY_NAME_COLUMN = "CategoryName";
        public const string PRODUCT_IMAGE_COLUMN = "ProductImage";
        public const string PRODUCT_NAME_COLUMN = "ProductName";
        public const string PRODUCT_DESCRITPTION_COLUMN = "ProductDescription";
        public const string PRODUCT_TABLE_PRCIE_COLUMN = "ProductTablePrice";
        public const string PRODUCT_TABLE_VAT_COLUMN = "ProductTableVat";
        public const string PRODUCT_PICKUP_PRCIE_COLUMN = "ProductPickupPrice";
        public const string PRODUCT_PICKUP_VAT_COLUMN = "ProductPickupVat";
        public const string PRODUCT_DELIVERY_PRCIE_COLUMN = "ProductDeliveryPrice";
        public const string PRODUCT_DELIVERY_VAT_COLUMN = "ProductDeliveryVat";
        public const string PRODUCT_SORT_BY_COLUMN = "ProductSortBy";
        public const string PRODUCT_QUANTITY_COLUMN = "ProductQuantity";
        public const string PRODUCT_HAS_VARIATION_COLUMN = "HasVariations";
        public const string PRODUCT_FEATURED_COLUMN = "Featured";
        public const string PRODUCT_CREATE_DATE_COLUMN = "CreationDate";
        public const string PRODUCT_UPDATE_DATE_COLUMN = "UpdateDate";
        public const string PRODUCT_DELETE_COLUMN = "IsDeleted";
        public const string PRODUCT_ACTIVE_COLUMN = "Active";

        public int? ProductId { get; set; }
        public int? CategoryId { get; set; }
        public int[]? selectionId { get; set; }
        public string? CategoryName { get; set; }
        public string? ProductImage { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public double? ProductTablePrice { get; set; }
        public double? ProductTableVat { get; set; }
        public double? ProductPickupPrice { get; set; }
        public double? ProductPickupVat { get; set; }
        public double? ProductDeliveryPrice { get; set; }
        public double? ProductDeliveryVat { get; set; }
        public int? ProductSortBy { get; set; }
        public int? ProductQuantity { get; set; }
        public bool? HasVariations { get; set; }
        public bool? Featured { get; set; }
        public int? Quantity { get; set; }

        public List<ProductVariants>? productVariants { get; set; }

        public static Products ExtractObject(DataRow dataRow)
        {
            var newObject = new Products();
            newObject.ProductId = Convert.ToInt32(dataRow[PRODUCT_ID_COLUMN]);
            newObject.CategoryId = Convert.ToInt32(dataRow[PRODUCT_CATEGORY_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[PRODUCT_BUSINESS_ID_COLUMN]);
            newObject.CategoryName = Convert.ToString(dataRow[CATEGORY_NAME_COLUMN]);
            newObject.ProductImage = Convert.ToString(dataRow[PRODUCT_IMAGE_COLUMN]);
            newObject.ProductName = Convert.ToString(dataRow[PRODUCT_NAME_COLUMN]);
            newObject.ProductDescription = Convert.ToString(dataRow[PRODUCT_DESCRITPTION_COLUMN]);
            newObject.ProductTablePrice = Convert.ToDouble(dataRow[PRODUCT_TABLE_PRCIE_COLUMN]);
            newObject.ProductTableVat = Convert.ToDouble(dataRow[PRODUCT_TABLE_VAT_COLUMN]);
            newObject.ProductPickupPrice = Convert.ToDouble(dataRow[PRODUCT_PICKUP_PRCIE_COLUMN]);
            newObject.ProductPickupVat = Convert.ToDouble(dataRow[PRODUCT_PICKUP_VAT_COLUMN]);
            newObject.ProductDeliveryPrice = Convert.ToDouble(dataRow[PRODUCT_DELIVERY_PRCIE_COLUMN]);
            newObject.ProductDeliveryVat = Convert.ToDouble(dataRow[PRODUCT_DELIVERY_VAT_COLUMN]);
            newObject.ProductSortBy = Convert.ToInt32(dataRow[PRODUCT_SORT_BY_COLUMN]);
            newObject.ProductQuantity = Convert.ToInt32(dataRow[PRODUCT_QUANTITY_COLUMN]);
            newObject.HasVariations = Convert.ToBoolean(dataRow[PRODUCT_HAS_VARIATION_COLUMN]);
            newObject.Featured = Convert.ToBoolean(dataRow[PRODUCT_FEATURED_COLUMN]);
            newObject.CreateDate = dataRow[PRODUCT_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_CREATE_DATE_COLUMN]);
            newObject.ModifyDate = dataRow[PRODUCT_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_UPDATE_DATE_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[PRODUCT_DELETE_COLUMN]);
            newObject.Active = Convert.ToBoolean(dataRow[PRODUCT_ACTIVE_COLUMN]);
            newObject.Quantity = 1;
            return newObject;
        }
    }   
}       