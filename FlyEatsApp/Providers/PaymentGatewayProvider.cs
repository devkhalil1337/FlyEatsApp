using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;

namespace FlyEatsApp.Providers
{
    public class PaymentGatewayProvider
    {
        string _ConnectionString;

        public PaymentGatewayProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        //Client Side API
        public IList<PaymentGatewayKeys> GetPaymentGatewaysKeys(int businessId)
        {
            List<PaymentGatewayKeys> allPaymentGateways = new List<PaymentGatewayKeys>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetPaymentGatewaysByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                    { "BusinessId", businessId },
                };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<PaymentGatewayKeys>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    allPaymentGateways.Add(PaymentGatewayKeys.ExtractFromDataRow(dataRow));
                }
            }
            catch (Exception ex)
            {
                // handle exception
            }

            return allPaymentGateways;
        }

        public IList<PaymentGateway> GetAllPaymentGateways(int businessId)
        {
            List<PaymentGateway> allPaymentGateways = new List<PaymentGateway>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetPaymentGatewaysByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                    { "BusinessId", businessId },
                };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<PaymentGateway>();
                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    allPaymentGateways.Add(PaymentGateway.ExtractFromDataRow(dataRow));
                }
            }
            catch (Exception ex)
            {
                // handle exception
            }

            return allPaymentGateways;
        }

        public bool AddPaymentGateway(PaymentGateway paymentGateway)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "sp_InsertPaymentGatewayConfig";
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "BusinessId", paymentGateway.BusinessId },
                { "GatewayName", paymentGateway.GatewayName },
                { "ApiKey", paymentGateway.ApiKey },
                { "ApiSecret", paymentGateway.ApiSecret },
                { "IsActive", paymentGateway.IsActive },
                { "PaymentMode", paymentGateway.PaymentMode }
            };
            try
            {
                var id = dataAccessProvider.ExecuteScalarStoredProcedure(storedProcedureName, parameters);
                
                return id != null ? true:false;
            }
            catch (Exception ex)
            {
                // Log the exception
                return false;
            }
        }

        public bool UpdatePaymentGateway(PaymentGateway paymentGateway)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "sp_UpdatePaymentGatewayConfig";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "Id", paymentGateway.Id },
                { "BusinessId", paymentGateway.BusinessId },
                { "GatewayName", paymentGateway.GatewayName },
                { "ApiKey", paymentGateway.ApiKey },
                { "ApiSecret", paymentGateway.ApiSecret },
                { "IsActive", paymentGateway.IsActive },
                { "PaymentMode", paymentGateway.PaymentMode },
            };
            try
            {
                var rowsAffected = dataAccessProvider.ExecuteScalarStoredProcedure(storedProcedureName, parameters);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                // log exception here
                return false;
            }
        }


    }
}
