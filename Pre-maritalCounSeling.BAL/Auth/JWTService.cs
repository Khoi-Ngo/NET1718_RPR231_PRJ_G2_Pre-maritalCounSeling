using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Pre_maritalCounSeling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pre_maritalCounSeling.BAL.Auth
{
    public class JWTService
    {
        #region INIT
        private readonly ILogger<JWTService> _logger;
        private readonly IConfiguration _configuration;
        public JWTService(ILogger<JWTService> logger)
        {
            _logger = logger;
        }
        #endregion

        //generating access token
        public string GenerateAccessToken(User user)
        {
            //Claim attribute for the token
            List<Claim> claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("FullName", user.FullName),
                new Claim("Email", user.Email),
                new Claim("RoleName", user.Role.Name),
                new Claim("IsActive", user.IsActive.ToString()),
                new Claim("Avatar", user.Avatar),

            };
            //get secretkey for jwt token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Key").Value!
                ));

            //create credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //create token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        //generating refresh token
    }
}
