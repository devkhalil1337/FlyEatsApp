using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class OrderDetailsProvider
    {

        string _ConnectionString;

        public OrderDetailsProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<OrderDetails> GetOrderById(string OrderId)
        {
            List<OrderDetails> GetOrder = new List<OrderDetails>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetOrdersDetailsByOrderId";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "OrderId", OrderId }
               };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<OrderDetails>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var order = OrderDetails.ExtractObject(dataRow);
//                    selectedOrder = order;
                    GetOrder.Add(order);
                }
            }
            catch (Exception ex)
            {

            }


            return GetOrder;

        }

        public object AddNewOrderDetails(OrderDetails[] order)
        {
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewOrderDetails";
            for(int i = 0; i < order.Length; i++)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "OrderId", order[i].OrderId },
                { "BusinessId", order[i].BusinessId },
                { "CategoryId", order[i].CategoryId },
                { "ProductId", order[i].ProductId },
                { "VariantId", order[i].VariantId },
                { "ProductName", order[i].ProductName },
                { "ProductQuantity", order[i].ProductQuantity },
                { "ProductPrice", order[i].ProductPrice },
                { "ProductComments", order[i].ProductComments },
                { "ProductHaveSelection", order[i].ProductHaveSelection }
            };



                try
                {
                    var productId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
                }
                catch (Exception ex)
                {

                    return results.onError(ex.Message);
                }
                return results.onSuccess();

            }
            return -1;

        }

    }
}
