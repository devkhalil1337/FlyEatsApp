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
    public class ProductSelectionProvider
    {


        string _ConnectionString;

        public ProductSelectionProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public int[] GetAllProductSelection(int productId)
        {
            List<ProductSelection> AllProductSelection = new List<ProductSelection>();
            int[] selectionsId;
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllProductSelectionByProductId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ProductId", productId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new int[] { };
                selectionsId = new int[dataSet.Tables[0].Rows.Count];
                int index = 0;
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
/*                    var newObject = new ProductSelection();
                    newObject.ProductSelectionId = Convert.ToInt32(dataRow[ProductSelection.PRODUCT_SELECTION_ID_COLUMN]);
                    newObject.ProductId = Convert.ToInt32(dataRow[ProductSelection.PRODUCT_ID_COLUMN]);
                    newObject.SelectionId = Convert.ToInt32(dataRow[ProductSelection.SELECTION_ID_COLUMN]);
                    newObject.BusinessId = Convert.ToInt32(dataRow[ProductSelection.PRODUCT_SELECTION_BUSINESS_ID_COLUMN]);
                    newObject.CreateDate = dataRow[ProductSelection.PRODUCT_SELECTION_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[ProductSelection.PRODUCT_SELECTION_CREATE_DATE_COLUMN]);
                    newObject.ModifyDate = dataRow[ProductSelection.PRODUCT_SELECTION_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[ProductSelection.PRODUCT_SELECTION_UPDATE_DATE_COLUMN]);
                    AllProductSelection.Add(newObject);*/
                    selectionsId[index] = Convert.ToInt32(dataRow[ProductSelection.SELECTION_ID_COLUMN]);
                    index++;
                }
                return selectionsId;
            }
            catch (Exception ex)
            {
            }

            return new int[] { };
        }

        public object AddNewProductSelection(int[] selectionIds,int productId,int BusinessId)
        {
            var results = new Object();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewProductSelection";

            var statusChangedDateTime = DateTime.UtcNow;
            for (int i = 0; i < selectionIds.Length; i++)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {


                { "ProductId", productId},
                { "SelectionId", selectionIds[i]},
                { "BusinessId", BusinessId },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },

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

       /* public object UpdateProductSelection(int[] selectionIds)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateProductSelection";

            var statusChangedDateTime = DateTime.UtcNow;
            for(int i = 0; i < selectionIds.Length; i++)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ProductSelectionId", productSelection.ProductId},
                { "ProductId", productSelection.ProductId},
                { "SelectionId", selectionIds[.SelectionId},
                { "BusinessId", productSelection.BusinessId },
                { "UpdateDate", statusChangedDateTime },
            };

                try
                {
                    var result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                    return result;
                }
                catch (Exception ex)
                {

                }
            }


            return false;
        }*/
        public object DeleteProductSelectionBy(long productId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteProductSelectionById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "ProductId", productId}
               };

            try
            {
                var result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return result;
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}