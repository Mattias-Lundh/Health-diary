using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Fitness.Data.access
{
    public class SystemRepository
    {
        private string _connectionString;
        public SystemRepository(string cs)
        {
            _connectionString = cs;
        }

        public string GetJwtSecret()
        {
            var queryString = "SELECT * FROM system";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                return con.Query<System>(queryString).First().JwtSecret;
            }
        }
    }
}
