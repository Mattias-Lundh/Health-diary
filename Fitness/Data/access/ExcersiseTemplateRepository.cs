using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Fitness.Data.access
{
    public class ExcersiseTemplateRepository
    {
        private string _connectionString;
        public ExcersiseTemplateRepository(string cs)
        {
            _connectionString = cs;
        }

        public List<ExcersiseTemplate> GetExcersiseTemplates(int campaignId)
        {
            var parameters = new { CampaignId = campaignId };
            var queryString = "SELECT ET.*, M.Id, M.enumCode, CA.Id FROM excersiseTemplate ET JOIN campaignExcersiseTemplate CET ON CET.ExcersiseTemplateId = ET.Id JOIN excersiseTemplateMuscle ETM ON ET.Id = ETM.ExcersiseTemplateId JOIN muscle M ON M.Id = ETM.MuscleId JOIN Campaign CA ON CA.Id = CET.CampaignId WHERE CA.Id = @CampaignId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<ExcersiseTemplate, Muscle, Campaign, ExcersiseTemplate>(
                    queryString, 
                    (et, m, c) => 
                    {
                        et.Muscles.Add(m);
                        et.Campaign = c.Id;
                        return et; 
                    }, 
                    parameters, 
                    splitOn: "Id").ToList();
            }
        }

        public ExcersiseTemplate GetExcersiseTemplate(int id)
        {
            var parameters = new {Id = id};
            var queryString = "SELECT ET.*, M.Id, M.enumCode, CA.Id FROM excersiseTemplate ET JOIN campaignExcersiseTemplate CET ON CET.ExcersiseTemplateId = ET.Id JOIN excersiseTemplateMuscle ETM ON ET.Id = ETM.ExcersiseTemplateId JOIN muscle M ON M.Id = ETM.MuscleId JOIN Campaign CA ON CA.Id = CET.CampaignId WHERE ET.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<ExcersiseTemplate, Muscle, Campaign, ExcersiseTemplate>(
                    queryString, 
                    (et, m, c) => 
                    {
                        et.Muscles.Add(m);
                        et.Campaign = c.Id;
                        return et;
                    },
                    parameters, 
                    splitOn: "Id").First();
                
            }
        }

        public int InsertExcersiseTemplate(ExcersiseTemplate excersiseTemplate)
        {
            var parameters = new 
            {                
                TemplateName = excersiseTemplate.TemplateName,
                Name = excersiseTemplate.Name,
                Duration = excersiseTemplate.Duration,
                Weight = excersiseTemplate.Weight,
                Reps = excersiseTemplate.Reps,
                Sets = excersiseTemplate.Sets,
                CampaignId = excersiseTemplate.Campaign
            };

            var musclesSql = excersiseTemplate.Muscles.Select(m => $"SELECT @MuscleId = Id FROM muscle M WHERE M.enumCode = {(int)m} INSERT INTO excersiseTemplateMuscle (ExcersiseTemplateId, MuscleId) VALUES (@ExcersiseTemplateId, @MuscleId); ");
            var queryString = $"DECLARE @MuscleId int; DECLARE @ExcersiseTemplateId int; INSERT INTO excersiseTemplate (TemplateName, Name, Duration, Weight, Reps, Sets) OUTPUT Inserted.ID VALUES (@TemplateName, @Name, @Duration, @Weight, @Reps, @Sets); SELECT @ExcersiseTemplateId = SCOPE_IDENTITY(); {string.Concat(musclesSql)} INSERT INTO campaignExcersiseTemplate (CampaignId, ExcersiseTemplateId) VALUES (@CampaignId, @ExcersiseTemplateId);";

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

        public void DeleteExcersiseTemplate(int id)
        {

            var parameters = new { Id = id };

            
            var queryString = $"DELETE FROM excersiseTemplate WHERE Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }

        public void UpdateExcersiseTemplate(ExcersiseTemplate excersiseTemplate)
        {
            var parameters = new
            {
                Id = excersiseTemplate.Id,
                TemplateName = excersiseTemplate.TemplateName,
                Name = excersiseTemplate.Name,
                Duration = excersiseTemplate.Duration,
                Weight = excersiseTemplate.Weight,
                Reps = excersiseTemplate.Reps,
                Sets = excersiseTemplate.Sets,
                
            };


            var muscleSql = new List<string>();
            muscleSql.Add("DELETE FROM excersiseTemplateMuscle WHERE ExcersiseTemplateId = @Id ");
            muscleSql.AddRange(excersiseTemplate.Muscles.Select(m => $"SELECT @MuscleId = Id FROM muscle M WHERE M.enumCode = {(int)m}; INSERT INTO excersiseTemplateMuscle (ExcersiseTemplateId, MuscleId) VALUES (@Id, @MuscleId) "));
            var queryString = $"DECLARE @MuscleId int; {string.Concat(muscleSql)} UPDATE excersiseTemplate SET TemplateName = @TemplateName, Name = @Name, Duration = @Duration, Weight = @Weight, Reps = @Reps, Sets = @Sets WHERE Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }
    }
}
