using AutoMapper;
using Data.Pizza.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Imaging;
using Web.Pizza.Helpers;
using Web.Pizza.Models;
using Web.Pizza.Servises;

namespace Web.Pizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AccountController(IJwtTokenService jwtTokenService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            var img = ImageWorker.SaveImage(model.Photo, "images\\account");

            var user = _mapper.Map<AppUser>(model);
            user.Photo = img; 
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(new { errors = result.Errors });
            
            return Ok(new { token = _jwtTokenService.CreateToken(user) });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    string token = await _jwtTokenService.CreateToken(user);
                    return Ok(new { token });
                }
                else
                {
                    return BadRequest(new {error = "Invalid user login"});
                }
            }
            catch(Exception)
            {
                throw;
            }
        }
    } 
}
