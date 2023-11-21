
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;
using FlyEatsApp.Payloads;
using log4net.Core;
using log4net;

namespace FlyEatsApp.Providers
{
    public class OrderProvider
    {

        string _ConnectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(OrderProvider));
        public OrderProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<Order> GetAllOrders(OrderPayload orderPayload)
        {
            List<Order> AllOrders = new List<Order>();
            Order order = new Order();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllOrdersByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", orderPayload.BusinessId },
                { "StartDate", orderPayload.startDate },
                { "EndDate", orderPayload.endDate },
                { "OrderStatus", orderPayload.OrderStatus },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<Order>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    AllOrders.Add(Order.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to retrieve all orders : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                return new List<Order>();
            }

            return AllOrders;
            ;
        }
        public object AddNewOrder(Order order)
        {
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewOrder";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", order.BusinessId },
                { "CustomerId", order.CustomerId },
                { "OrderInvoiceNumber", order.OrderInvoiceNumber },
                { "OrderType", order.OrderType },
                { "OrderTableId", order.OrderTableId },
                { "OrderStatus", order.OrderStatus },
                { "ServiceChargeAmount", order.ServiceChargeAmount },
                { "DiscountAmount", order.DiscountAmount },
                { "VoucherId", order.VoucherId },
                { "VoucherDiscountAmount", order.VoucherDiscountAmount },
                { "TotalAmount", order.TotalAmount },
                { "VatAmount", order.VatAmount },
                { "VatPercentage", order.VatPercentage },
                { "VatType", order.VatType },
                { "PaymentStatus", order.PaymentStatus },
                { "PaymentMethod", order.PaymentMethod },
                { "AveragePreparationTime", order.AveragePreparationTime },
                { "Comments", order.Comments },
                { "DeliveryTime", order.DeliveryTime },
                { "CustomerDeliveryId", order.CustomerDeliveryId },
                { "CompletedBy", order.CompletedBy },
                { "DeliveryCharges", order.DeliveryCharges },
                { "CardPaymentId", order.CardPaymentId },
                { "CreatedDate", order.CreatedDate },
                { "ModifiedDate", order.CreatedDate },
                { "IsDeleted", order.IsDeleted },
             };


            try
            {
                results = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to Creating New Order : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                return results.onError(ex.Message);
                /* LogEntry logEntry = new LogEntry()
                 {
                     Severity = System.Diagnostics.TraceEventType.Error,
                     Title = string.Format("Creating New business Info for a customer ", categories.BusinessName),
                     Message = ex.Message + Environment.NewLine + ex.StackTrace
                 };
                 Logger.Write(logEntry);*/
            }
            return results;

        }
        public ResponseModel UpdateOrderStatus(string orderNumber,string orderStatus)
        {
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateOrderStatusByInvoiceNumber";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "orderInvoiceNumber", orderNumber},
                { "newOrderStatus", orderStatus}
            };

            try
            {
                results = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
                if (results.success)
                {
                    /* Update custom field of an order */
                    if (orderStatus.Contains("completed", StringComparison.OrdinalIgnoreCase))
                    {
                        updateOrderAttributes(orderNumber);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to Creating New Order : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                results.onError(ex.Message);
            }


            return results;
        }
        public Order GetOrderById(string OrderId)
        {
            List<Order> GetOrder = new List<Order>();
            Order selectedOrder = new Order();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetOrderById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "OrderInvoiceNumber", OrderId }
               };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new Order();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var order = Order.ExtractObject(dataRow);
                    selectedOrder = order;
                    GetOrder.Add(order);
                }
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to gettig order by Id : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
            }


            return selectedOrder;

        }

        public object DeleteProductById(int productId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteProductById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "ProductId", productId}};

            try
            {
                var results = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return results;
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to deleting order by Id : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
            }

            return false;
        }


        //For Client Side

        public IList<Order> GetOrdersByCustomerId(int customerId)
        {
            List<Order> orders = new List<Order>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetOrdersByCustomerId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "CustomerId", customerId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<Order>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    orders.Add(Order.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to GetOrdersByCustomerId : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                return new List<Order>();
            }

            return orders;
        }


        public List<string> GetOrderStatusById(string[] OrderIds)
        {
            List<string> orderStatusList = new List<string>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetOrderById";

            foreach (var OrderId in OrderIds)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
               { "OrderInvoiceNumber", OrderId }
           };

                try
                {
                    var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                    if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    {
                        orderStatusList.Add("Order not found");
                        continue;
                    }

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        var order = Order.ExtractObject(dataRow);
                        orderStatusList.Add(order.OrderStatus);
                    }
                }
                catch (Exception ex)
                {
                    var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to GetOrderStatusById : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                    logger.Logger.Log(logEntry);
                    orderStatusList.Add("Error getting order status: " + ex.Message);
                }
            }

            return orderStatusList;
        }

        private void updateOrderAttributes(String orderNumber) 
        {
            Order order = new Order();
            order = GetOrderById(orderNumber);
            order.PaymentStatus = "PAID";
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateOrderAttributes";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "OrderInvoiceNumber", orderNumber },
                { "OrderStatus", order.OrderStatus },
                { "ServiceChargeAmount", order.ServiceChargeAmount },
                { "DiscountAmount", order.DiscountAmount },
                { "VoucherId", order.VoucherId },
                { "VoucherDiscountAmount", order.VoucherDiscountAmount },
                { "TotalAmount", order.TotalAmount },
                { "VatAmount", order.VatAmount },
                { "VatPercentage", order.VatPercentage },
                { "VatType", order.VatType },
                { "PaymentStatus", order.PaymentStatus },
                { "PaymentMethod", order.PaymentMethod },
                { "AveragePreparationTime", order.AveragePreparationTime },
                { "Comments", order.Comments },
                { "DeliveryTime", order.DeliveryTime },
                { "CustomerDeliveryId", order.CustomerDeliveryId },
                { "CompletedBy", order.CompletedBy },
                { "DeliveryCharges", order.DeliveryCharges },
                { "CardPaymentId", order.CardPaymentId },
                { "ModifiedDate", order.ModifiedDate }
             };

            try
            {
                results = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to update an existing order : " + ex.Message + Environment.NewLine + ex.StackTrace, null);
                logger.Logger.Log(logEntry);
            }

        }

    }
}
