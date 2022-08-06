
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class SelectionsProvider
    {


        string _ConnectionString;

        public SelectionsProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<Selections> GetAllSelections(int businessId)
        {
            List<Selections> AllSelections = new List<Selections>();
            SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
            var result = new Object();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllSelectionByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },

            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = new Selections();
                    newObject.SelectionId = Convert.ToInt32(dataRow[Selections.SELECTION_ID_COLUMN]);
                    newObject.BusinessId = Convert.ToInt32(dataRow[Selections.SELECTION_BUSINESS_ID_COLUMN]);
                    newObject.SelectionName = Convert.ToString(dataRow[Selections.SELECTION_NAME_COLUMN]);
                    newObject.MinimumSelection = Convert.ToInt32(dataRow[Selections.SELECTION_MINIMUM_COLUMN]);
                    newObject.MaximumSelection = Convert.ToInt32(dataRow[Selections.SELECTION_MAXIMUM_COLUMN]);
                    newObject.CreateDate = dataRow[Selections.SELECTION_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[Selections.SELECTION_CREATE_DATE_COLUMN]);
                    newObject.ModifyDate = dataRow[Selections.SELECTION_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[Selections.SELECTION_UPDATE_DATE_COLUMN]);
                    newObject.IsDeleted = Convert.ToBoolean(dataRow[Selections.SELECTION_DELETE_COLUMN]);
                    newObject.Active = Convert.ToBoolean(dataRow[Selections.SELECTION_ACTIVE_COLUMN]);
                    result = selectionChoicesProvider.GetAllSelectionChoices((int) newObject.SelectionId);
                    if (result != null)
                    {
                        newObject.selectionChocices = (List<SelectionChoices>?)result;
                    }
                    AllSelections.Add(newObject);
                }
            }
            catch (Exception ex)
            {
            }

            return AllSelections;
        }
        public object AddNewSelections(Selections selections)
        {
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewSelection";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {


                { "BusinessId", selections.BusinessId},
                { "SelectionName", selections.SelectionName },
                { "MinimumSelection", selections.MinimumSelection },
                { "MaximumSelection", selections.MaximumSelection },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", selections.IsDeleted },
                { "Active", selections.Active },
            };

            try
            {
                var productId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
                int _productId = (int)(productId == null ? -1 : Convert.ToInt64(productId));
                int _businessId = (int) selections.BusinessId;
                if (_productId > -1 && selections.selectionChocices != null && selections.selectionChocices.Count > 0)
                {
                    SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
                    selectionChoicesProvider.AddNewSelectionChoices(selections.selectionChocices, _productId, _businessId);
                }

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
        public object UpdateSelections(Selections selections)
        {
            var result = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateSelection";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "SelectionId", selections.SelectionId},
                { "BusinessId", selections.BusinessId},
                { "SelectionName", selections.SelectionName },
                { "MinimumSelection", selections.MinimumSelection },
                { "MaximumSelection", selections.MaximumSelection },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", selections.IsDeleted },
                { "Active", selections.Active },
            };

            try
            {
                result = (ResponseModel) dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                if (result.success)
                {
                    int selectionId = (int) selections.SelectionId;
                    int _businessId = (int) selections.BusinessId;
                    if (selectionId > -1 && selections.selectionChocices != null && selections.selectionChocices.Count > 0)
                    {
                        SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
                        selectionChoicesProvider.AddNewSelectionChoices(selections.selectionChocices, selectionId, _businessId);
                    }
                }
                return result.onSuccess();
            }
            catch (Exception ex)
            {
                return result.onError(ex.Message);
            }


            return false;
        }
        public IList<Selections> GetSelectionsById(int selectionId)
        {
            List<Selections> GetSelection = new List<Selections>();
            SelectionChoicesProvider selectionChoicesProvider = new SelectionChoicesProvider();
            var result = new Object();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetSelectionById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "SelectionId", selectionId }
               };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var selections = Selections.ExtractObject(dataRow);
                    result = selectionChoicesProvider.GetAllSelectionChoices((int) selections.SelectionId);
                    if(result != null)
                    {
                        selections.selectionChocices = (List<SelectionChoices>?) result;
                    }
                    GetSelection.Add(selections);
                }
            }
            catch (Exception ex)
            {

            }


            return GetSelection;

        }
        public object DeleteSelectionsBy(long selectionId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteSelectionById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "SelectionId", selectionId}
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