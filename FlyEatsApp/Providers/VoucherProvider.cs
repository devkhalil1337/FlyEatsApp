using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;


namespace FlyEatsApp.Providers
{
    public class VoucherProvider
    {

        string _ConnectionString;

        public VoucherProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }
        public Voucher GetVoucherById(int voucherId)
        {
            Voucher selectedVoucher = new Voucher();
            var storedProcedureName = "SP_GetVoucherById";
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherId", voucherId }
        };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new Voucher();

                selectedVoucher = Voucher.ExtractObject(dataSet.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return selectedVoucher;
        }

        public int CheckVoucherRedemptionEligibility(string voucherCode, int userId,decimal billAmount)
        {
            Voucher selectedVoucher = new Voucher();
            var storedProcedureName = "SP_CheckVoucherRedemptionEligibility";
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherCode", voucherCode },
            { "UserId", userId },
            { "Amount", billAmount }
                
        };

            try
            {
                var dataSet = dataAccessProvider.ExecuteScalarStoredProcedure(storedProcedureName, parameters);
                return  (int)Convert.ToInt64(dataSet);
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return 0;
        }

        public List<Voucher> GetAllVouchersByBusinessId(int businessId)
        {
            List<Voucher> vouchers = new List<Voucher>();
            var storedProcedureName = "SP_GetAllVouchersByBusinessId";
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object> {
                    { "BusinessId", businessId }
                };
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return vouchers;

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var voucher = Voucher.ExtractObject(dataRow);
                    vouchers.Add(voucher);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return vouchers;
        }

        public int AddVoucher(Voucher voucher)
        {
            var storedProcedureName = "SP_CreateVoucher";
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherCode", voucher.VoucherCode },
            { "MinValue", voucher.MinValue },
            { "MaxValue", voucher.MaxValue },
            { "StartDate", voucher.StartDate },
            { "EndDate", voucher.EndDate },
            { "CreatedBy", voucher.CreatedBy },
            { "BusinessId", voucher.BusinessId },
            { "IsActive", voucher.IsActive },
            { "RedeemCount", voucher.RedeemCount }
        };

            try
            {
                var rowsAffected = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                return (int)Convert.ToInt64(rowsAffected);
            }
            catch (Exception ex)
            {
                // Handle the exception
                return -1;
            }
        }

        public object UpdateVoucher(Voucher voucher)
        {
            var results = new ResponseModel();
            var storedProcedureName = "SP_UpdateVoucher";
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherId", voucher.VoucherId },
            { "VoucherCode", voucher.VoucherCode },
            { "MinValue", voucher.MinValue },
            { "MaxValue", voucher.MaxValue },
            { "StartDate", voucher.StartDate },
            { "EndDate", voucher.EndDate },
            { "CreatedBy", voucher.CreatedBy },
            { "BusinessId", voucher.BusinessId },
            { "IsActive", voucher.IsActive },
            { "RedeemCount", voucher.RedeemCount }
        };

            try
            {
                var rowsAffected = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);

                return results.onSuccess();
            }
            catch (Exception ex)
            {
                // Handle the exception
                return results.onError(ex.Message);
            }
        }

        public bool DeleteVoucher(int voucherId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DeleteVoucher";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherId", voucherId }
        };
            try
            {
                dataAccessProvider.ExecuteNonQueryStoredProcedure(storedProcedureName, parameters);
                return true;
            }
            catch (Exception ex)
            {
                // handle exception
                return false;
            }
        }



        public Voucher GetVoucherIdByCode(string voucherCode)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            Voucher voucher = null;
            var storedProcedureName = "SP_GetVoucherByCode";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherCode", voucherCode }
        };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new Voucher();

                voucher = Voucher.ExtractObject(dataSet.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                // handle exception
            }
            return voucher;
        }
    }
}
