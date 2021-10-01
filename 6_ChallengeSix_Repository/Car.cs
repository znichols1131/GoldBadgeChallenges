using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_ChallengeSix_Repository
{
    public enum FuelType
    {
        Null = -1,
        Electric = 0,
        GasPowered = 1,
        Hybrid = 2
    }

    public abstract class Car
    {
        public int CarID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public FuelType Fuel { get; set; }
        public double CostToMake { get; set; }

        public abstract double AverageCostPerMile(double[] dollarsPerFuel);
        public abstract double FuelRange();
    }
}
