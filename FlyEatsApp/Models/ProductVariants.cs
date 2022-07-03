using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class ProductVariants
    {
        public const string PRODUCT_VARIANT_ID_COLUMN = "VariantId";
        public const string PRODUCT_ID_COLUMN = "ProductId";
        public const string PRODUCT_VARIANT_BUSINESS_ID_COLUMN = "BusinessId";
        public const string PRODUCT_VARIANT_NAME_COLUMN = "VariationName";
        public const string PRODUCT_VARIANT_PRICE_COLUMN = "VariationPrice";
        public const string PRODUCT_VARIANT_CREATE_DATE_COLUMN = "CreationDate";
        public const string PRODUCT_VARIANT_UPDATE_DATE_COLUMN = "UpdateDate";
        public const string PRODUCT_VARIANT_DELETE_COLUMN = "IsDeleted";
        public const string PRODUCT_VARIANT_ACTIVE_COLUMN = "Active";

        public int? VariantId { get; set; }
        public int? ProductId { get; set; }
        public int? BusinessId { get; set; }
        public string? VariationName { get; set; }
        public double? VariationPrice { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreationDate
    {
            get {
                return CreateDate.HasValue ? CreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : null;
            }

            set
            {
                if (value != null)
                {
                    CreateDate = DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public DateTime? ModifyDate { get; set; }
        public string? UpdateDate
    {
            get { return ModifyDate.HasValue ? ModifyDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : null; }

            set
            {
                if (value != null)
                {
                    ModifyDate = DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public bool? IsDeleted { get; set; }
        public bool? Active { get; set; }


        public static ProductVariants ExtractObject(DataRow dataRow)
        {
            var newObject = new ProductVariants();
            newObject.VariantId = Convert.ToInt32(dataRow[PRODUCT_VARIANT_ID_COLUMN]);
            newObject.ProductId = Convert.ToInt32(dataRow[PRODUCT_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[PRODUCT_VARIANT_BUSINESS_ID_COLUMN]);
            newObject.VariationName = Convert.ToString(dataRow[PRODUCT_VARIANT_NAME_COLUMN]);
            newObject.VariationPrice = Convert.ToDouble(dataRow[PRODUCT_VARIANT_PRICE_COLUMN]);
            newObject.CreateDate = dataRow[PRODUCT_VARIANT_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_VARIANT_CREATE_DATE_COLUMN]);
            newObject.ModifyDate = dataRow[PRODUCT_VARIANT_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_VARIANT_UPDATE_DATE_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[PRODUCT_VARIANT_DELETE_COLUMN]);
            newObject.Active = Convert.ToBoolean(dataRow[PRODUCT_VARIANT_ACTIVE_COLUMN]);
            
            return newObject;
        }
    }   
}       