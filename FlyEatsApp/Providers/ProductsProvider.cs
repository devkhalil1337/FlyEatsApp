
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class ProductsProvider
    {


        string _ConnectionString;

        public ProductsProvider()
        {
            _ConnectionString = "Data Source=DESKTOP-9FIV1UO\\SQLEXPRESS;Initial Catalog=Flyeats;Integrated Security=True"; //ConfigurationManager.ConnectionStrings["foodBuyConnectionString"].ConnectionString;
        }

        public IList<Products> GetAllProducts(int businessId)
        {
            List<Products> AllProducts = new List<Products>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllProductsByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = new Products();
                    newObject.ProductId = Convert.ToInt32(dataRow[Products.PRODUCT_ID_COLUMN]);
                    newObject.CategoryId = Convert.ToInt32(dataRow[Products.PRODUCT_CATEGORY_ID_COLUMN]);
                    newObject.BusinessId = Convert.ToInt32(dataRow[Products.PRODUCT_BUSINESS_ID_COLUMN]);
                    newObject.CategoryName = Convert.ToString(dataRow[Products.CATEGORY_NAME_COLUMN]);
                    newObject.ProductImage = Convert.ToString(dataRow[Products.PRODUCT_IMAGE_COLUMN]);
                    newObject.ProductName = Convert.ToString(dataRow[Products.PRODUCT_NAME_COLUMN]);
                    newObject.ProductDescription = Convert.ToString(dataRow[Products.PRODUCT_DESCRITPTION_COLUMN]);
                    newObject.ProductTablePrice = Convert.ToDouble(dataRow[Products.PRODUCT_TABLE_PRCIE_COLUMN]);
                    newObject.ProductTableVat = Convert.ToDouble(dataRow[Products.PRODUCT_TABLE_VAT_COLUMN]);
                    newObject.ProductPickupPrice = Convert.ToDouble(dataRow[Products.PRODUCT_PICKUP_PRCIE_COLUMN]);
                    newObject.ProductPickupVat = Convert.ToDouble(dataRow[Products.PRODUCT_PICKUP_VAT_COLUMN]);
                    newObject.ProductDeliveryPrice = Convert.ToDouble(dataRow[Products.PRODUCT_DELIVERY_PRCIE_COLUMN]);
                    newObject.ProductDeliveryVat = Convert.ToDouble(dataRow[Products.PRODUCT_DELIVERY_VAT_COLUMN]);
                    newObject.ProductSortBy = Convert.ToInt32(dataRow[Products.PRODUCT_SORT_BY_COLUMN]);
                    newObject.ProductQuantity = Convert.ToInt32(dataRow[Products.PRODUCT_QUANTITY_COLUMN]);
                    newObject.HasVariations = Convert.ToBoolean(dataRow[Products.PRODUCT_HAS_VARIATION_COLUMN]);
                    newObject.Featured = Convert.ToBoolean(dataRow[Products.PRODUCT_FEATURED_COLUMN]);
                    newObject.CreateDate = dataRow[Products.PRODUCT_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[Products.PRODUCT_CREATE_DATE_COLUMN]);
                    newObject.ModifyDate = dataRow[Products.PRODUCT_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[Products.PRODUCT_UPDATE_DATE_COLUMN]);
                    newObject.IsDeleted = Convert.ToBoolean(dataRow[Products.PRODUCT_DELETE_COLUMN]);
                    newObject.Active = Convert.ToBoolean(dataRow[Products.PRODUCT_ACTIVE_COLUMN]);
                    AllProducts.Add(newObject);
                }
            }
            catch (Exception ex)
            {
            }

            return AllProducts;
            ;
        }
        public object AddNewProduct(Products product)
        {

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewProduct";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
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
                { "CreationDate", statusChangedDateTime},
                { "UpdateDate", statusChangedDateTime},
                { "IsDeleted", product.IsDeleted},
                { "Active", product.Active},
            };

            try
            {
                var results = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return results;
            }
            catch (Exception ex)
            {
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
                var results = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return results;
            }
            catch (Exception ex)
            {

            }


            return false;
        }
        public IList<Products> GetProductsById(int productId)
        {
            List<Products> GetProduct = new List<Products>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetProductDetailById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "ProductId", productId }
               };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var category = Products.ExtractObject(dataRow);
                    GetProduct.Add(category);
                }
            }
            catch (Exception ex)
            {

            }


            return GetProduct;

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
    }
}