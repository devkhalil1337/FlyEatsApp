using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FlyEatsApp.Models
{
    public class AppUser 
    {
        private const string USER_ID_COLUMN = "Id";
        private const string BUSINESS_ID_COLUMN = "businessId";
        private const string FIRST_NAME_COLUMN = "firstName";
        private const string LAST_NAME_COLUMN = "lastName";
        private const string USERNAME_COLUMN = "userName";
        private const string USER_EMAIL_COLUMN = "Email";
        private const string USER_PASSWORD_COLUMN = "Password";
        private const string USER_ACCOUNT_TYPE_COLUMN = "AccountType";
        
        public enum AccountType
        {
            Admin = 1,
            Driver,
            System
        }

        public long? Id { get; set; }
        public long? BusinessId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? MobileNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public AccountType? accountType  { get; set; }
        public int? Role { get; set; }

        public DateTime? CreateDate { get; set; }
        public string? CreationDate
        {
            get
            {
                return CreateDate.HasValue ? CreateDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : null;
            }

            set
            {
                if (value != null)
                {
                    CreateDate = DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public DateTime? ModifyDate { get; set; }
        public string? UpdateDate
        {
            get { return ModifyDate.HasValue ? ModifyDate.Value.ToString("yyyy-MM-dd hh:mm:ss") : null; }

            set
            {
                if (value != null)
                {
                    ModifyDate = DateTime.ParseExact(value, "yyyy-MM-dd hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public bool? Deleted { get; set; }
        public bool? Active { get; set; }


        public string? Token { get; set; }

        public static AppUser ExtractObject(DataRow dataRow)
        {
            var appUser = new AppUser();
            appUser.Id = dataRow[USER_ID_COLUMN] == DBNull.Value ? null : (long?)Convert.ToInt64(dataRow[USER_ID_COLUMN]);
            appUser.BusinessId = Convert.ToInt32(dataRow[BUSINESS_ID_COLUMN]);
            appUser.FirstName = Convert.ToString(dataRow[FIRST_NAME_COLUMN]);
            appUser.LastName = Convert.ToString(dataRow[LAST_NAME_COLUMN]);
            appUser.UserName = Convert.ToString(dataRow[USERNAME_COLUMN]);
            appUser.Email = Convert.ToString(dataRow[USER_EMAIL_COLUMN]);
            appUser.Password = Convert.ToString(dataRow[USER_PASSWORD_COLUMN]);
            appUser.accountType = (AppUser.AccountType)Convert.ToInt32(dataRow[USER_ACCOUNT_TYPE_COLUMN]);
            return appUser;
        }
    }
}