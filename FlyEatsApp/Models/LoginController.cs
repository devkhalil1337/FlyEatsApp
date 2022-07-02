using Microsoft.AspNetCore.Mvc;
using static FlyEatsApp.Models.CredentialsResult;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FlyEatsApp.Providers;

namespace FlyEatsApp.Models
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public IActionResult CheckUserCredentials([FromBody] AppUser _user)
        {
            CredentialsResult credentialsResult = new CredentialsResult();

            CredentialsProvider credentialsProvider = new CredentialsProvider();

            string encryptedPassword = CipherProvider.EncryptPassword(_user.Password);


            var userId = credentialsProvider.CanUserLogin(_user.Email, encryptedPassword);

            if (userId > 0)
            {
                credentialsResult.Result = CredentialsStatus.Success;

                var user = credentialsProvider.GetUser(_user.Email);

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


    }
}
