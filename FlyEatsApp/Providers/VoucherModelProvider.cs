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
    public class VoucherModelProvider
    {
        string _ConnectionString;

        public VoucherModelProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public IList<VoucherModel> GetVouchers(int businessId)
        {
            List<VoucherModel> vouchers = new List<VoucherModel>();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetVouchersByBusinessId";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "BusinessId", businessId },
        };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<VoucherModel>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    var newObject = VoucherModel.ExtractObject(dataRow);
                    vouchers.Add(newObject);
                }
            }
            catch (Exception ex)
            {
                // handle exception
            }

            return vouchers;
        }

        public VoucherModel AddVoucher(VoucherModel voucher)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddVoucher";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherCode", voucher.VoucherCode },
            { "VoucherType", voucher.VoucherType },
            { "DiscountAmount", voucher.DiscountAmount },
            { "MaxRedemption", voucher.MaxRedemption },
            { "ExpiryDate", voucher.ExpiryDate },
            { "BusinessId", voucher.BusinessId }
        };
            try
            {
                dataAccessProvider.ExecuteNonQueryStoredProcedure(storedProcedureName, parameters);
                return voucher;
            }
            catch (Exception ex)
            {
                // handle exception
                return new VoucherModel();
            }
        }

        public VoucherModel UpdateVoucher(VoucherModel voucher)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateVoucher";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherId", voucher.VoucherId },
            { "VoucherCode", voucher.VoucherCode },
            { "VoucherType", voucher.VoucherType },
            { "DiscountAmount", voucher.DiscountAmount },
            { "MaxRedemption", voucher.MaxRedemption },
            { "ExpiryDate", voucher.ExpiryDate },
            { "BusinessId", voucher.BusinessId }
        };
            try
            {
                dataAccessProvider.ExecuteNonQueryStoredProcedure(storedProcedureName, parameters);
                return voucher;
            }
            catch (Exception ex)
            {
                // handle exception
                return new VoucherModel();
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

        public VoucherModel GetVoucherById(int voucherId)
        {
            VoucherModel voucher = null;
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetVoucherById";
            Dictionary<string, object> parameters = new Dictionary<string, object> {
            { "VoucherId", voucherId },
        };
            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new VoucherModel();

                voucher = VoucherModel.ExtractObject(dataSet.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                // Handle the exception
            }

            return voucher;
        }

    }
}
