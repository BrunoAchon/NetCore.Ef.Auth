using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WS.Core.Domain;
using WS.Manager.Interfaces.Services;

namespace WS.Data.Services
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(AspNetUser aspNetUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JWT:Secret").Value);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, aspNetUser.Id.ToString()),
                new Claim(ClaimTypes.Name, aspNetUser.UserName),
                new Claim(ClaimTypes.Authentication, aspNetUser.PasswordHash)
            };

            claims.AddRange(aspNetUser.aspNetUserRoles.Select(p => new Claim(ClaimTypes.Role, p.aspNetRole.Name)));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = _configuration.GetSection("JWT:Audience").Value,
                Issuer = _configuration.GetSection("JWT:Issuer").Value,
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("JWT:ExpiresMinutes").Value)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
