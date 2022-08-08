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
        private DateTime? StartDay { get; set; }

        public string? startDate
        {
            get
            {
                return StartDay.HasValue ? StartDay.Value.ToString("yyyy-MM-dd hh:mm:ss") : null;
            }

            set
            {
                if (value != null)
                {
                    StartDay = DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

       
        private DateTime? EndDay { get; set; }

        public string? endDate
        {
            get { return EndDay.HasValue ? EndDay.Value.ToString("yyyy-MM-dd hh:mm:ss") : null; }

            set
            {
                if (value != null)
                {
                    EndDay = DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public static BusinessTimes ExtractObject(DataRow dataRow)
        {
            var newObject = new BusinessTimes();
            newObject.BusinessTimesId = Convert.ToInt32(dataRow[BUSINESS_TIMES_ID_COLUMN]);
            newObject.BusinessDaysId = Convert.ToInt32(dataRow[BUSINESS_DAYS_ID_COLUMN]);
            newObject.StartDay = dataRow[BUSINESS_START_DAY_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[BUSINESS_START_DAY_COLUMN]);
            newObject.EndDay = dataRow[BUSINESS_END_DAY_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[BUSINESS_END_DAY_COLUMN]);
            return newObject;
        }
    }
}
