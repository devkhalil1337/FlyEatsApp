
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class ProductVariantsProvider
    {


        string _ConnectionString;

        public ProductVariantsProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public List<ProductVariants> GetAllProductVariants(int productId)
        {
            List<ProductVariants> AllProductVariants = new List<ProductVariants>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllProductsVariantsByProductId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ProductId", productId},
               
            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = new ProductVariants();
                    newObject.VariantId = Convert.ToInt32(dataRow[ProductVariants.PRODUCT_VARIANT_ID_COLUMN]);
                    newObject.ProductId = Convert.ToInt32(dataRow[ProductVariants.PRODUCT_ID_COLUMN]);
                    newObject.BusinessId = Convert.ToInt32(dataRow[ProductVariants.PRODUCT_VARIANT_BUSINESS_ID_COLUMN]);
                    newObject.VariationName = Convert.ToString(dataRow[ProductVariants.PRODUCT_VARIANT_NAME_COLUMN]);
                    newObject.VariationPrice = Convert.ToDouble(dataRow[ProductVariants.PRODUCT_VARIANT_PRICE_COLUMN]);
                    newObject.CreateDate = dataRow[ProductVariants.PRODUCT_VARIANT_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[ProductVariants.PRODUCT_VARIANT_CREATE_DATE_COLUMN]);
                    newObject.ModifyDate = dataRow[ProductVariants.PRODUCT_VARIANT_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[ProductVariants.PRODUCT_VARIANT_UPDATE_DATE_COLUMN]);
                    newObject.IsDeleted = Convert.ToBoolean(dataRow[ProductVariants.PRODUCT_VARIANT_DELETE_COLUMN]);
                    newObject.Active = Convert.ToBoolean(dataRow[ProductVariants.PRODUCT_VARIANT_ACTIVE_COLUMN]);


                    AllProductVariants.Add(newObject);
                }
            }
            catch (Exception ex)
            {
            }

            return AllProductVariants;
            ;
        }
        public object AddNewProductVariants(List<ProductVariants> productVariants,int productId)
        {
            var results = new Object();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewProductVariant";

            var statusChangedDateTime = DateTime.UtcNow;
            for(int i = 0; i < productVariants.Count; i++)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ProductId", productId},
                { "BusinessId", productVariants[i].BusinessId},
                { "VariationName", productVariants[i].VariationName},
                { "VariationPrice", productVariants[i].VariationPrice},
                { "CreationDate", statusChangedDateTime},
                { "UpdateDate", statusChangedDateTime},
                { "IsDeleted", productVariants[i].IsDeleted},
                { "Active", productVariants[i].Active},
            };

                try
                {
                    results = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
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
            }
           
            return results;
        
       } 
        public object UpdateProductVariant(List<ProductVariants> productVariants)
        {
            var result = new object();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateProductVariant";

            var statusChangedDateTime = DateTime.UtcNow;
            for (int i = 0; i < productVariants.Count; i++)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {

                { "VariantId", productVariants[i].VariantId},
               { "ProductId", productVariants[i].ProductId},
                { "BusinessId", productVariants[i].BusinessId},
                { "VariationName", productVariants[i].VariationName},
                { "VariationPrice", productVariants[i].VariationPrice},
                { "CreationDate", statusChangedDateTime},
                { "UpdateDate", statusChangedDateTime},
                { "IsDeleted", productVariants[i].IsDeleted},
                { "Active", productVariants[i].Active},

                };
                try
                {
                    result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                }
                catch (Exception ex)
                {

                }
            }


            return result;
        }
        public bool DeleteProductVariantById (int variantId)
           {
               IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
               var storedProcedureName = "SP_DeleteProductVariantById";

               Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "VariantId", variantId}};

               try
               {
                   var result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                   return true;
               }
               catch (Exception ex)
               {
                  
               }

               return false;
           }
    }
}