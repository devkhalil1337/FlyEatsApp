
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class BusinessHoursProvider
    {
        string _ConnectionString;

        public BusinessHoursProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<BusinessHours> GetBusinessHours(int businessId)
        {
            List<BusinessHours> businessHours = new List<BusinessHours>();
            BusinessTimesProvider businessTimesProvider = new BusinessTimesProvider();
            var result = new Object();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetBusinessDaysById";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<BusinessHours>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = new BusinessHours();
                    newObject = BusinessHours.ExtractObject(dataRow);
                    result = businessTimesProvider.GetBusinessHoursById((int) newObject.BusinessDaysId);
                    if (result != null)
                    {
                        newObject.businessTimes = (List<BusinessTimes>?)result;
                    }
                    businessHours.Add(newObject);
                }
            }
            catch (Exception ex)
            {
            }

            return businessHours;
        }
        public object UpdateBusinessHours(List<BusinessHours>? businessHours)
        {
            var result = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            BusinessTimesProvider businessTimesProvider = new BusinessTimesProvider();
            var storedProcedureName = "SP_UpdateBusinessDays";
            String errorMessage = "";
            var statusChangedDateTime = DateTime.UtcNow;
            for(int i = 0; i < businessHours.Count; i++)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessDaysId", businessHours[i].BusinessDaysId},
                { "BusinessId", businessHours[i].BusinessId},
                { "WeekDayName", businessHours[i].WeekDayName },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
                { "Active", businessHours[i].Active },
            };

                try
                {
                    if(dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters) != null &&
                       businessHours[i].businessTimes != null && (businessHours[i].businessTimes.Count > 0))
                    {
                        businessTimesProvider.AddBusinessHours(businessHours[i].businessTimes,(int) businessHours[i].BusinessDaysId);
                    }
/*                    if (result.success)
                    {
                        int selectionId = (int)selections.SelectionId;
                        int _businessId = (int)selections.BusinessId;
                        if (selectionId > -1 && selections.selectionChoices != null && selections.selectionChoices.Count > 0)
                        {
                            SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
                            selectionChoicesProvider.AddNewSelectionChoices(selections.selectionChoices, selectionId, _businessId);
                        }
                    }
                    return result.onSuccess();*/
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    
                }
            }
            return errorMessage.Length > 0 ? result.onError(errorMessage) : result.onSuccess();
        }
    }
}
