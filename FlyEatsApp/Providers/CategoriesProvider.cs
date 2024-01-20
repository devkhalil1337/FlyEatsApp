
using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Configuration;
using log4net;
using log4net.Core;

namespace FlyEatsApp.Providers
{
    public class CategoriesProvider
    {


        string _ConnectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(CategoriesProvider));
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
                    return new List<Categories>(); ;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    AllCategories.Add(Categories.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(CategoriesProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while fetching all category: " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                Console.WriteLine("An error occurred while fetching all category: " + ex.Message);
                return new List<Categories>(); ;
            }

            return AllCategories;
        }
        public object AddNewCategory(Categories categories)
        {
            ResponseModel results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewCategory";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {


                { "BusinessId", categories.BusinessId},
                { "CategoryImage", categories.CategoryImage },
                { "CategoryName", categories.CategoryName },
                { "CategoryDetails", categories.CategoryDetails },
                { "CategoryType", categories.CategoryType },
                { "CategorySortBy", categories.CategorySortBy },
                { "CreationDate", statusChangedDateTime },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", categories.IsDeleted },
                { "Active", categories.Active },
            };

            try
            {
                dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                results.success = true;
                results.message = "";
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(CategoriesProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while adding new category: " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                Console.WriteLine("An error occurred while adding new category: " + ex.Message);
                results.success = false;
                results.message = ex.Message;
            }

            return results;

        }
        public object UpdateCategory(Categories categories)
        {
            ResponseModel results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateCategory";

            var statusChangedDateTime = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "CategoryId", categories.CategoryId},
                { "BusinessId", categories.BusinessId},
                { "CategoryImage", categories.CategoryImage },
                { "CategoryName", categories.CategoryName },
                { "CategoryDetails", categories.CategoryDetails },
                { "CategoryType", categories.CategoryType },
                { "CategorySortBy", categories.CategorySortBy },
                { "UpdateDate", statusChangedDateTime },
                { "IsDeleted", categories.IsDeleted },
                { "Active", categories.Active },
            };

            try
            {
                dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                results.success = true;
                results.message = "";
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(CategoriesProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while updating category: " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                Console.WriteLine("An error occurred while updating category: " + ex.Message);
                results.success = false;
                results.message = ex.Message;
            }
            return results;
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
                    return new List<Categories>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var category = Categories.ExtractObject(dataRow);
                    GetCategories.Add(category);
                }
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(CategoriesProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while fetching new category: " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                Console.WriteLine("An error occurred while fetching category: " + ex.Message);
                return new List<Categories>();
            }


            return GetCategories;

        }
        public object DeleteCategoryBy(long categoryId)
        {
            ResponseModel response = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteCategoryById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "CategoryId", categoryId}
               };

            try
            {
                dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                response.success = true;
                response.message = "";
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(CategoriesProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while deleting category: " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                Console.WriteLine("An error occurred while deleting category: " + ex.Message);
                response.success = false;
                response.message = ex.Message;

            }
            return response;
        }
    }
}