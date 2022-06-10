using Fitness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controller.Contract
{
    public class MealContract
    {
        public MealContract()
        {

        }
        public MealContract(Meal meal)
        {
            Id = meal.Id;
            Name = meal.Name;
            FoodItems = new List<ConsumptionTemplateContract>();
            FoodItems.AddRange(meal.FoodItems.Select(ct => new ConsumptionTemplateContract(ct))); 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ConsumptionTemplateContract> FoodItems { get; set; }

        public Meal ToMeal()
        {

            var meal = new Meal();
            meal.Id = Id;
            meal.Name = Name;
            meal.FoodItems.AddRange(FoodItems.Select(ct => ct.ToConsumptionTemplate()));

            return meal;
        }

    }
}
