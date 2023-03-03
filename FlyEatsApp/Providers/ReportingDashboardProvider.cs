
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class ReportingDashboardProvider
    {
        string _ConnectionString;

        public ReportingDashboardProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public int GetNumberOfOrders(int businessId,string orderStatus, string Datefrom,string Dateto)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            int numberofOrders = 0;
            var storedProcedureName = "SP_GetOrdersByTypeAndDateRange";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "BusinessId", businessId },
                   { "FromDate", Datefrom },
                   { "ToDate", Dateto },
                   { "OrderStatus", orderStatus }
               };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return 0;
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    numberofOrders = ReportingDashboard.ExtractNumberOfOrders(dataRow);
                }
                return numberofOrders;
            }
            catch (Exception ex)
            {
                return 0;
            }


            return 0;

        }

        public List<dynamic> GetGrossSalesByDay(int businessId, string dateFrom, string dateTo)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetGrossSales";

                    Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },
                { "StartDate", dateFrom },
                { "EndDate", dateTo }
            };

            var grossSalesByDay = new Dictionary<string, decimal>();

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<dynamic>();

                var startDate = DateTime.Parse(dateFrom);
                var endDate = DateTime.Parse(dateTo);
                for (var currentDate = startDate; currentDate <= endDate; currentDate = currentDate.AddDays(1))
                {
                    var dateStr = currentDate.ToString("yyyy-MM-dd");
                    grossSalesByDay[dateStr] = 0;
                }

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var totalAmount = dataRow.Field<decimal>("GrossSales");
                    var createdDate = DateTime.Parse(dataRow.Field<string>("CreatedDate"));
                    var dateStr = createdDate.ToString("yyyy-MM-dd");
                    if (grossSalesByDay.ContainsKey(dateStr))
                    {
                        grossSalesByDay[dateStr] += totalAmount;
                    }
                }

                // Convert the dictionary into a format suitable for use with Highcharts
                var result = new List<dynamic>();
                foreach (var kvp in grossSalesByDay)
                {
                    result.Add(new { date = kvp.Key, amount = kvp.Value, y = kvp.Value });
                }
                return result;
            }
            catch (Exception ex)
            {
                return new List<dynamic>();
            }
        }





    }
}
