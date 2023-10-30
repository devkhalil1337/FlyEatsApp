using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class ProductSelection: BaseFilter
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

        public static ProductSelection ExtractObject(DataRow dataRow)
        {
            var newObject = new ProductSelection();
            newObject.ProductSelectionId = Convert.ToInt32(dataRow[PRODUCT_SELECTION_ID_COLUMN]);
            newObject.ProductId = Convert.ToInt32(dataRow[PRODUCT_ID_COLUMN]);
            newObject.SelectionId = Convert.ToInt32(dataRow[SELECTION_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[PRODUCT_SELECTION_BUSINESS_ID_COLUMN]);
            newObject.CreateDate = dataRow[PRODUCT_SELECTION_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_SELECTION_CREATE_DATE_COLUMN]);
            newObject.UpdateDate = dataRow[PRODUCT_SELECTION_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[PRODUCT_SELECTION_UPDATE_DATE_COLUMN]);
           
            return newObject;
        }
    }
}