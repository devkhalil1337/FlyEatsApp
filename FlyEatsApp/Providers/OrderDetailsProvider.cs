﻿using FlyEatsApp.Models;
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
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            OrderDetailSelectionRelationProvider orderDetailSelectionRelationProvider = new OrderDetailSelectionRelationProvider();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetOrdersDetailsByOrderId";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
               { "OrderId", OrderId }
           };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<OrderDetails>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    OrderDetails orderDetails = OrderDetails.ExtractObject(dataRow);
                    if (orderDetails.ProductHaveSelection)
                    {
                        orderDetails.productVariants = orderDetailSelectionRelationProvider.GetSelectionsById(orderDetails.OrderDetailsId);
                    }
                    orderDetailsList.Add(orderDetails);
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("An error occurred while trying to retrieve order details: " + ex.Message);
            }

            return orderDetailsList;
        }


        public object AddNewOrderDetails(OrderDetails[] orders,int businessId)
        {
            var results = new ResponseModel();
            OrderDetailSelectionRelationProvider orderDetailSelectionRelationProvider = new OrderDetailSelectionRelationProvider();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewOrderDetails";
            foreach (var order in orders)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                    { "OrderId", order.OrderId },
                    { "BusinessId", businessId },
                    { "CategoryId", order.CategoryId },
                    { "ProductId", order.ProductId },
                    { "VariantId", order.VariantId },
                    { "ProductName", order.ProductName },
                    { "ProductQuantity", order.ProductQuantity },
                    { "ProductPrice", order.ProductPrice },
                    { "ProductComments", order.ProductComments },
                    { "ProductHaveSelection", order.ProductHaveSelection }
                };

                try
                {
                   var orderDetailsId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
                    if (order.ProductHaveSelection)
                    {
                        int id = (int) Convert.ToInt64(orderDetailsId);
                        if (order.productVariants.Count > 0)
                        {
                            orderDetailSelectionRelationProvider.AddNewSelections(order.productVariants, id);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return results.onError(ex.Message);
                }
            }
            return results.onSuccess();
        }


    }
}
