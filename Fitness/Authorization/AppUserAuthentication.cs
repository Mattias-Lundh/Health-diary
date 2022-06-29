using Fitness.Data;
using Fitness.Data.access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Fitness.Authorization
{
    public class AppUserAuthentication
    {
        private Security _security;
        private readonly JWT _jwt;

        public AppUserAuthentication(Database data, JWT jwt)
        {
            _security = new Security();
            _jwt = jwt;
        }
        public CreatePasswordResult CreatePasswordHash(string rawPassword)
        {
            _security.GeneratePasswordData(rawPassword);

            return new CreatePasswordResult(_security.HashedPassword, _security.Salt);
        }

        public bool Login(string userEmail, string password, AppUser appUser)
        {
            return _security.ComparePassword(appUser.Password, password, appUser.Salt);
        }

        public string GetJWT(int appUserId)
        {
            
            return _jwt.GetToken(appUserId).ToString();
        }

        // return appUserId if valid
        public int ValidateToken(string token)
        {
            return _jwt.ValidateToken(token);
        }
    }

    public class CreatePasswordResult
    {
        public CreatePasswordResult(string passwordHash, string salt)
        {
            Salt = salt;
            PasswordHash = passwordHash;
        }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
    }
}
