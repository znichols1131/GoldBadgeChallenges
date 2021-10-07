using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Repository
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Cost { get; set; }

        // Constructors
        public Ingredient() { }

        public Ingredient(string name)
        {
            Name = name;
        }

        public Ingredient(string name, double cost)
        {
            Name = name;
            Cost = cost;
        }

        // Cloning for testing
        public Ingredient Clone()
        {
            return new Ingredient(Name, Cost);
        }
    }
}
