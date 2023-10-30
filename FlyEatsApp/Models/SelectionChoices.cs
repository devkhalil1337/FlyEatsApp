using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class SelectionChoices: BaseFilter
    {
        public const string CHOICE_ID_COLUMN = "ChoicesId";
        public const string CHOICE_SELECTION_ID_COLUMN = "SelectionId";
        public const string CHOICE_BUSINESS_ID_COLUMN = "BusinessId";     
        public const string CHOICE_NAME_COLUMN = "ChoiceName";
        public const string CHOICE_PRICE_COLUMN = "ChoicePrice";
        public const string CHOICE_SORT_BY_COLUMN = "ChoiceSortedBy";
        public const string CHOICE_CREATE_DATE_COLUMN = "CreationDate";
        public const string CHOICE_UPDATE_DATE_COLUMN = "UpdateDate";
        public const string CHOICE_DELETE_COLUMN = "IsDeleted";

        public int? ChoicesId { get; set; }
        public int? SelectionId { get; set; }
        public string? ChoiceName { get; set; }
        public double? ChoicePrice { get; set; }
        public int? ChoiceSortedBy { get; set; }

        public static SelectionChoices ExtractObject(DataRow dataRow)
        {
            var newObject = new SelectionChoices();
            newObject.ChoicesId = Convert.ToInt32(dataRow[CHOICE_ID_COLUMN]);
            newObject.SelectionId = Convert.ToInt32(dataRow[CHOICE_SELECTION_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[CHOICE_BUSINESS_ID_COLUMN]);        
            newObject.ChoiceName = Convert.ToString(dataRow[CHOICE_NAME_COLUMN]);
            newObject.ChoicePrice = Convert.ToInt32(dataRow[CHOICE_PRICE_COLUMN]);
            newObject.ChoiceSortedBy = Convert.ToInt32(dataRow[CHOICE_SORT_BY_COLUMN]);
            newObject.CreateDate = dataRow[CHOICE_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[CHOICE_CREATE_DATE_COLUMN]);
            newObject.UpdateDate = dataRow[CHOICE_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[CHOICE_UPDATE_DATE_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[CHOICE_DELETE_COLUMN]);
            return newObject;
        }
    }
}