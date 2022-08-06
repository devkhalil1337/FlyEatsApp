using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Selections: BaseFilter
    {
        public const string SELECTION_ID_COLUMN = "SelectionId";
        public const string SELECTION_BUSINESS_ID_COLUMN = "BusinessId";     
        public const string SELECTION_NAME_COLUMN = "SelectionName";
        public const string SELECTION_MINIMUM_COLUMN = "MinimumSelection";
        public const string SELECTION_MAXIMUM_COLUMN = "MaximumSelection";
        public const string SELECTION_CREATE_DATE_COLUMN = "CreationDate";
        public const string SELECTION_UPDATE_DATE_COLUMN = "UpdateDate";
        public const string SELECTION_DELETE_COLUMN = "IsDeleted";
        public const string SELECTION_ACTIVE_COLUMN = "Active";

        public int? SelectionId { get; set; }
        public string? SelectionName { get; set; }
        public int? MinimumSelection { get; set; }
        public int? MaximumSelection { get; set; }
        public List<SelectionChoices>? selectionChoices { get; set; }
        public static Selections ExtractObject(DataRow dataRow)
        {
            var newObject = new Selections();
            newObject.SelectionId = Convert.ToInt32(dataRow[SELECTION_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[SELECTION_BUSINESS_ID_COLUMN]);        
            newObject.SelectionName = Convert.ToString(dataRow[SELECTION_NAME_COLUMN]);
            newObject.MinimumSelection = Convert.ToInt32(dataRow[SELECTION_MINIMUM_COLUMN]);
            newObject.MaximumSelection = Convert.ToInt32(dataRow[SELECTION_MAXIMUM_COLUMN]);
            newObject.CreateDate = dataRow[SELECTION_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[SELECTION_CREATE_DATE_COLUMN]);
            newObject.ModifyDate = dataRow[SELECTION_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[SELECTION_UPDATE_DATE_COLUMN]);
            newObject.IsDeleted = Convert.ToBoolean(dataRow[SELECTION_DELETE_COLUMN]);
            newObject.Active = Convert.ToBoolean(dataRow[SELECTION_ACTIVE_COLUMN]);

            return newObject;
        }
    }
}