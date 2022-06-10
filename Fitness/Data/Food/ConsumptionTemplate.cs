using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Data
{
    public class ConsumptionTemplate
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public string Name { get; set; }
        //per 100g
        public int Calories { get; set; }
        //per 100g
        public int Protein { get; set; }
        //per 100g
        public int Carbs { get; set; }
        public int Amount { get; set; }
        public Unit Unit { get; set; }
        public int Campaign { get; set; }

    }
}
