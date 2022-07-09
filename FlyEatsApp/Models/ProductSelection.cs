using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class ProductSelection
    {
        public const string PRODUCT_SELECTION_ID_COLUMN = "ProductSelectionId";
        public const string PRODUCT_ID_COLUMN = "ProductId";
        public const string SELECTION_ID_COLUMN = "SelectionId";
        public const string PRODUCT_SELECTION_BUSINESS_ID_COLUMN = "BusinessId";
        public const string PRODUCT_SELECTION_CREATE_DATE_COLUMN = "CreationDate";
        public const string PRODUCT_SELECTION_UPDATE_DATE_COLUMN = "UpdateDate";
   

        public int? ProductSelectionId { get; set; }
        public int? ProductId { get; set; }
        public int? SelectionId { get; set; }
        public int? BusinessId { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? CreationDate
        {
            get
            {
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

        public static ProductSelection ExtractObject(DataRow dataRow)
        {
            var newObject = new ProductSelection();
            newObject.ProductSelectionId = Convert.ToInt32(dataRow[PRODUCT_SELECTION_ID_COLUMN]);
            newObject.ProductId = Convert.ToInt32(dataRow[PRODUCT_ID_COLUMN]);
            newObject.SelectionId = Convert.ToInt32(dataRow[SELECTION_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[PRODUCT_SELECTION_BUSINESS_ID_COLUMN]);
            newObject.CreateDate = dataRow[PRODUCT_SELECTION_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_SELECTION_CREATE_DATE_COLUMN]);
            newObject.ModifyDate = dataRow[PRODUCT_SELECTION_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_SELECTION_UPDATE_DATE_COLUMN]);
           
            return newObject;
        }
    }
}