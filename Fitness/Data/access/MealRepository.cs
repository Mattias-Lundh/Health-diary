using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Fitness.Data.access
{
    public class MealRepository
    {
        private string _connectionString;
        public MealRepository(string cs)
        {
            _connectionString = cs;
        }

        public List<Meal> GetMeals(int campaignId)
        {
            var parameters = new { CampaignId = campaignId };
            var queryString = "SELECT M.Id, M.Name, Ct.*, C.Id FROM meal M JOIN mealConsumptionTemplate MCT ON M.Id = MCT.MealId JOIN consumptionTemplate CT ON CT.Id = MCT.ConsumptionTemplateId JOIN campaignConsumptionTemplate CCT ON CT.Id = CCT.ConsumptionTemplateId JOIN campaign C on C.Id = CCT.CampaignId WHERE CCT.CampaignId = @CampaignId";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<Meal, ConsumptionTemplate, Campaign, Meal>(
                    queryString, 
                    (m, ct, c) => 
                    {
                        ct.Campaign = c.Id;
                        m.FoodItems.Add(ct);
                        return m; 
                    }, 
                    parameters, 
                    splitOn: "Id").ToList();
            }
        }

        public Meal GetMeal(int id)
        {
            var parameters = new {Id = id};
            var queryString = "SELECT M.Id, M.Name, Ct.*, C.Id FROM meal M JOIN mealConsumptionTemplate MCT ON M.Id = MCT.MealId JOIN consumptionTemplate CT ON CT.Id = MCT.ConsumptionTemplateId JOIN campaignConsumptionTemplate CCT ON CT.Id = CCT.ConsumptionTemplateId JOIN campaign C on C.Id = CCT.CampaignId WHERE M.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();

                return con.Query<Meal, ConsumptionTemplate, Campaign, Meal>(
                    queryString,
                     (m, ct, c) =>
                     {
                         ct.Campaign = c.Id;
                         m.FoodItems.Add(ct);
                         return m;
                     },
                    parameters, 
                    splitOn: "Id").First();
                
            }
        }

        public int InsertMeal(Meal meal)
        {
            var parameters = new 
            {                
                Name = meal.Name                
            };

            var foodItems = meal.FoodItems.Select(ct => $"INSERT INTO mealConsumptionTemplate (MealId, ConsumptionTemplateId) VALUES (@MealId, {ct.Id}); ");
            

            var queryString = $"DECLARE @MealId int; INSERT INTO meal (Name) OUTPUT Inserted.ID VALUES (@Name); SELECT @MealId = SCOPE_IDENTITY() {string.Concat(foodItems)}";
            
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var trx = con.BeginTransaction())
                {
                    var id = con.Execute(queryString, parameters, trx);
                    trx.Commit();
                    return id;
                }

                    
            }
        }

        public void DeleteMeal(int id)
        {

            var parameters = new { Id = id };

            
            var queryString = $"DELETE FROM meal WHERE meal.Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                con.Execute(queryString, parameters);
            }
        }

        public void UpdateMeal(Meal meal)
        {
            var parameters = new
            {
                Id = meal.Id,
                Name = meal.Name
            };

            var foodItems = new List<string>();
            foodItems.Add("DELETE FROM mealConsumptionTemplate WHERE mealConsumptionTemplate.MealId = @Id ");
            foodItems.AddRange(meal.FoodItems.Select(ct => $"INSERT INTO mealConsumptionTemplate (MealId, ConsumptionTemplateId) VALUES (@Id, {ct.Id}); "));
            var queryString = $"{string.Concat(foodItems)}; UPDATE meal SET Name = @Name WHERE Id = @Id";
            using (var con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (var trx = con.BeginTransaction())
                {
                    con.Execute(queryString, parameters, trx);
                    trx.Commit();
                }
                    
            }
        }
    }
}
