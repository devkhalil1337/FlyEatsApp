using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class BusinessTimesProvider
    {
        string _ConnectionString;

        public BusinessTimesProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<BusinessTimes> GetBusinessHoursById(int businessDaysId)
        {
            List<BusinessTimes> businessHours = new List<BusinessTimes>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetBusinessTimesById";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessDaysId", businessDaysId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<BusinessTimes>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = new BusinessTimes();
                    newObject = BusinessTimes.ExtractObject(dataRow);
                    businessHours.Add(newObject);
                }
            }
            catch (Exception ex)
            {
                return new List<BusinessTimes>();
            }

            return businessHours;
        }


        public object AddBusinessHours(List<BusinessTimes>? businessTimes, int BusinessDaysId)
        {
            var result = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddBusinessTimes";
            String errorMessage = "";
            Boolean isUpdateCall = false;
            bool isDeleteCall = false;
            for (int i = 0; i < businessTimes.Count; i++)
            {
                isDeleteCall = businessTimes[i].isDeleted;
                if (isDeleteCall != null && isDeleteCall)
                {
                    DeleteBusinessTimeById((int)businessTimes[i].BusinessTimesId);
                    continue;
                }
                if(businessTimes[i].BusinessTimesId != null  && businessTimes[i].BusinessTimesId > -1)
                {
                    isUpdateCall = true;
                    storedProcedureName = "SP_UpdateBusinessTimes";
                }
                else
                {
                    isUpdateCall = false;
                    storedProcedureName = "SP_AddBusinessTimes";
                }
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "StartDay", businessTimes[i].startDate },
                { "EndDay", businessTimes[i].endDate },
            };
                if (isUpdateCall)
                {
                    parameters.Add("BusinessTimesId", businessTimes[i].BusinessTimesId);
                }
                else
                {
                    parameters.Add("BusinessDaysId", BusinessDaysId);
                }

                try
                {
                    var _result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                    
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;

                }
            }
            return errorMessage.Length > 0 ? result.onError(errorMessage) : result.onSuccess();

        }

        public object UpdateBusinessHours(BusinessTimes[] businessTimes)
        {
            var result = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateBusinessDays";
            String errorMessage = "";
            for (int i = 0; i < businessTimes.Length; i++)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessTimesId", businessTimes[i].BusinessTimesId},
                { "BusinessDaysId", businessTimes[i].BusinessDaysId},
                { "StartDay", businessTimes[i].startDate },
                { "EndDay", businessTimes[i].endDate },
            };

                try
                {
                    result = (ResponseModel)dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;

                }
            }
            return errorMessage.Length > 0 ? result.onError(errorMessage) : result.onSuccess();
        }

        public object DeleteBusinessTimeById(int BusinessTimesId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteBusinessTimesById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "BusinessTimesId", BusinessTimesId}
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
