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


            var userId = credentialsProvider.CanUserLogin(loginModel.UserName, encryptedPassword);

            if (userId > 0)
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
            CredentialsResult credentialsResult = new CredentialsResult();

            CredentialsProvider credentialsProvider = new CredentialsProvider();



            user.Password = CipherProvider.EncryptPassword(user.Password);

            var userId = credentialsProvider.CreateNewUser(user);
            if (userId > 0)
            {
                credentialsResult.Result = CredentialsStatus.Success;
                //           var user = credentialsProvider.GetUser(user.UserName);
            }
            else
            {
                credentialsResult.Result = CredentialsStatus.Failed;
            }

            return Ok(new CredentialsResult { Result = credentialsResult.Result });
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



    }
}
