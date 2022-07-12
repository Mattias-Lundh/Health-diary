using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Controller.Contract;
using Fitness.Data.access;

namespace Fitness.Controller
{
    [Route("api/[controller]/")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private Database _data;

        public MealController(Database data)
        {
            _data = data;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<MealContract> GetAll(int campaignId)
        {
            return _data.Meal.GetMeals(campaignId).Select(m => new MealContract(m));
        }

        [HttpGet]
        [Route("{id}")]
        public MealContract Get(int id)
        {
            return new MealContract(_data.Meal.GetMeal(id));
        }

        [HttpPost]
        [Route("")]
        public MealContract Create(MealContract meal)
        {
            if(meal.FoodItems.Count == 0)
            {
                throw new Exception("meals must contain food items");
            }

            meal.Id = _data.Meal.InsertMeal(meal.ToMeal());
            return meal;
        }

        [HttpPut]
        [Route("")]
        public string Update(MealContract meal)
        {
            if (meal.FoodItems.Count == 0)
            {
                throw new Exception("meals must contain food items");
            }

            _data.Meal.UpdateMeal(meal.ToMeal());
            return "success";
        }

        [HttpDelete]
        [Route("")]
        public string Delete(int id)
        {
            _data.Meal.DeleteMeal(id);
            return "success";
        }
    }
}
