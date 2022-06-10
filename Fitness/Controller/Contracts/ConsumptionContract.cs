using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fitness.Data;

namespace Fitness.Controller.Contract
{
    public class ConsumptionContract
    {
        public ConsumptionContract()
        {

        }
        public ConsumptionContract(Consumption consumption)
        {
            Id = consumption.Id;
            Name = consumption.Name;
            Calories = consumption.Calories;
            Protein = consumption.Protein;
            Carbs = consumption.Carbs;
            Amount = consumption.Amount;
            Unit = consumption.Unit.ToString();
            Day = consumption.Day;
            Campaign = consumption.Campaign;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        //per 100g
        public int Protein { get; set; }
        //per 100g
        public int Carbs { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public DateTime Day { get; set; }
        public int Campaign { get; set; }

        public Consumption ToConsumption()
        {
            var consumption = new Consumption();
            consumption.Id = Id;
            consumption.Name = Name;
            consumption.Calories = Calories;
            consumption.Protein = Protein;
            consumption.Carbs = Carbs;
            consumption.Amount = Amount;
            consumption.Unit = (Unit) Enum.Parse(typeof(Unit), Unit);
            consumption.Day = Day;
            consumption.Campaign = Campaign;

            return consumption;
        }
    }
}
