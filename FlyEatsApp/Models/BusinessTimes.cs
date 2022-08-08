using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class BusinessTimes
    {
        public const string BUSINESS_TIMES_ID_COLUMN = "BusinessTimesId";
        public const string BUSINESS_DAYS_ID_COLUMN = "BusinessDaysId";
        public const string BUSINESS_START_DAY_COLUMN = "StartDay";
        public const string BUSINESS_END_DAY_COLUMN = "EndDay";

        public int? BusinessTimesId { get; set; }
        public int? BusinessDaysId { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public bool isDeleted { get; set; }

        public static BusinessTimes ExtractObject(DataRow dataRow)
        {
            var newObject = new BusinessTimes();
            newObject.BusinessTimesId = Convert.ToInt32(dataRow[BUSINESS_TIMES_ID_COLUMN]);
            newObject.BusinessDaysId = Convert.ToInt32(dataRow[BUSINESS_DAYS_ID_COLUMN]);
            newObject.startDate = dataRow[BUSINESS_START_DAY_COLUMN] == DBNull.Value ? null : Convert.ToString(dataRow[BUSINESS_START_DAY_COLUMN]);
            newObject.endDate = dataRow[BUSINESS_END_DAY_COLUMN] == DBNull.Value ? null : Convert.ToString(dataRow[BUSINESS_END_DAY_COLUMN]);
            return newObject;
        }
    }
}
