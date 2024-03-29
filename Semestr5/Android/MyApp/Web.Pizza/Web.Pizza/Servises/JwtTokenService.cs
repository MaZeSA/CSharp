﻿using Data.Pizza.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Web.Pizza.Servises
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(AppUser user);
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public JwtTokenService(IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("name", user.UserName)
            };

            foreach(var role in roles)
            {
                claims.Add(new Claim("roles", role));
            }

            var _signingCredentials = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<String>("JwtKey")));
            var mCredentials = new SigningCredentials(_signingCredentials, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: mCredentials,
                expires: DateTime.Now.AddDays(1000),
                claims:claims
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
