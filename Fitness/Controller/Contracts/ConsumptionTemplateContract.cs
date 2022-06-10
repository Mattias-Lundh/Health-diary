using Fitness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controller.Contract
{ 
    public class ConsumptionTemplateContract
    {
        public ConsumptionTemplateContract()
        {

        }
        public ConsumptionTemplateContract(ConsumptionTemplate consumptionTemplate)
        {
                Id = consumptionTemplate.Id;
                TemplateName = consumptionTemplate.TemplateName;
                Name = consumptionTemplate.Name;
                Calories = consumptionTemplate.Calories;
                Protein = consumptionTemplate.Protein;
                Carbs = consumptionTemplate.Carbs;
                Amount = consumptionTemplate.Amount;
                Unit = consumptionTemplate.Unit.ToString();
                Campaign = consumptionTemplate.Campaign;
        }
    
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        //per 100g
        public int Protein { get; set; }
        //per 100g
        public int Carbs { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public int Campaign { get; set; }

        public ConsumptionTemplate ToConsumptionTemplate()
        {
            var consumption = new ConsumptionTemplate();
            consumption.Id = Id;
            consumption.TemplateName = TemplateName;
            consumption.Name = Name;
            consumption.Calories = Calories;
            consumption.Protein = Protein;
            consumption.Carbs = Carbs;
            consumption.Amount = Amount;
            consumption.Unit = Unit == null? Data.Unit.g : (Unit)Enum.Parse(typeof(Unit), Unit);
            consumption.Campaign = Campaign;

            return consumption;
        }
    }
}
