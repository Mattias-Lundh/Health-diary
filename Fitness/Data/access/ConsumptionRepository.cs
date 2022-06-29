using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Fitness.Data.access
{
    public class ConsumptionRepository
    {
        private string _connectionString;
        public ConsumptionRepository(string cs)
        {
            _connectionString = cs;
        }

        public List<Consumption> GetConsumptions(int campaignId)
        {
            var parameters = new { CampaignId = campaignId };
            var queryString = "SELECT C.*, CA.Id FROM consumption C JOIN campaignConsumption CC ON C.Id = CC.ConsumptionId JOIN Campaign CA ON CA.Id = CC.CampaignId WHERE CC.CampaignId = @CampaignId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<Consumption, Campaign, Consumption>(
                    queryString,
                    (co, ca) => 
                    {
                        co.Campaign = ca.Id;
                        return co;
                    },
                    parameters,
                    splitOn: "Id"
                    ).ToList();
            }
        }

        public Consumption GetConsumption(int id)
        {
            var parameters = new {ConsumptionId = id};
            var queryString = "SELECT C.*, CA.Id FROM consumption C JOIN campaignConsumption CC ON C.Id = CC.ConsumptionId JOIN Campaign CA ON CA.Id = CC.CampaignId WHERE C.Id = @ConsumptionId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<Consumption, Campaign, Consumption>(
                    queryString,
                    (co, ca) =>
                    {
                        co.Campaign = ca.Id;
                        return co;
                    },
                    parameters,
                    splitOn: "Id"
                    ).First();
            }
        }

        public int InsertConsumption(Consumption consumption)
        {
            var parameters = new 
            {                
                Name = consumption.Name,
                Calories = consumption.Calories,
                Protein = consumption.Protein,
                Carbs = consumption.Carbs,
                Amount = consumption.Amount,
                Unit = consumption.Unit,
                Day = consumption.Day,
                CampaignId = consumption.Campaign
            };

            var queryString = "DECLARE @ConsumptionId int; INSERT INTO consumption (Name, Calories, Protein, Carbs, Amount, Unit, Day) OUTPUT Inserted.ID VALUES (@Name, @Calories, @Protein, @Carbs, @Amount, @Unit, @Day); SELECT @ConsumptionId = SCOPE_IDENTITY(); INSERT INTO campaignConsumption (CampaignId, ConsumptionId) VALUES (@CampaignId, @ConsumptionId)";
            
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

        public void DeleteConsumption(int id)
        {
            var parameters = new { Id = id };
            var queryString = "DELETE FROM consumption WHERE consumption.Id = @Id;";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }

        public void UpdateConsumption(Consumption consumption)
        {
            var parameters = new
            {
                Id = consumption.Id,                
                Name = consumption.Name,
                Calories = consumption.Calories,
                Protein = consumption.Protein,
                Carbs = consumption.Carbs,
                Amount = consumption.Amount,
                Unit = consumption.Unit,
                Day = consumption.Day
            };
            var queryString = "UPDATE consumption SET Name = @Name, Calories = @Calories, Protein = @Protein, Carbs = @Carbs, Amount = @Amount, Unit = @Unit, Day = @Day WHERE consumption.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }
    }
}
