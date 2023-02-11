
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;

namespace FlyEatsApp.Providers
{
    public class OrderDetailSelectionRelationProvider
    {
        string _ConnectionString;
        public OrderDetailSelectionRelationProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public List<OrderDetailSelectionRelation> GetSelectionsById(int orderId)
        {
            List<OrderDetailSelectionRelation> GetSelections = new List<OrderDetailSelectionRelation>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetOrderSelectionsById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                 { "OrderDetailsId", orderId }
            };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<OrderDetailSelectionRelation>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var selection = OrderDetailSelectionRelation.ExtractObject(dataRow);
                    GetSelections.Add(selection);
                }
            }
            catch (Exception ex)
            {
                // Log the error message here
                Console.WriteLine("Error occured while retrieving order selections. Error: " + ex.Message);
            }

            return GetSelections;
        }


        public object AddNewSelections(List<OrderDetailSelectionRelation> selections,int orderDetailsId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewOrderDetailSelectionRelation";

            try
            {
                foreach (var selection in selections)
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object> {
                    { "OrderDetailsId", orderDetailsId },
                    { "BusinessId", selection.BusinessId },
                    { "SelectionId", selection.SelectionId },
                    { "ChoicesId", selection.ChoicesId },
                    { "ChoiceName", selection.ChoiceName },
                    { "ChoicePrice", selection.ChoicePrice },
                };

                    var results = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                }
                return "All selections added successfully";
            }
            catch (Exception ex)
            {
                return "An error occurred while adding the selections: " + ex.Message;
            }
        }

    }
}
