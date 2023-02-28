using FlyEatsApp.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace FlyEatsApp.Models
{
    public class ReportingDashboard : BaseFilter
    {
        public const string NUMBERS_OF_ORDERS_COLUMN = "NumberOfOrders";


        public int NumberOfOrders { get; set; }


        public static int ExtractNumberOfOrders(DataRow dataRow)
        {
            return Convert.ToInt32(dataRow[NUMBERS_OF_ORDERS_COLUMN]);
        }
    }
}
