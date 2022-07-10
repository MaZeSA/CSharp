using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebBackend.Data.Entities;
using WebBackend.Models;

namespace WebBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AplicattionUser> _userManager;
        public AccountController(UserManager<AplicattionUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("GoogleExternalLogin")]
        public async Task<IActionResult> GoogleExternalLoginAsync([FromBody] ExternalLoginDTO model)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                {
                    "829254852983-9als4nv9587l1oqagst8q9rpsuluaouf.apps.googleusercontent.com"
                }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(model.Token, settings);
            if(payload != null)
            {
                var info = new UserLoginInfo(model.Provider, payload.Subject, model.Provider);
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                if(user == null)
                {
                    user = await _userManager.FindByEmailAsync(payload.Email);
                    if (user == null)
                    {
                        user = new AplicattionUser
                        {
                            Email = payload.Email,
                            UserName = payload.Email,
                            FirstName = payload.GivenName,
                            LastName = payload.FamilyName
                        };
                        var resultCreate = await _userManager.CreateAsync(user);    
                        if(!resultCreate.Succeeded)
                        {
                            return BadRequest(new { error = "Щось пішло не так" });
                        }
                    }
                    var resultAddLogin = await _userManager.AddLoginAsync(user, info);
                    if(!resultAddLogin.Succeeded)
                        return BadRequest(new { error = "Щось пішло не так" });
                }

                return Ok(new {userId = user.Id});
            }
            return Ok();
        }
    }
}
