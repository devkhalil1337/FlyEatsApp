using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class SettingsProvider
    {
        string _ConnectionString;

        public SettingsProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }


        public IList<Settings> GetSettingsById(int businessId)
        {
            List<Settings> settings = new List<Settings>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetSettingsById";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<Settings>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    settings.Add(Settings.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
                return new List<Settings>();
            }

            return settings;
        }


        public object AddNewSettings(Settings settings)
        {
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewSettings";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {


                { "BusinessId", settings.BusinessId},
                { "RegisterNumber", settings.RegisterNumber },
                { "Vat", settings.Vat },
                { "VatType", settings.VatType },
                { "ServiceCharges", settings.ServiceCharges },
                { "MinimumOrder", settings.MinimumOrder },
                { "DeliveryTime", settings.DeliveryTime },
                { "AveragePrepareTime", settings.AveragePrepareTime },
                { "IsGuestLoginActive", settings.IsGuestLoginActive },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
            };

            try
            {
                var productId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
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
        public object UpdateSettings(Settings settings)
        {
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateSettings";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {

                { "SettingsId", settings.SettingsId},
                { "BusinessId", settings.BusinessId},
                { "RegisterNumber", settings.RegisterNumber },
                { "Vat", settings.Vat },
                { "VatType", settings.VatType },
                { "ServiceCharges", settings.ServiceCharges },
                { "MinimumOrder", settings.MinimumOrder },
                { "DeliveryTime", settings.DeliveryTime },
                { "AveragePrepareTime", settings.AveragePrepareTime },
                { "IsGuestLoginActive", settings.IsGuestLoginActive },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
            };

            try
            {
                var productId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
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
    }
}
