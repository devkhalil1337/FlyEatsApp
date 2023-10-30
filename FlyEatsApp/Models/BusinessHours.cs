using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class BusinessHours:BaseFilter
    {
        public const string BUSINESS_DAYS_ID_COLUMN = "BusinessDaysId";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string BUSINESS_WEEK_DAY_NAME_COLUMN = "WeekDayName";
        public const string BUSINESS_CREATION_DATE_COLUMN = "CreationDate";
        public const string BUSINESS_UPDATE_DATE_COLUMN = "UpdateDate";
        public const string BUSINESS_ACTIVE_COLUMN = "Active";

        public int? BusinessDaysId { get; set; }
        public string? WeekDayName { get; set; }

        public List<BusinessTimes>? businessTimes { get; set; }

        public static BusinessHours ExtractObject(DataRow dataRow)
        {
            var newObject = new BusinessHours();
            newObject.BusinessDaysId = Convert.ToInt32(dataRow[BUSINESS_DAYS_ID_COLUMN]);
            newObject.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            newObject.WeekDayName = Convert.ToString(dataRow[BUSINESS_WEEK_DAY_NAME_COLUMN]).Replace(" ", string.Empty);
            newObject.CreateDate = dataRow[BUSINESS_CREATION_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[BUSINESS_CREATION_DATE_COLUMN]);
            newObject.UpdateDate = dataRow[BUSINESS_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[BUSINESS_UPDATE_DATE_COLUMN]);
            newObject.Active = Convert.ToBoolean(dataRow[BUSINESS_ACTIVE_COLUMN]);

            return newObject;
        }
    }
}
