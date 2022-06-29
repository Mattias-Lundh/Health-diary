using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Fitness.Data.access
{
    public class ConsumptionTemplateRepository
    {
        private string _connectionString;
        public ConsumptionTemplateRepository(string cs)
        {
            _connectionString = cs;
        }

        public List<ConsumptionTemplate> GetConsumptionTemplates(int campaignId)
        {
            var parameters = new { CampaignId = campaignId };
            var queryString = "SELECT CT.*, CA.Id FROM consumptionTemplate CT JOIN campaignConsumptionTemplate CCT ON CT.Id = CCT.ConsumptionTemplateId JOIN Campaign CA ON CA.Id = CCT.CampaignId WHERE CCT.CampaignId = @CampaignId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<ConsumptionTemplate, Campaign, ConsumptionTemplate>(
                    queryString,
                    (ct, ca) =>
                    {
                        ct.Campaign = ca.Id;
                        return ct;
                    },
                    parameters,
                    splitOn: "Id"
                    ).ToList();
            }
        }

        public ConsumptionTemplate GetConsumptionTemplate(int id)
        {
            var parameters = new { ConsumptionTemplateId = id };
            var queryString = "SELECT CT.*, CA.Id FROM consumptionTemplate CT JOIN campaignConsumptionTemplate CCT ON CT.Id = CCT.ConsumptionTemplateId JOIN campaign CA ON CA.Id = CCT.CampaignId WHERE CCT.ConsumptionTemplateId = @ConsumptionTemplateId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<ConsumptionTemplate, Campaign, ConsumptionTemplate>(
                     queryString,
                    (ct, ca) =>
                    {
                        ct.Campaign = ca.Id;
                        return ct;
                    },
                    parameters,
                    splitOn: "Id")
                    .First();
            }
        }

        public int InsertConsumptionTemplate(ConsumptionTemplate consumptionTemplate)
        {
            var parameters = new
            {
                TemplateName = consumptionTemplate.TemplateName,
                Name = consumptionTemplate.Name,
                Calories = consumptionTemplate.Calories,
                Protein = consumptionTemplate.Protein,
                Carbs = consumptionTemplate.Carbs,
                Amount = consumptionTemplate.Amount,
                Unit = consumptionTemplate.Unit,
                CampaignId = consumptionTemplate.Campaign
            };

            var queryString = "DECLARE @ConsumptionTemplateId int; INSERT INTO consumptionTemplate (TemplateName, Name, Calories, Protein, Carbs, Amount, Unit) OUTPUT Inserted.ID VALUES (@TemplateName, @Name, @Calories, @Protein, @Carbs, @Amount, @Unit); SELECT @ConsumptionTemplateId = SCOPE_IDENTITY(); INSERT INTO campaignConsumptionTemplate (CampaignId, ConsumptionTemplateId) VALUES (@CampaignId, @ConsumptionTemplateId)";
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

        public void DeleteConsumptionTemplate(int Id)
        {
            var parameters = new { Id = Id };
            var queryString = "DELETE FROM consumptionTemplate WHERE Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }

        public void UpdateConsumptionTemplate(ConsumptionTemplate consumptionTemplate)
        {
            var parameters = new
            {
                Id = consumptionTemplate.Id,
                TemplateName = consumptionTemplate.TemplateName,
                Name = consumptionTemplate.Name,
                Calories = consumptionTemplate.Calories,
                Protein = consumptionTemplate.Protein,
                Carbs = consumptionTemplate.Carbs,
                Amount = consumptionTemplate.Amount,
                Unit = consumptionTemplate.Unit,
            };
            var queryString = "UPDATE consumptionTemplate SET TemplateName = @TemplateName, Name = @Name, Calories = @Calories, Protein = @Protein, Carbs = @Carbs, Amount = @Amount, Unit = @Unit WHERE consumptionTemplate.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
                return;
            }
        }
    }
}
