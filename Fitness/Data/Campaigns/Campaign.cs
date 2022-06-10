using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Data
{
    public class Campaign
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public List<Consumption> Consumptions { get; set; }
        public List<ConsumptionTemplate> ConsumptionTemplates{ get; set; }
        public List<Meal>Meals { get; set; }
        public List<Excersise> Excersises{ get; set; }
        public List<ExcersiseTemplate> ExcersiseTemplates { get; set; }
    }
}
