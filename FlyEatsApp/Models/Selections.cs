using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class Selections
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
        public int? BusinessId { get; set; }
        public string? SelectionName { get; set; }
        public int? MinimumSelection { get; set; }
        public int? MaximumSelection { get; set; }
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

        public bool? IsDeleted { get; set; }
        public bool? Active { get; set; }


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