using System.Data;

namespace FlyEatsApp.Models
{
    public class InternalUser
    {
        public const string ID_COLUMN = "Id";
        public const string BUSINESS_ID_COLUMN = "BusinessId";
        public const string FULL_NAME_COLUMN = "FullName";
        public const string USERNAME_COLUMN = "Username";
        public const string EMAIL_COLUMN = "Email";
        public const string PASSWORD_COLUMN = "Password";
        public const string MOBILE_NUMBER_COLUMN = "MobileNumber";
        public const string ACCOUNT_TYPE_COLUMN = "AccountType";
        public const string ROLE_COLUMN = "Role";
        public const string CREATION_DATE_COLUMN = "CreationDate";
        public const string UPDATE_DATE_COLUMN = "UpdateDate";
        public const string IS_DELETED_COLUMN = "IsDeleted";
        public const string ACTIVE_COLUMN = "Active";

        public int? Id { get; set; }
        public int BusinessId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public int AccountType { get; set; }
        public int Role { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool Active { get; set; }

        public string? Token { get; set; }

        public static InternalUser ExtractFromDataRow(DataRow dataRow)
        {
            InternalUser internalUser = new InternalUser();
            internalUser.Id = (int)dataRow[ID_COLUMN];
            internalUser.BusinessId = (int)dataRow[BUSINESS_ID_COLUMN];
            internalUser.FullName = (string)dataRow[FULL_NAME_COLUMN];
            internalUser.Username = (string)dataRow[USERNAME_COLUMN];
            internalUser.Email = (string)dataRow[EMAIL_COLUMN];
            internalUser.Password = (string)dataRow[PASSWORD_COLUMN];
            internalUser.MobileNumber = (string)dataRow[MOBILE_NUMBER_COLUMN];
            internalUser.AccountType = (int)dataRow[ACCOUNT_TYPE_COLUMN];
            internalUser.Role = (int)dataRow[ROLE_COLUMN];
            internalUser.CreationDate = (DateTime?)dataRow[CREATION_DATE_COLUMN];
            internalUser.UpdateDate = (DateTime?)dataRow[UPDATE_DATE_COLUMN];
            internalUser.IsDeleted = (bool)dataRow[IS_DELETED_COLUMN];
            internalUser.Active = (bool)dataRow[ACTIVE_COLUMN];
            return internalUser;
        }
    }

}
