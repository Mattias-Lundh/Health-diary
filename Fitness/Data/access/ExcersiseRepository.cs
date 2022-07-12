using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Fitness.Data.access
{
    public class ExcersiseRepository
    {
        private string _connectionString;
        public ExcersiseRepository(string cs)
        {
            _connectionString = cs;
        }

        public List<Excersise> GetExcersises(int campaignId)
        {
            var parameters = new { CampaignId = campaignId };
            var queryString = "SELECT E.*, M.Id, M.enumCode, CA.Id FROM excersise E JOIN campaignExcersise CE ON CE.ExcersiseId = E.Id JOIN excersiseMuscle EM ON E.Id = Em.ExcersiseId JOIN muscle M ON M.Id = EM.MuscleId JOIN Campaign CA ON CA.Id = CE.CampaignId WHERE CE.CampaignId = @CampaignId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<Excersise, Muscle, Campaign, Excersise>(
                    queryString,
                    (e, m, c) =>
                    {
                        e.Muscles.Add(m);
                        e.Campaign = c.Id;
                        return e;
                    },
                    parameters,
                    splitOn: "Id")
                    .GroupBy(e => e.Id)
                    .Select(group => 
                    {
                        var excersise = new Excersise
                        {
                            Id = group.First().Id,
                            Name = group.First().Name,
                            Duration = group.First().Duration,
                            Weight = group.First().Weight,
                            Reps = group.First().Reps,
                            Sets = group.First().Sets,
                            Day = group.First().Day,
                            Campaign = group.First().Campaign
                        };

                        excersise.Muscles.AddRange(group.Select(e => e.Muscles.First()));
                        return excersise;
                    })
                    .ToList();
            }
        }

        public Excersise GetExcersise(int id)
        {
            var parameters = new {Id = id};
            var queryString = "SELECT E.*, M.Id, M.enumCode, CA.Id FROM excersise E JOIN campaignExcersise CE ON CE.ExcersiseId = E.Id JOIN excersiseMuscle EM ON E.Id = Em.ExcersiseId JOIN muscle M ON M.Id = EM.MuscleId JOIN Campaign CA ON CA.Id = CE.CampaignId WHERE E.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<Excersise, Muscle, Campaign, Excersise>(
                    queryString, 
                    (e, m, c) => 
                    {
                        e.Muscles.Add(m);
                        e.Campaign = c.Id;
                        return e;
                    },
                    parameters, 
                    splitOn: "Id")
                    .First();
                
            }
        }

        public int InsertExcersise(Excersise excersise)
        {
            var parameters = new 
            {               
               Name = excersise.Name,
               Duration = excersise.Duration,
               Weight = excersise.Weight,
               Reps = excersise.Reps,
               Sets = excersise.Sets,
               Day = excersise.Day,
               CampaignId = excersise.Campaign
            };

            var musclesSql = excersise.Muscles.Select(m => $"SELECT @MuscleId = Id FROM muscle M WHERE M.enumCode = {(int)m} INSERT INTO excersiseMuscle (ExcersiseId, MuscleId) VALUES (@ExcersiseId, @MuscleId); ");
            var queryString = $"DECLARE @MuscleId int; DECLARE @ExcersiseId int; INSERT INTO excersise (Name, Duration, Weight, Reps, Sets, Day) OUTPUT Inserted.ID VALUES (@Name, @Duration, @Weight, @Reps, @Sets, @Day); SELECT @ExcersiseId = SCOPE_IDENTITY(); {string.Concat(musclesSql)} INSERT INTO campaignExcersise (CampaignId, ExcersiseId) VALUES (@CampaignId, @ExcersiseId);";
            
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

        public void DeleteExcersise(int id)
        {

            var parameters = new { Id = id };

            
            var queryString = $"DELETE FROM excersise WHERE Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }

        public void UpdateExcersise(Excersise excersise)
        {
            var parameters = new
            {
                Id = excersise.Id,
                Name = excersise.Name,
                Duration = excersise.Duration,
                Weight = excersise.Weight,
                Reps = excersise.Reps,
                Sets = excersise.Sets,
                Day = excersise.Day,
            };
            var muscleSql = new List<string>();
            muscleSql.Add("DELETE FROM excersiseMuscle WHERE ExcersiseId = @Id ");
            muscleSql.AddRange(excersise.Muscles.Select(m => $"SELECT @MuscleId = Id FROM muscle M WHERE M.enumCode = {(int)m}; INSERT INTO excersiseMuscle (ExcersiseId, MuscleId) VALUES (@Id, @MuscleId) "));
            var queryString = $"DECLARE @MuscleId int; {string.Concat(muscleSql)} UPDATE excersise SET Name = @Name, Duration = @Duration, Weight = @Weight, Reps = @Reps, Sets = @Sets, Day = @Day WHERE Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }
    }
}
