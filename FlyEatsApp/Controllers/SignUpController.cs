using FlyEatsApp.Models;
using FlyEatsApp.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static FlyEatsApp.Models.CredentialsResult;

namespace FlyEatsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        [HttpPost]
        public IActionResult Create(string firstName, string lastName ,string email, string password, int accountType)
        {
            CredentialsResult credentialsResult = new CredentialsResult();

            CredentialsProvider credentialsProvider = new CredentialsProvider();


            
                string encryptedPassword = CipherProvider.EncryptPassword(password);

//                var newDriverCode = credentialsProvider.GenerateNewDriverCode();

                AppUser newUser = new AppUser()
                {
                    BusinessId = 5,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Password = encryptedPassword,
                    accountType = (AppUser.AccountType) accountType,

                };

                var userId = credentialsProvider.CreateNewUser(newUser);
                if (userId > 0)
                {
                    credentialsResult.Result = CredentialsStatus.Success;
                    var user = credentialsProvider.GetUser(email);
                }
                else
                {
                    credentialsResult.Result = CredentialsStatus.Failed;
                }

            return Ok(new CredentialsResult { Result = credentialsResult.Result });
        }
    }
}
