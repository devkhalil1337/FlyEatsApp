
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
                { "OrderServiceCharges", order.OrderServiceCharges },
                { "OrderDiscount", order.OrderDiscount },
                { "OrderVoucherId", order.OrderVoucherId },
                { "OrderVoucherDiscountAmount", order.OrderVoucherDiscountAmount },
                { "OrderTotalAmount", order.OrderTotalAmount },
                { "OrderVatAmount", order.OrderVatAmount },
                { "OrderVatPercentage", order.OrderVatPercentage },
                { "VatType", order.VatType },
                { "OrderPaymentStatus", order.OrderPaymentStatus },
                { "OrderPaymentMethod", order.OrderPaymentMethod },
                { "AverageOrderPreprationTime", order.AverageOrderPreprationTime },
                { "OrderComments", order.OrderComments },
                { "OrderDeliveryTime", order.OrderDeliveryTime },
                { "CustomerDeliveryId", order.CustomerDeliveryId },
                { "OrderCompletedBy", order.OrderCompletedBy },
                { "CreationDate", order.CreationDate },
                { "UpdateDate", statusChangedDateTime },
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
        public object UpdateProduct(Products product)
        {
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateProduct";

            var productUpdateChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ProductId", product.ProductId},
                { "CategoryId", product.CategoryId},
                { "BusinessId", product.BusinessId},
                { "ProductImage", product.ProductImage},
                { "ProductName", product.ProductName},
                { "ProductDescription", product.ProductDescription},
                { "ProductTablePrice", product.ProductTablePrice},
                { "ProductTableVat", product.ProductTableVat},
                { "ProductPickupPrice", product.ProductPickupPrice},
                { "ProductPickupVat", product.ProductPickupVat},
                { "ProductDeliveryPrice", product.ProductDeliveryPrice},
                { "ProductDeliveryVat", product.ProductDeliveryVat},
                { "ProductSortBy", product.ProductSortBy},
                { "ProductQuantity", product.ProductQuantity},
                { "HasVariations", product.HasVariations},
                { "Featured", product.Featured},
                { "UpdateDate", productUpdateChangedDateTime},
                { "IsDeleted", product.IsDeleted},
                { "Active", product.Active},
            };

            try
            {
                var results = (ResponseModel)dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                int _productId = (int)product.ProductId, _businessId = (int)product.BusinessId;
                if (results.success)
                {
                    ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
                    if (product.productVariants != null && product.productVariants.Count > 0)
                    {
                        productVariantsProvider.AddNewProductVariants(product.productVariants, _productId, _businessId);
                    }
                    else
                    {
                        productVariantsProvider.DeleteProductVariantById(_productId);
                    }
                    if (product.selectionId != null && product.selectionId.Length > 0)
                    {
                        // Clear selections ids from reference table before adding/updating new entries
                        productSelection.DeleteProductSelectionBy(_productId);
                        productSelection.AddNewProductSelection(product.selectionId, _productId, _businessId);
                    }
                    else
                    {
                        // Clear All selections ids from reference table
                        productSelection.DeleteProductSelectionBy(_productId);
                    }
                }
                return results;
            }
            catch (Exception ex)
            {

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


    }
}
