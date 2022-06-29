using System.Linq;
using System.Data.SqlClient;
using Dapper;

namespace Fitness.Data.access
{
    public class AppUserRepository
    {
        private string _connectionString;
        public AppUserRepository(string cs)
        {
            _connectionString = cs;
        }

        public AppUser GetByEmail(string email)
        {            
            var parameters = new { Email = email};
            var queryString = "SELECT AU.* from AppUser AU WHERE AU.Email = @Email";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();                
                return con.Query<AppUser>(
                    queryString,
                    parameters
                    ).First();
            }            
        }        

        public int InsertAppUser(AppUser appUser)
        {
            var parameters = new
            {
                Email = appUser.Email,
                Password = appUser.Password,
                Salt = appUser.Salt
            };

            var queryString = "INSERT INTO AppUser (Email, Password, Salt) OUTPUT Inserted.ID VALUES (@Email, @Password, @Salt);";

            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var trx = con.BeginTransaction())
                {
                    var id = con.QuerySingle<int>(queryString, parameters, trx);
                    trx.Commit();
                    return id;
                }
            }
        }

        public void DeleteAppUser(int id)
        {
            // TODO delete all user data if nessasary
            var parameters = new { Id = id };
            var queryString = "DELETE FROM AppUser WHERE AppUser.Id = @Id;";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }

        public void UpdateAppUser(AppUser appUser)
        {           
            var parameters = new
            {
                Id = appUser.Id,
                Email = appUser.Email,
                Password = appUser.Password,
                Salt = appUser.Salt
            };
            var queryString = "UPDATE AppUser SET Email = @Email, Password = @Password, Salt = @Salt WHERE AppUser.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }
    }
}
