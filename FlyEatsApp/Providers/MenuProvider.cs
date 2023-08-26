using FlyEatsApp.Models;
using DataAccessLayer;
using System.Data;

namespace FlyEatsApp.Providers
{
    public class MenusProvider
    {
        string _ConnectionString;

        public MenusProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<Menus> GetAllMenus(int businessId)
        {
            List<Menus> AllMenus = new List<Menus>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllMenusByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId },
            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<Menus>(); ;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    AllMenus.Add(Menus.ExtractObject(dataRow));
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return AllMenus;
        }

        public object AddNewMenu(Menus menu)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewMenu";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", menu.BusinessId },
                { "MenuName", menu.MenuName },
                { "MenuUrl", menu.MenuUrl },
                { "OrderBy", menu.OrderBy },
                { "isActive", menu.isActive },
            };

            try
            {
                var results = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return results;
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return -1;
        }

        public object UpdateMenu(Menus menu)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateMenu";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "Id", menu.Id },
                { "BusinessId", menu.BusinessId },
                { "MenuName", menu.MenuName },
                { "MenuUrl", menu.MenuUrl },
                { "OrderBy", menu.OrderBy },
                { "isActive", menu.isActive },
            };

            try
            {
                var result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return result;
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return false;
        }


        public object UpdateBulkMenus(List<Menus> menus)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateMenu";

            List<object> updateResults = new List<object>();

            foreach (var menu in menus)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "Id", menu.Id },
            { "BusinessId", menu.BusinessId },
            { "MenuName", menu.MenuName },
            { "MenuUrl", menu.MenuUrl },
            { "OrderBy", menu.OrderBy },
            { "isActive", menu.isActive },
        };

                try
                {
                    var result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                    updateResults.Add(result);
                }
                catch (Exception ex)
                {
                    // Handle the exception
                    updateResults.Add(false);
                }
            }

            return updateResults;
        }


        public IList<Menus> GetMenuById(int menuId)
        {
            List<Menus> GetMenus = new List<Menus>();

            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetMenuDetailById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "MenuId", menuId }
               };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var menu = Menus.ExtractObject(dataRow);
                    GetMenus.Add(menu);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return GetMenus;
        }

        public object DeleteMenuById(int menuId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteMenuById";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                   { "MenuId", menuId }
               };

            try
            {
                var result = dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
                return result;
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return false;
        }
    }
}
