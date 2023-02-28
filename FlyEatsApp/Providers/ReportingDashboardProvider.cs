
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


    }
}
