using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FlyEatsApp.Models
{
    public class User
    {
        public const string USER_ID_COLUMN = "UserId";
        public const string FIRST_NAME_COLUMN = "FirstName";
        public const string LAST_NAME_COLUMN = "LastName";
        public const string EMAIL_COLUMN = "Email";
        public const string PHONE_NUMBER_COLUMN = "PhoneNumber";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string PASSWORD_HASH_COLUMN = "PasswordHash";
        public const string SALT_COLUMN = "Salt";
        public const string CREATED_AT_COLUMN = "CreatedAt";
        public const string UPDATED_AT_COLUMN = "UpdatedAt";


        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int? BusinessId { get; set; }

        public string? Password { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? Salt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public static User extractObj(DataRow dataRow)
        {
            User user = new User();
            user.UserId = (int)dataRow[USER_ID_COLUMN];
            user.FirstName = (string)dataRow[FIRST_NAME_COLUMN];
            user.LastName = (string)dataRow[LAST_NAME_COLUMN];
            user.Email = (string)dataRow[EMAIL_COLUMN];
            user.PhoneNumber = (string)dataRow[PHONE_NUMBER_COLUMN];
            user.BusinessId = (int)dataRow[BUSINESS_ID_COLUMN];
            user.PasswordHash = Convert.FromBase64String((string)dataRow["PasswordHash"]);
            user.Salt = Convert.FromBase64String((string)dataRow["Salt"]);
            user.CreatedAt = (DateTime)dataRow[CREATED_AT_COLUMN];
            user.UpdatedAt = (DateTime)dataRow[UPDATED_AT_COLUMN];
            return user;
        }


    }


}
