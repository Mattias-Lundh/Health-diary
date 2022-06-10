using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Data.access
{
    public class Database
    {
        public Database(string sc)
        {
            Consumption = new ConsumptionRepository(sc);
            ConsumptionTemplate = new ConsumptionTemplateRepository(sc);
            Excersise = new ExcersiseRepository(sc);
            ExcersiseTemplate = new ExcersiseTemplateRepository(sc);
            Meal = new MealRepository(sc);
        }

        public ConsumptionRepository Consumption { get; }
        public ConsumptionTemplateRepository ConsumptionTemplate { get; }
        public ExcersiseRepository Excersise { get; }
        public ExcersiseTemplateRepository ExcersiseTemplate { get; }
        public MealRepository Meal { get; }
    }
}
