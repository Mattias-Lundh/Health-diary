using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Fitness.Data.access
{
    public class CampaignRepository
    {
        private string _connectionString;
        public CampaignRepository(string cs)
        {
            _connectionString = cs;
        }

        public List<Campaign> GetCampaigns(int appUserId)
        {
            var parameters = new { AppUserId = appUserId };
            var queryString = "SELECT CA.Id, CA.Name, AU.* FROM campaign CA JOIN AppUser AU ON AU.Id = CA.AppUser WHERE AU.Id = @AppUserId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<Campaign, AppUser, Campaign>(
                    queryString,
                    (ca, au) => 
                    {
                        ca.AppUser = au;
                        return ca;
                    },
                    parameters,
                    splitOn: "Id"
                    ).ToList();
            }
        }        

        public int InsertCampaign(Campaign campaign)
        {
            var parameters = new 
            {                
                Name = campaign.Name,
                AppUser = campaign.AppUser.Id
            };

            var queryString = "INSERT INTO campaign (Name, AppUser) OUTPUT INSERTED.Id VALUES (@Name, @AppUser);";
            
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                var id = con.QuerySingle<int>(queryString, parameters);
                return id;
                
            }
        }

        public void DeleteCampaign(int id)
        {
            // TODO if nessasary, delete all data associated with campaign.
            var parameters = new { Id = id };
            var queryString = "DELETE FROM campaign WHERE campaign.Id = @Id;";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }

        public void UpdateCampaign(Campaign campaign)
        {
            var parameters = new
            {
                Id = campaign.Id,
                Name = campaign.Name,
                AppUser = campaign.AppUser.Id
            };
            var queryString = "UPDATE campaign SET Name = @Name, AppUser = @AppUser WHERE campaign.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }
    }
}
