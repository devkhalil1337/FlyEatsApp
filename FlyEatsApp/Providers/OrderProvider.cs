
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class OrderProvider
    {

        string _ConnectionString;

        public OrderProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<Order> GetAllOrders(int businessId)
        {
            List<Order> AllOrders = new List<Order>();
            Order order = new Order();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllOrdersByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },

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
            }

            return AllOrders;
            ;
        }
        public object AddNewOrder(Order order)
        {
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewOrder";

            var statusChangedDateTime = DateTime.UtcNow;

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
                var productId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
                int _productId = (int)(productId == null ? -1 : Convert.ToInt64(productId));
                return results.onSuccess();
            }
            catch (Exception ex)
            {

                return results.onError(ex.Message);
                /* LogEntry logEntry = new LogEntry()
                 {
                     Severity = System.Diagnostics.TraceEventType.Error,
                     Title = string.Format("Creating New business Info for a customer ", categories.BusinessName),
                     Message = ex.Message + Environment.NewLine + ex.StackTrace
                 };
                 Logger.Write(logEntry);*/
            }
            return -1;

        }
        public Boolean UpdateOrderStatus(string orderNumber,string orderStatus)
        {
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateOrderStatusByInvoiceNumber";

            var productUpdateChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "orderInvoiceNumber", orderNumber},
                { "newOrderStatus", orderStatus}
            };

            try
            {
                var results = dataAccessProvider.ExecuteScalarStoredProcedure(storedProcedureName, parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


            return false;
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
                    orderStatusList.Add("Error getting order status: " + ex.Message);
                }
            }

            return orderStatusList;
        }



    }
}
