using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Data
{
    public class Meal
    {
        public Meal()
        {
            FoodItems = new List<ConsumptionTemplate>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ConsumptionTemplate> FoodItems { get; set; }
    }
}
