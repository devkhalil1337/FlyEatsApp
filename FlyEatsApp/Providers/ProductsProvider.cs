
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
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<Products> GetAllProducts(int businessId)
        {
            List<Products> AllProducts = new List<Products>();
            Products products = new Products();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllProductsByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<Products>();
                ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
                ProductSelectionProvider productSelection = new ProductSelectionProvider();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = Products.ExtractObject(dataRow);
                    newObject.selectionId = productSelection.GetAllProductSelection((int)newObject.ProductId);
                    newObject.productVariants = productVariantsProvider.GetAllProductVariants((int)newObject.ProductId);
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
            var results = new ResponseModel();
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
                var productId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);

               int _productId =  (int)(productId == null ? -1 : Convert.ToInt64(productId));
               int _businessId = (int) product.BusinessId;
                //Product Selections
                if(_productId > -1 && product.selectionId != null && product.selectionId.Length > 0)
                {
                    ProductSelectionProvider productSelectionProvider = new ProductSelectionProvider();
                    productSelectionProvider.AddNewProductSelection(product.selectionId, _productId, _businessId);
                }
                //Produect Variants
                if (_productId != -1 && product.productVariants != null && product.productVariants.Count > 0)
                {

                    ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
                    productVariantsProvider.AddNewProductVariants(product.productVariants, _productId, _businessId);
                }

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
                var results = (ResponseModel)  dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
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
        public IList<Products> GetProductsById(int productId)
        {
            List<Products> GetProduct = new List<Products>();
            ProductVariantsProvider productVariantsProvider = new ProductVariantsProvider();
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetProductDetailById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "ProductId", productId }
               };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<Products>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var product = Products.ExtractObject(dataRow);
                    product.selectionId = productSelection.GetAllProductSelection(productId);
                    product.productVariants = productVariantsProvider.GetAllProductVariants(productId);
                    GetProduct.Add(product);
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