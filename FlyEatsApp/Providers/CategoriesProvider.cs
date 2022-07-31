
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
namespace FlyEatsApp.Providers
{
    public class CategoriesProvider
    {


        string _ConnectionString;
        public CategoriesProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<Categories> GetAllCategories(int businessId)
        {
            List<Categories> AllCategories = new List<Categories>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllCategoriesByBusinessId";
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
                    var categories = new Categories();
                    categories.CategoryId = Convert.ToInt32(dataRow[Categories.Category_ID_COLUMN]);
                    categories.BusinessId = Convert.ToInt32(dataRow[Categories.CATEGORY_BUSINESS_ID_COLUMN]);
                    categories.CategoryImage = Convert.ToString(dataRow[Categories.CATEGORY_IMAGE_COLUMN]);
                    categories.CategoryName = Convert.ToString(dataRow[Categories.CATEGORY_NAME_COLUMN]);
                    categories.CategoryDetails = Convert.ToString(dataRow[Categories.CATEGORY_DETAIL_COLUMN]);
                    categories.CategorySortBy = Convert.ToInt32(dataRow[Categories.CATEGORY_SORT_BY_COLUMN]);
                    categories.CreateDate = dataRow[Categories.CATEGORY_CREATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[Categories.CATEGORY_CREATE_DATE_COLUMN]);
                    categories.ModifyDate = dataRow[Categories.CATEGORY_UPDATE_DATE_COLUMN] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(dataRow[Categories.CATEGORY_UPDATE_DATE_COLUMN]);
                    categories.IsDeleted = Convert.ToBoolean(dataRow[Categories.CATEGORY_DELETE_COLUMN]);
                    categories.Active = Convert.ToBoolean(dataRow[Categories.CATEGORY_ACTIVE_COLUMN]);
                    AllCategories.Add(categories);
                }
            }
            catch (Exception ex)
            {
            }

            return AllCategories;
        }
        public object AddNewCategory(Categories categories)
        {

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewCategory";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {


                { "BusinessId", categories.BusinessId},
                { "CategoryImage", categories.CategoryImage },
                { "CategoryName", categories.CategoryName },
                { "CategoryDetails", categories.CategoryDetails },
                { "CategorySortBy", categories.CategorySortBy },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", categories.IsDeleted },
                { "Active", categories.Active },
            };

            try
            {
                var results = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return results;
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
        public object UpdateCategory(Categories categories)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateCategory";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "CategoryId", categories.CategoryId},
                { "BusinessId", categories.BusinessId},
                { "CategoryImage", categories.CategoryImage },
                { "CategoryName", categories.CategoryName },
                { "CategoryDetails", categories.CategoryDetails },
                { "CategorySortBy", categories.CategorySortBy },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", categories.IsDeleted },
                { "Active", categories.Active },
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
        public IList<Categories> GetCategoryById(int categoryId)
        {
            List<Categories> GetCategories = new List<Categories>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetCategoryDetailById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "CategoryId", categoryId }
               };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var category = Categories.ExtractObject(dataRow);
                    GetCategories.Add(category);
                }
            }
            catch (Exception ex)
            {

            }


            return GetCategories;

        }
        public object DeleteCategoryBy(long categoryId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteCategoryById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "CategoryId", categoryId}
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