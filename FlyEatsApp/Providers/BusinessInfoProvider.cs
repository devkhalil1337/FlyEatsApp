
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class BusinessInfoProvider
    {


        string _ConnectionString;

        public BusinessInfoProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<BusinessInfo> GetAllBusinesUnits()
        {
            List<BusinessInfo> businessUnits = new List<BusinessInfo>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllBusinesUnits";
            Dictionary<string, object> parameters = new Dictionary<string, object> { };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    businessUnits.Add(BusinessInfo.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
            }

            return businessUnits;
        }



        public ResponseModel AddNewBusinessUnit (BusinessInfo businessInfo)
        {
            ResponseModel response = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewBusiness";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
            

                { "Localization", businessInfo.Localization},
                { "BusinessName", businessInfo.BusinessName},
                { "BusinessContact", businessInfo.BusinessContact },
                { "BusinessEmail", businessInfo.BusinessEmail },
                { "BusinessAddress", businessInfo.BusinessAddress },
                { "BusinessLogo", businessInfo.BusinessLogo },
                { "BusinessPostcode", businessInfo.BusinessPostcode },
                { "BusinessCity", businessInfo.BusinessCity },
                { "BusinessCountry", businessInfo.BusinessCountry },
                { "BusinessDetails", businessInfo.BusinessDetails },
                { "BusinessLatitude", businessInfo.BusinessLatitude },
                { "BusinessLongitude", businessInfo.BusinessLongitude },
                { "BusinessCurrency", businessInfo.BusinessCurrency },
                { "BusinessWebsiteUrl", businessInfo.BusinessWebsiteUrl },
                { "BusinessTempClose", businessInfo.BusinessTempClose },
                { "ClosetillDate", businessInfo.ClosetillDate },
                { "BusinessExpiryDate", businessInfo.BusinessExpiryDate },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
                { "Deleted", businessInfo.Deleted },
                { "Active", businessInfo.Active },
            };

            try
            {
                response = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
                /* LogEntry logEntry = new LogEntry()
                 {
                     Severity = System.Diagnostics.TraceEventType.Error,
                     Title = string.Format("Creating New business Info for a customer ", businessInfo.BusinessName),
                     Message = ex.Message + Environment.NewLine + ex.StackTrace
                 };
                 Logger.Write(logEntry);*/
            }


            return response;
        }
        public ResponseModel UpdateBusinessUnit(BusinessInfo businessInfo)
        {
            ResponseModel response = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateBusinesInfo";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessInfo.BusinessId},
                { "Localization", businessInfo.Localization},
                { "BusinessName", businessInfo.BusinessName},
                { "BusinessContact", businessInfo.BusinessContact },
                { "BusinessEmail", businessInfo.BusinessEmail },
                { "BusinessAddress", businessInfo.BusinessAddress },
                { "BusinessLogo", businessInfo.BusinessLogo },
                { "BusinessPostcode", businessInfo.BusinessPostcode },
                { "BusinessCity", businessInfo.BusinessCity },
                { "BusinessCountry", businessInfo.BusinessCountry },
                { "BusinessDetails", businessInfo.BusinessDetails },
                { "BusinessLatitude", businessInfo.BusinessLatitude },
                { "BusinessLongitude", businessInfo.BusinessLongitude },
                { "BusinessCurrency", businessInfo.BusinessCurrency },
                { "BusinessWebsiteUrl", businessInfo.BusinessWebsiteUrl },
                { "BusinessTempClose", businessInfo.BusinessTempClose },
                { "ClosetillDate", businessInfo.ClosetillDate },
                { "BusinessExpiryDate", businessInfo.BusinessExpiryDate },
                { "UpdateDate", statusChangedDateTime },
                { "Deleted", businessInfo.Deleted },
                { "Active", businessInfo.Active },
             };

            try
            {
                response = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }


            return response;
        }
        public IList<BusinessInfo> GetBusinessUnitById(int BusinessId)
        {
            List<BusinessInfo> GetBusinessUnits = new List<BusinessInfo>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetBusinessInfo";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "BusinessId", BusinessId }
               };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<BusinessInfo>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    GetBusinessUnits.Add(BusinessInfo.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
                return new List<BusinessInfo>();
            }
            
            
            return GetBusinessUnits;

        } 
        public ResponseModel DeleteBusinessUnit(long BusinessId)
           {
            ResponseModel response = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
               var storedProcedureName = "SP_DeleteBusinessInfo";

               Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "BusinessId", BusinessId}
               };

               try
               {
                response = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
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