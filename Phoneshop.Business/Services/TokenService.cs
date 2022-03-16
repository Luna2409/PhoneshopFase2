using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Phoneshop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Phoneshop.Business.Services
{
    public class TokenService : ITokenService
    {
        IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string Generate(List<Claim> claims)
        {
            if (claims is null || !claims.Any())
                throw new ArgumentNullException(nameof(claims));

            var mySecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_config.GetSection("jwtSettings:SignKey").Value));
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _config.GetSection("jwtSettings:Issuer").Value,
                Audience = _config.GetSection("jwtSettings:Audience").Value,
                SigningCredentials = new SigningCredentials(
                    mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }
    }
}
