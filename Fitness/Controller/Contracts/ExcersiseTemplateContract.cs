using Fitness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controller.Contract
{
    public class ExcersiseTemplateContract
    {
        public ExcersiseTemplateContract()
        {
            Muscles = new List<string>();
        }
        public ExcersiseTemplateContract(ExcersiseTemplate excersiseTemplate)
        {
            Id = excersiseTemplate.Id;
            TemplateName = excersiseTemplate.TemplateName;
            Name = excersiseTemplate.Name;
            Muscles = new List<string>();
            Muscles.AddRange(excersiseTemplate.Muscles.Select(m => m.ToString()));
            Duration = excersiseTemplate.Duration;
            Weight = excersiseTemplate.Weight;
            Reps = excersiseTemplate.Reps;
            Sets = excersiseTemplate.Sets;
            Campaign = excersiseTemplate.Campaign;
        } 
        public int Id { get; set; }
        public string Name { get; set; }
        public string TemplateName { get; set; }
        public List<string> Muscles { get; set; }
        // sec
        public int Duration { get; set; }
        //kg
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }

        public int Campaign { get; set; }
        public ExcersiseTemplate ToExcersiseTemplate()
        {
            var excersiseTemplate = new ExcersiseTemplate();
            excersiseTemplate.Id = Id;
            excersiseTemplate.Name = Name;
            excersiseTemplate.TemplateName = TemplateName;
            excersiseTemplate.Muscles = new List<Muscle>();
            excersiseTemplate.Muscles.AddRange(Muscles.Select(m => (Muscle)Enum.Parse(typeof(Muscle), m)));
            excersiseTemplate.Duration = Duration;
            excersiseTemplate.Weight = Weight;
            excersiseTemplate.Reps = Reps;
            excersiseTemplate.Sets = Sets;
            excersiseTemplate.Campaign = Campaign;

            return excersiseTemplate;
        }

    }
}
