using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_ChallengeSix_Repository
{
    public class ElectricCar : Car
    {
        // Parent class properties
        public int CarID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public FuelType Fuel { get; set; }
        public double CostToMake { get; set; }

        // Unique properties
        public double MilesPerKWH { get; set; }
        public double Capacity_KWH { get; set; }


        // Constructors
        public ElectricCar() { }

        public ElectricCar(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
            Fuel = FuelType.Electric;
        }
        public ElectricCar(string make, string model, int year, double costToMake)
        {
            Make = make;
            Model = model;
            Year = year;
            CostToMake = costToMake;
            Fuel = FuelType.Electric;
        }

        // Implement parent class methods
        public override double AverageCostPerMile(double[] dollarsPerFuel)
        {
            // Expects $/kWH and $/gallon as arguments
            return dollarsPerFuel[0] / MilesPerKWH;
        }

        public override double FuelRange()
        {
            return MilesPerKWH * Capacity_KWH;
        }
    }
}
