using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ExperimentToolApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExperimentToolApi.Secure
{
    public class TokenManager
    {
        private readonly IOptions<JwtSettings> _config;
        public TokenManager(IOptions<JwtSettings> config)
        {
            _config = config;
        }
        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.Value.Key);

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            

            return tokenHandler.WriteToken(token);
        }
        public string RefreshToken()
        {
            byte[] random = new Byte[32];

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(random);

            return Convert.ToBase64String(random);
        }
        public string decodeUsername(string jwtToken){
            JwtSecurityTokenHandler readHandler =  new JwtSecurityTokenHandler();
            
            var token = readHandler.ReadJwtToken(jwtToken) as JwtSecurityToken;

            string username = token.Claims.First().Value;

            return username;
        }
    }
}