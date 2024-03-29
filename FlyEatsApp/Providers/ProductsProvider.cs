﻿
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
                    if(newObject.productVariants.Count > 0)
                    {
                        newObject.productPrice = newObject.productVariants[0].VariationPrice;
                    }

                    /*if (newObject.Active == true)
                    {
                        AllProducts.Add(newObject);
                    }*/
                    AllProducts.Add(newObject);
                }
            }
            catch (Exception ex)
            {
                return new List<Products>();
            }
            return AllProducts;
            
        }
        public object AddNewProduct(Products product)
        {
            ResponseModel results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddProduct";

            var statusChangedDateTime = DateTime.UtcNow;
            Boolean hasVariations = product.productVariants.Count > 0;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                    { "BusinessId", product.BusinessId },
                    { "CategoryId", product.CategoryId },
                    { "ProductName", product.ProductName },
                    { "ProductDescription", product.ProductDescription },
                    { "ProductImage", product.ProductImage },
                    { "ProductSortOrder", product.ProductSortOrder },
                    { "ProductQuantity", product.ProductQuantity },
                    { "IsTableProduct", false },
                    { "TablePrice", product.TablePrice },
                    { "TableVat", product.TableVat },
                    { "IsPickupProduct", product.IsPickupProduct },
                    { "PickupPrice", product.PickupPrice },
                    { "PickupVat", product.PickupVat },
                    { "IsDeliveryProduct", product.IsDeliveryProduct },
                    { "DeliveryPrice", product.DeliveryPrice },
                    { "DeliveryVat", product.DeliveryVat },
                    { "HasVariations", hasVariations },
                    { "Featured", product.Featured },
                    { "CreationDate", statusChangedDateTime },
                    { "UpdateDate", statusChangedDateTime },
                    { "IsDeleted", product.IsDeleted },
                    { "Active", product.Active },
                    {"IsPopular",product.IsPopular }
                };

            try
            {
                int productId = dataAccessProvider.ExecuteScalarStoredProcedure(storedProcedureName, parameters);

                int _productId = (int)(productId == null ? -1 : Convert.ToInt64(productId));
                int _businessId = (int)product.BusinessId;
                //Product Selections
                if (_productId > -1 && product.selectionId != null && product.selectionId.Length > 0)
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
                results.success = true;
                results.message = "";
            }
            catch (Exception ex)
            {
                results.success = false;
                results.message = ex.Message;
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
        public object UpdateProduct(Products product)
        {
            var results = new ResponseModel();
            ProductSelectionProvider productSelection = new ProductSelectionProvider();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateProduct";

            var productUpdateChangedDateTime = DateTime.UtcNow;
            Boolean hasVariations = product.productVariants.Count > 0;
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ProductId", product.ProductId},
                { "CategoryId", product.CategoryId},
                { "BusinessId", product.BusinessId},
                { "ProductName", product.ProductName },
                { "ProductDescription", product.ProductDescription },
                { "ProductImage", product.ProductImage },
                { "ProductSortOrder", product.ProductSortOrder },
                { "ProductQuantity", product.ProductQuantity },
                { "IsTableProduct", product.IsTableProduct },
                { "TablePrice", product.TablePrice },
                { "TableVat", product.TableVat },
                { "IsPickupProduct", product.IsPickupProduct },
                { "PickupPrice", product.PickupPrice },
                { "PickupVat", product.PickupVat },
                { "IsDeliveryProduct", product.IsDeliveryProduct },
                { "DeliveryPrice", product.DeliveryPrice },
                { "DeliveryVat", product.DeliveryVat },
                { "HasVariations", hasVariations },
                { "Featured", product.Featured },
                { "UpdateDate", productUpdateChangedDateTime },
                { "IsDeleted", product.IsDeleted },
                { "Active", product.Active }
            };

            try
            {
                bool isUpdated = dataAccessProvider.ExecuteNonQueryStoredProcedure(storedProcedureName, parameters);
                int _productId = (int)product.ProductId, _businessId = (int)product.BusinessId;
                if (isUpdated)
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
                results.success = true;
                results.message = "";
            }
            catch (Exception ex)
            {
                results.success = false;
                results.message = ex.Message;
            }


            return results;
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
                return new List<Products>();
            }


            return GetProduct;

        }

        public object DeleteProductById(int productId)
        {
            ResponseModel response = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteProductById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "ProductId", productId}};

            try
            {
                dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                response.success = true;
                response.message = "";
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;

            }

            return response;
        }
    }
}