using FlyEatsApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using DataAccessLayer;
using Microsoft.Extensions.Logging.Abstractions;
using System.Security.Cryptography;
using log4net;
using log4net.Core;

namespace FlyEatsApp.Providers
{
    public class UserProvider
    {
        string _ConnectionString;
        private static readonly ILog logger = LogManager.GetLogger(typeof(UserProvider));
        public UserProvider()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true);
            _ConnectionString = builder.Build().GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public User GetUserById(int userId)
        {
            User user = null;
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "sp_GetCustomerById";
            var parameters = new Dictionary<string,
              object> {
          {
            "UserId",
            userId
          }
        };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return user;

                user = User.extractObj(dataSet.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {

            }

            return user;
        }

        public ResponseModel InsertUser(User user)
        {
            byte[] salt = GenerateSalt();
            string saltString = Convert.ToBase64String(salt);
            byte[] passwordHash = HashPasswordDBPassword(user.Password, salt);
            string passwordHashString = Convert.ToBase64String(passwordHash);
            var results = new ResponseModel();
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_AddNewCustomer";
            var parameters = new Dictionary<string,
              object> {
          {
            "@firstName",
            user.FirstName
          },
          {
            "@lastName",
            user.LastName
          },
          {
            "@email",
            user.Email
          },
          {
            "@phoneNumber",
            user.PhoneNumber
          },
          {
            "@businessId",
            user.BusinessId
          },
          {
            "@passwordHash",
            passwordHashString
          },
          {
            "@salt",
            saltString
          }
        };

            try
            {
                results = dataAccessProvider.ExecuteStoredProcedureWithReturnObject(storedProcedureName, parameters);
            }
            catch (Exception ex)
            {
                var logEntry = new LoggingEvent(typeof(OrderProvider), logger.Logger.Repository, "logger", Level.Error, "An error occurred while trying to Creating New User : " + ex.Message + Environment.NewLine + ex.StackTrace, null); // Exception
                logger.Logger.Log(logEntry);
                return results;
            }
            return results;
        }

        public bool UpdateUser(User user)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_UpdateCustomer";
            string saltString = Convert.ToBase64String(user.Salt);
            string passwordHashString = Convert.ToBase64String(user.PasswordHash);

            byte[] salt = GenerateSalt();
            if(user.Password != null)
            {
               saltString = Convert.ToBase64String(salt);
               byte[] passwordHash = HashPasswordDBPassword(user.Password, salt);
               passwordHashString = Convert.ToBase64String(passwordHash);
            }

            var parameters = new Dictionary<string,
              object> {
          {
            "@UserId",
            user.UserId
          },
          {
            "@FirstName",
            user.FirstName
          },
          {
            "@LastName",
            user.LastName
          },
          {
            "@Email",
            user.Email
          },
          {
            "@PhoneNumber",
            user.PhoneNumber
          },
          {
            "@BusinessId",
            user.BusinessId
          },
          {
            "@PasswordHash",
            passwordHashString
          },
          {
            "@Salt",
            saltString
          }
        };

            try
            {
                var rowsAffected = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);
                return (int)Convert.ToInt64(rowsAffected) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "sp_DeleteCustomer";
            var parameters = new Dictionary<string,
              object> {
          {
            "@UserId",
            userId
          }
        };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public object AuthenticateUser(string email, string password,int businessId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetCustomerByEmail";
            var parameters = new Dictionary<string,object> 
            {
                {"email",  email},
                {"businessId",businessId }
            };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                var dataRow = dataSet.Tables[0].Rows[0];
                var user = User.extractObj(dataRow);
                /*if(user.BusinessId != businessId)
                {
                    return null;
                }*/
                user.PasswordHash = Convert.FromBase64String((string)dataRow["PasswordHash"]);
                user.Salt = Convert.FromBase64String((string)dataRow["Salt"]);
                if (VerifyPassword(password, user.Salt, user.PasswordHash))
                {
                    return user;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUserByEmail(string email, int businessId)
        {
            IDatabaseAccessProvider dataAccessProvider = new SqlDataAccess(_ConnectionString);
            var storedProcedureName = "SP_GetCustomerByEmail";
            var parameters = new Dictionary<string, object>
            {
                {"email",  email},
                {"businessId",businessId }
            };

            try
            {
                var dataSet = dataAccessProvider.ExecuteStoredProcedure(storedProcedureName, parameters);

                if (dataSet.Tables.Count < 1 || dataSet.Tables[0].Rows.Count < 1)
                    return null;

                var dataRow = dataSet.Tables[0].Rows[0];
                var user = User.extractObj(dataRow);
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return salt;
        }

        public static byte[] HashPassword(byte[] password, byte[] salt)
        {
            byte[] hashPassword;
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = ConcatenateArrays(password, salt);
                hashPassword = sha256.ComputeHash(saltedPassword);
            }

            return hashPassword;
        }

        private static byte[] ConcatenateArrays(byte[] a, byte[] b)
        {
            var result = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, result, 0, a.Length);
            Buffer.BlockCopy(b, 0, result, a.Length, b.Length);

            return result;
        }

        public static bool VerifyPassword(string password, byte[] salt, byte[] hashPassword)
        {
            var newHashPassword = HashPasswordDBPassword(password, salt);
            return ConstantTimeComparison(newHashPassword, hashPassword);
        }

        public static byte[] HashPasswordDBPassword(string password, byte[] salt)
        {
            byte[] hashPassword;
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + Convert.ToBase64String(salt);
                hashPassword = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(saltedPassword));
            }

            return hashPassword;
        }

        private static bool ConstantTimeComparison(byte[] a, byte[] b)
        {
            int diff = a.Length ^ b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }

            return diff == 0;
        }

    }
}