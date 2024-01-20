using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Categories : BaseFilter
    {
        public const string Category_ID_COLUMN = "CategoryId";
        public const string CATEGORY_BUSINESS_ID_COLUMN = "BusinessId";
        public const string CATEGORY_IMAGE_COLUMN = "CategoryImage";
        public const string CATEGORY_NAME_COLUMN = "CategoryName";
        public const string CATEGORY_DETAIL_COLUMN = "CategoryDetails";
        public const string CATEGORY_SORT_BY_COLUMN = "CategorySortBy";
        public const string CATEGORY_CREATE_DATE_COLUMN = "CreationDate";
        public const string CATEGORY_UPDATE_DATE_COLUMN = "UpdateDate";
        public const string CATEGORY_DELETE_COLUMN = "IsDeleted";
        public const string CATEGORY_ACTIVE_COLUMN = "Active";
        public const string CATEGORY_TYPE_COLUMN = "categoryType";

        public int? CategoryId { get; set; }
        public string? CategoryImage { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDetails { get; set; }
        public int? CategorySortBy { get; set; }
        public string? CategoryType { get; set; }

        public static Categories ExtractObject(DataRow dataRow)
        {
            var newObject = new Categories();
            newObject.CategoryId = Convert.ToInt32(dataRow[Category_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[CATEGORY_BUSINESS_ID_COLUMN]);
            newObject.CategoryImage = Convert.ToString(dataRow[CATEGORY_IMAGE_COLUMN]);
            newObject.CategoryName = Convert.ToString(dataRow[CATEGORY_NAME_COLUMN]);
            newObject.CategoryDetails = Convert.ToString(dataRow[CATEGORY_DETAIL_COLUMN]);
            newObject.CategoryType = Convert.ToString(dataRow[CATEGORY_TYPE_COLUMN]);
            newObject.CategorySortBy = Convert.ToInt32(dataRow[CATEGORY_SORT_BY_COLUMN]);
            newObject.CreateDate = dataRow[CATEGORY_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[CATEGORY_CREATE_DATE_COLUMN]);
            newObject.UpdateDate = dataRow[CATEGORY_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[CATEGORY_UPDATE_DATE_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[CATEGORY_DELETE_COLUMN]);
            newObject.Active = Convert.ToBoolean(dataRow[CATEGORY_ACTIVE_COLUMN]);

            return newObject;
        }
    }
}