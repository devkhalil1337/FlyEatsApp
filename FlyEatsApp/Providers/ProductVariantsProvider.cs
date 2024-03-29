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
                    return new List<ProductVariants>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    AllProductVariants.Add(ProductVariants.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
            }

            return AllProductVariants;
            ;
        }
        public object AddNewProductVariants(List<ProductVariants> productVariants,int productId,int businessId)
        {
            var results = new Object();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewProductVariant";

            var statusChangedDateTime = DateTime.UtcNow;
            Boolean isUpdateCall = false;
            for(int i = 0; i < productVariants.Count; i++)
            {
                if (productVariants[i].VariantId != null && productVariants[i].VariantId > 0)
                {
                    storedProcedureName = "SP_UpdateProductVariant";
                    isUpdateCall = true;
                }
                else
                {
                    storedProcedureName = "SP_AddNewProductVariant";
                    isUpdateCall = false;
                }
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                    { "ProductId", productId},
                    { "BusinessId", businessId},
                    { "VariationName", productVariants[i].VariationName},
                    { "VariationPrice", productVariants[i].VariationPrice},
                    { "CreationDate", statusChangedDateTime},
                    { "UpdateDate", statusChangedDateTime},
                    { "IsDeleted", productVariants[i].IsDeleted},
                    { "Active", productVariants[i].Active},
                };
                if(isUpdateCall)
                    parameters.Add("VariantId", productVariants[i].VariantId);
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
        public bool DeleteProductVariantById (int productId)
           {
               IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
               var storedProcedureName = "SP_DeleteProductVariantById";

               Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "ProductId", productId}};

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