using Fitness.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Controller.Contract
{
    public class ExcersiseContract
    {
        public ExcersiseContract()
        {
            Muscles = new List<string>();
        }

        public ExcersiseContract(Excersise excersise)
        {
            Id = excersise.Id;
            Name = excersise.Name;
            Muscles = new List<string>();
            Muscles.AddRange(excersise.Muscles.Select(m => m.ToString()));
            Duration = excersise.Duration;
            Weight = excersise.Weight;
            Reps = excersise.Reps;
            Sets = excersise.Sets;
            Day = excersise.Day;
            Campaign = excersise.Campaign;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Muscles { get; set; }
        // sec
        public int Duration { get; set; }
        //kg
        public int Weight { get; set; }
        public int Reps { get; set; }
        public int Sets { get; set; }
        public DateTime Day { get; set; }
        public int Campaign { get; set; }

        public Excersise ToExcersise()
        {
            var excersise = new Excersise();
            excersise.Id = Id;
            excersise.Name = Name;
            excersise.Muscles = new List<Muscle>();
            excersise.Muscles.AddRange(Muscles.Select(m => (Muscle)Enum.Parse(typeof(Muscle), m)));
            excersise.Duration = Duration;
            excersise.Weight = Weight;
            excersise.Reps = Reps;
            excersise.Sets = Sets;
            excersise.Day = Day;
            excersise.Campaign = Campaign;

            return excersise;
        }
    }
}
