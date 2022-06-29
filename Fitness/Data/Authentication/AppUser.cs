using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Fitness.Data
{
    public class AppUser
    {
        public AppUser()
        {

        }
        public AppUser(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}

