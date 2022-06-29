using Fitness.Authorization;
using Fitness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Fitness.Controller.Contracts
{
    public class RegisterContract
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public AppUser ToUser()
        {
            var appUser = new AppUser();
            appUser.Email = Email;            
            appUser.Password = Password;

            return appUser;
        }        
    }
}
