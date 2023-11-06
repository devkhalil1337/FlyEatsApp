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

        public ResponseModel AddAddress(AddressBook address)
        {
            ResponseModel response = new ResponseModel();
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
                response = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }
            return response;
        }

        public ResponseModel UpdateAddress(AddressBook address)
        {
            ResponseModel response = new ResponseModel();
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
                response = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);

            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }


            return response;
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


        public ResponseModel DeleteAddress(int addressId)
        {
            ResponseModel response = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteAddress";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "AddressId", addressId },
            { "Active", false }
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
