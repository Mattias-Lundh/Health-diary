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

            System = new SystemRepository(sc);
            AppUser = new AppUserRepository(sc);
            Consumption = new ConsumptionRepository(sc);
            ConsumptionTemplate = new ConsumptionTemplateRepository(sc);
            Excersise = new ExcersiseRepository(sc);
            ExcersiseTemplate = new ExcersiseTemplateRepository(sc);
            Meal = new MealRepository(sc);
            Campaign = new CampaignRepository(sc);
        }
        public SystemRepository System { get; set; }
        public AppUserRepository AppUser { get; set; }
        public CampaignRepository Campaign { get; set; }
        public ConsumptionRepository Consumption { get; }
        public ConsumptionTemplateRepository ConsumptionTemplate { get; }
        public ExcersiseRepository Excersise { get; }
        public ExcersiseTemplateRepository ExcersiseTemplate { get; }
        public MealRepository Meal { get; }
        
    }
}
