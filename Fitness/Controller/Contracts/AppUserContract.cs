using Fitness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controller.Contracts
{
    public class AppUserContract
    {
        public AppUserContract(AppUser appUser)
        {

        }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
