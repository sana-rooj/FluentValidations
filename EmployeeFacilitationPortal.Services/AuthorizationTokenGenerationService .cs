using EmployeeFacilitationPortal.Entities.Common.Utility;
using EmployeeFacilitationPortal.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeFacilitationPortal.Services
{
    public class AuthorizationTokenGenerationService : IGenerateWebToken
    {
        private int tokenExpiryDurationInMins = AppConfigurations.TokenTimeOutInMinutes;
        private IConfiguration _config;
        public AuthorizationTokenGenerationService(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateJSONWebToken(Entities.Models.Employee userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims:new Claim[]
              {
                new Claim(ClaimTypes.Role, userInfo.Role.Id.ToString())
              },
              expires: DateTime.Now.AddMinutes(this.tokenExpiryDurationInMins),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
