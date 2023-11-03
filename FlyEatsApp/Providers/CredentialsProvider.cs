
using FlyEatsApp.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace FlyEatsApp.Providers
{
    public class CredentialsProvider
    {
        public enum UserStatus
        {
            DoesNotExist,
            ExistsWithPassword,
            ExistsButWrongPasswordSpecified
        }

        string _ConnectionString;

        public CredentialsProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public long CanUserLogin(string userName, string password)
        {
            ResponseModel response = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "sp_GetInternalUserByCredentials";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "Username", userName },
                { "Password", password }
            };

            try
            {
                response = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);

                return response.GetId();

            }
            catch (Exception ex)
            {
                response.success = false;
                response.message = ex.Message;
            }

            return -1;
        }

        public bool DoesUserExist(string email)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_DoesUserExist";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "email", email }
            };

            try
            {
                var userId = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);

                return userId != null;

            }
            catch (Exception ex)
            {
               
            }


            return false;
        }

        public long CreateNewUser(InternalUser user)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "sp_AddInternalUser";

            var joinDate = DateTime.UtcNow;

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", user.BusinessId },
                { "FullName", user.FullName },
                { "Username", user.Username },
                { "Email", user.Email },
                { "Password", user.Password },
                { "MobileNumber", user.MobileNumber },
                { "AccountType", user.AccountType },
                { "Role", user.Role },
                { "CreationDate", joinDate },
                { "UpdateDate", joinDate },
                { "IsDeleted", user.IsDeleted },
                { "Active", user.Active }
                };

            try
            {
                var userId = dataAccessProvider.ExecuteScalarStoredProcedure(storedProcedureName, parameters);

                return userId == null ? -1 : Convert.ToInt64(userId);

            }
            catch (Exception ex)
            {
                /*LogEntry logEntry = new LogEntry()
                {
                    Severity = System.Diagnostics.TraceEventType.Error,
                    Title = "Agency User SignUp",
                    Message = ex.Message + Environment.NewLine + ex.StackTrace
                };
                Logger.Write(logEntry);*/
            }


            return -1;
        }

        public InternalUser GetUser(string username)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetUserByUserName";

            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "Username", username }
            };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                DataRow dataRow = dataSet.Tables[0].Rows[0];

                var appUser = InternalUser.ExtractFromDataRow(dataRow);
                return appUser;
            }
            catch (Exception ex)
            {
                LogEntry logEntry = new LogEntry()
                {
                    Severity = System.Diagnostics.TraceEventType.Error,
                    Title = string.Format("Get User with email: {0}", username),
                    Message = ex.Message + Environment.NewLine + ex.StackTrace
                };
                Logger.Write(logEntry);
            }


            return null;
        }

        public List<InternalUser> GetAllInternalUserByBusinessId(int businessId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetAllInternalUserByBusinessId";
            List<InternalUser> internalUsers = new List<InternalUser>();
            Dictionary<string, object> parameters = new Dictionary<string, object> {
                { "BusinessId", businessId }
            };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return new List<InternalUser>();

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    internalUsers.Add(InternalUser.ExtractFromDataRow(dataRow));
                }

                return internalUsers;
            }
            catch (Exception ex)
            {
               
            }

            return new List<InternalUser>();
        }

       

       /* public bool RequestPasswordResetCode(string email)
        {
            try
            {
                var emailServerName = ConfigurationManager.AppSettings["EmailServerName"];
                var emailServerPort = Convert.ToInt32(ConfigurationManager.AppSettings["EmailServerPort"]);
                var emailAddress = ConfigurationManager.AppSettings["ProfileMyDriverFromEmailAddress"];
                var emailAddressPassword = ConfigurationManager.AppSettings["ProfileMyDriverFromEmailAddressPassword"];

                DateTime today = DateTime.UtcNow;
                DateTime tomorrow = today.AddDays(1);
                string code = string.Format("{0}£{1}£{2}", email, today.ToString("yyyyMMdd"), tomorrow.ToString("yyyyMMdd"));

                string encryptedCode = CipherProvider.EncryptResetCode(code);

                var client = new SmtpClient(emailServerName)
                {
                    Credentials = new NetworkCredential(emailAddress, emailAddressPassword),
                    EnableSsl = true
                };
                client.DeliveryMethod = SmtpDeliveryMethod.Network;

                StringBuilder body = new StringBuilder();
                body.Append("Hello,");
                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);

                body.Append("You have requested to reset your ProfileMyDriver® password. In order to reset your password, you will need to use the following code on the ProfileMyDriver® website.");
                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);

                body.AppendFormat("ProfileMyDriver® Reset Code: {0}", encryptedCode);

                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);

                body.Append("Yours sincerely,");
                body.Append(Environment.NewLine);
                body.Append(Environment.NewLine);

                body.Append("ProfileMyDriver® Admin Team");

                client.Send(emailAddress, email, "Password Reset Code", body.ToString());

                return true;
            }
            catch (Exception ex)
            {
                LogEntry logEntry = new LogEntry()
                {
                    Severity = System.Diagnostics.TraceEventType.Error,
                    Title = string.Format("Sending password reset code for User with Email: {0}", email),
                    Message = ex.Message + Environment.NewLine + ex.StackTrace
                };
                Logger.Write(logEntry);

                return false;
            }
        }
       */
        public int CheckUserExists(string email, string password)
        {
            return (int)UserExists(email, password);
        }

        private UserStatus UserExists(string email, string password)
        {
            var user = GetUser(email);

            var returnValue = UserStatus.DoesNotExist;

            if (user != null)
            {
                if (user.Password == password)
                {
                    returnValue = UserStatus.ExistsWithPassword;
                }
                else
                {
                    returnValue = UserStatus.ExistsButWrongPasswordSpecified;
                }
            }

            return returnValue;
        }

        public bool UpdatePassword(string email, string newPassword, string resetCode)
        {
            try
            {
                string decryptedCode = CipherProvider.DecryptResetCode(resetCode);

                string[] parts = decryptedCode.Split('?');

                int fromDate = Convert.ToInt32(parts[1]);
                int toDate = Convert.ToInt32(parts[2]);

                int today = Convert.ToInt32(DateTime.UtcNow.ToString("yyyyMMdd"));

                if (today >= fromDate && today <= toDate)
                {
                    var userExists = UserExists(parts[0], "");

                    if (userExists == UserStatus.ExistsButWrongPasswordSpecified)
                    {
                        IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
                        var storedProcedureName = "sp_UpdateUserPassword";

                        Dictionary<string, object> parameters = new Dictionary<string, object> {
                            { "email", parts[0] },
                            { "newPassword", newPassword }
                        };

                        try
                        {
                            var result = dataAccessProvider.ExecuteNonQueryStoredProcedure(storedProcedureName, parameters);

                            return result;

                        }
                        catch (Exception ex)
                        {
                            LogEntry logEntry = new LogEntry()
                            {
                                Severity = System.Diagnostics.TraceEventType.Error,
                                Title = string.Format("Change Password for User: {0}", email),
                                Message = ex.Message + Environment.NewLine + ex.StackTrace
                            };
                            Logger.Write(logEntry);
                        }

                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return false;
        }
    }
}