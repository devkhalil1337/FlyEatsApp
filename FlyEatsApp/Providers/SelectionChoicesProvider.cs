
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class SelectionChoicesProvider
    {


        string _ConnectionString;

        public SelectionChoicesProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<SelectionChoices> GetAllSelectionChoices(int selectionId)
        {
            List<SelectionChoices> AllSelectionChoices = new List<SelectionChoices>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllSelectionChoicesBySelectionId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SelectionId", selectionId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = new SelectionChoices();
                    newObject.ChoicesId = Convert.ToInt32(dataRow[SelectionChoices.CHOICE_ID_COLUMN]);
                    newObject.SelectionId = Convert.ToInt32(dataRow[SelectionChoices.CHOICE_SELECTION_ID_COLUMN]);
                    newObject.BusinessId = Convert.ToInt32(dataRow[SelectionChoices.CHOICE_BUSINESS_ID_COLUMN]);
                    newObject.ChoiceName = Convert.ToString(dataRow[SelectionChoices.CHOICE_NAME_COLUMN]);
                    newObject.ChoicePrice = Convert.ToInt32(dataRow[SelectionChoices.CHOICE_PRICE_COLUMN]);
                    newObject.ChoiceSortedBy = Convert.ToInt32(dataRow[SelectionChoices.CHOICE_SORT_BY_COLUMN]);
                    newObject.CreateDate = dataRow[SelectionChoices.CHOICE_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[SelectionChoices.CHOICE_CREATE_DATE_COLUMN]);
                    newObject.ModifyDate = dataRow[SelectionChoices.CHOICE_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[SelectionChoices.CHOICE_UPDATE_DATE_COLUMN]);
                    newObject.IsDeleted = Convert.ToBoolean(dataRow[SelectionChoices.CHOICE_DELETE_COLUMN]);
                    AllSelectionChoices.Add(newObject);
                }
            }
            catch (Exception ex)
            {
            }

            return AllSelectionChoices;
        }
        public object AddNewSelectionChoices(List<SelectionChoices> selectionChoices, int selectionId, int businessId)
        {

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewSelectionChoice";

            var statusChangedDateTime = DateTime.UtcNow;
            Boolean isUpdateCall = false;
            for(int i = 0; i < selectionChoices.Count; i++)
            {
                if (selectionChoices[i].ChoicesId != null && selectionChoices[i].ChoicesId > -1) {
                    isUpdateCall = true;
                    storedProcedureName = "SP_UpdateSelectionChoice";
                }
                else
                {
                    storedProcedureName = "SP_AddNewSelectionChoice";
                    isUpdateCall = false;
                }
                Dictionary<string, object> parameters = new Dictionary<string, object> {


                { "SelectionId", selectionId},
                { "BusinessId", businessId},
                { "ChoiceName", selectionChoices[i].ChoiceName },
                { "ChoicePrice", selectionChoices[i].ChoicePrice },
                { "ChoiceSortedBy", selectionChoices[i].ChoiceSortedBy },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", selectionChoices[i].IsDeleted },

            };
                if(isUpdateCall)
                    parameters.Add("ChoicesId", selectionChoices[i].ChoicesId);

                try
                {
                    dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
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


            return -1;

        }
        public object UpdateSelectionChoices(SelectionChoices selectionChoices)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateSelectionChoice";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "ChoicesId", selectionChoices.ChoicesId},
                { "SelectionId", selectionChoices.SelectionId},
                { "BusinessId", selectionChoices.BusinessId},
                { "ChoiceName", selectionChoices.ChoiceName },
                { "ChoicePrice", selectionChoices.ChoicePrice },
                { "ChoiceSortedBy", selectionChoices.ChoiceSortedBy },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", selectionChoices.IsDeleted },
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
        public object DeleteSelectionChoicesById(long choicesId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteSelectionChoicesById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "ChoicesId", choicesId}
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