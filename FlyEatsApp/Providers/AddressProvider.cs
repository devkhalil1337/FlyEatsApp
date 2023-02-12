using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class AddressProvider
    {
        string _ConnectionString;

        public AddressProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public List<AddressBook> GetAddressesByUserId(int userId)
        {
            var addresses = new List<AddressBook>();
            var dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAddressesForUser";
            var parameters = new Dictionary<string, object>
        {
            { "UserId", userId }
        };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                {
                    return new List<AddressBook>();
                }

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    addresses.Add(AddressBook.Extract(dataRow));
                }
            }
            catch (Exception ex)
            {
                return new List<AddressBook>();
            }

            return addresses;
        }

        public int AddAddress(AddressBook address)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddAddress";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "UserId", address.UserId },
                { "AddressLine1", address.AddressLine1 },
                { "AddressLine2", address.AddressLine2 },
                { "City", address.City },
                { "StateProvince", address.StateProvince },
                { "ZipPostalCode", address.ZipPostalCode },
                { "Country", address.Country },
                { "Latitude", address.Latitude },
                { "Longitude", address.Longitude },
                { "PhoneNumber", address.PhoneNumber },
                { "AddressType", address.AddressType },
                { "Active", address.Active },
            };

            try
            {
                var result = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
                if (result == null)
                    return 0;
                return (int)Convert.ToInt64(result);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool UpdateAddress(AddressBook address)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateAddress";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "AddressId", address.AddressId },
                { "AddressLine1", address.AddressLine1 },
                { "AddressLine2", address.AddressLine2 },
                { "City", address.City },
                { "StateProvince", address.StateProvince },
                { "ZipPostalCode", address.ZipPostalCode },
                { "Country", address.Country },
                { "Latitude", address.Latitude },
                { "Longitude", address.Longitude },
                { "PhoneNumber", address.PhoneNumber },
                { "AddressType", address.AddressType },
                { "Active", address.Active }
            };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return false;
        }

        public AddressBook GetAddressById(int addressId)
        {
            AddressBook selectedAddress = new AddressBook();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAddressByAddressId";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
           { "AddressId", addressId }
       };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new AddressBook();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    selectedAddress = AddressBook.Extract(dataRow);
                }
            }
            catch (Exception ex)
            {

            }

            return selectedAddress;
        }


        public void DeleteAddress(int addressId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteAddress";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "AddressId", addressId },
            { "Active", false }
        };

        try
        {
           dataAccessProvider.ExecuteStoredProcedureWithReturnMessage(storedProcedureName, parameters);
        }
        catch (Exception ex)
        {
            // Log the error
            }
        }




    }
}
