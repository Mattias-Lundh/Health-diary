using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Data
{
    public class Excersise
    {
        public Excersise()
        {
            Muscles = new List<Muscle>();
        }
        public int Id { get; set; }        
        public string Name { get; set; }
        public List<Muscle>  Muscles{ get; set; }
        // sec
        public int Duration { get; set; }
        //kg
        public int Weight { get; set; }
        public int Reps{ get; set; }
        public int Sets { get; set; }
        public DateTime Day { get; set; }
        public int Campaign { get; set; }
    }
}
