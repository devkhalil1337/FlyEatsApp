using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using static FlyEatsApp.Models.CredentialsResult;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FlyEatsApp.Functions;
using DataAccessLayer;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]/[action]")]
    public class InternalUsersController : Controller
    {
        [HttpPost]
        public IActionResult CheckUserCredentials([FromBody] LoginModel loginModel)
        {
            CredentialsResult credentialsResult = new CredentialsResult();

            CredentialsProvider credentialsProvider = new CredentialsProvider();

            string encryptedPassword = CipherProvider.EncryptPassword(loginModel.Password);


            var results = credentialsProvider.CanUserLogin(loginModel.UserName, encryptedPassword);

            if (results != null)
            {
                credentialsResult.Result = CredentialsStatus.Success;

                var user = credentialsProvider.GetUser(loginModel.UserName);

                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                user.Token = tokenString;
                return Ok(user);

            }
            credentialsResult.Result = CredentialsStatus.UserDoesNotExist;
            return Ok(new CredentialsResult { Result = credentialsResult.Result });
        }

        [HttpPost]
        public IActionResult AddNewCreate([FromBody] InternalUser user)
        {
            var results = new ResponseModel();
            CredentialsResult credentialsResult = new CredentialsResult();

            CredentialsProvider credentialsProvider = new CredentialsProvider();



            user.Password = CipherProvider.EncryptPassword(user.Password);
            if(user.Id != null)
            {
                results = credentialsProvider.UpdateInternalUser(user);
            }
            else
            {
                results = credentialsProvider.CreateNewUser(user);
            }
            

            return Ok(results);
        }

        [HttpGet]
        public object GetAllInternalUserByBusinessId()
        {
            CredentialsProvider credentialsProvider = new CredentialsProvider();
            BusinessUnitsFunctions businessUnitsFunctions = new BusinessUnitsFunctions();
            int businessId = businessUnitsFunctions.GetBusinessIdFromHeaders(Request);
            if(businessId == -1)
            {
                return Unauthorized(500);
            }
           return credentialsProvider.GetAllInternalUserByBusinessId(businessId);
        }

        [HttpDelete("{id}")]
        public object DeleteInternalUser(int id)
        {
            CredentialsProvider credentialsProvider = new CredentialsProvider();
            var result = credentialsProvider.DeleteInternalUser(id);
            return result;
        }


    }
}
