using Fitness.Data.access;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Fitness.Authorization
{
    public class JWT
    {
        private Database _database;
        public JWT(Database database)
        {
            _database = database;
        }
        public string GetToken(int appUser)
        {            
            return GenerateToken(appUser.ToString());
        }

        public int ValidateToken(string token)
        {
            if (token == null)
            {
                throw new Exception("invalid token");
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(GetSecret());
            try
            {
                var a = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var appUserId = int.Parse(jwtToken.Claims.First(x => x.Type == "appUserId").Value);

                // return user id from JWT token if validation successful
                return appUserId;
            }
            catch
            {
                throw new Exception("invalid token");
            }
        }


        private string GetSecret()
        {
            return _database.System.GetJwtSecret();
        }

        private string GenerateToken(string appUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();            
            var key = Encoding.ASCII.GetBytes(GetSecret());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                
                Subject = new ClaimsIdentity(new[] { new Claim("appUserId", appUser) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }        
    }
}

