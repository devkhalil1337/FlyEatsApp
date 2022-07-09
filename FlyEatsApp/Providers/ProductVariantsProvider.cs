
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
            _ConnectionString = "Data Source=DESKTOP-9FIV1UO\\SQLEXPRESS;Initial Catalog=Flyeats;Integrated Security=True"; //ConfigurationManager.ConnectionStrings["foodBuyConnectionString"].ConnectionString;
        }

        public IList<ProductVariants> GetAllProductVariants(int productId)
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
        public long AddNewProductVariants(ProductVariants productVariants)
        {
            
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewProductVariant";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ProductId", productVariants.ProductId},
                { "BusinessId", productVariants.BusinessId},
                { "VariationName", productVariants.VariationName},
                { "VariationPrice", productVariants.VariationPrice},
                { "CreationDate", statusChangedDateTime},
                { "UpdateDate", statusChangedDateTime},
                { "IsDeleted", productVariants.IsDeleted},
                { "Active", productVariants.Active},
            };

            try
            {
                var id = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return id == null ? -1 : Convert.ToInt64(id);
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
        public bool UpdateProductVariant(ProductVariants productVariants)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateProductVariant";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {

                { "VariantId", productVariants.VariantId},
               { "ProductId", productVariants.ProductId},
                { "BusinessId", productVariants.BusinessId},
                { "VariationName", productVariants.VariationName},
                { "VariationPrice", productVariants.VariationPrice},
                { "CreationDate", statusChangedDateTime},
                { "UpdateDate", statusChangedDateTime},
                { "IsDeleted", productVariants.IsDeleted},
                { "Active", productVariants.Active}

                };
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