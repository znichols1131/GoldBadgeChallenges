using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_ChallengeSix_Repository
{
    public class HybridCar : Car
    {
        // Parent class properties are in Car class

        // Unique properties
        public double MilesPerKWH { get; set; }
        public double Capacity_KWH { get; set; }
        public double MilesPerGallon { get; set; }
        public double Capacity_Gallons { get; set; }

        // Constructors
        public HybridCar() { }

        public HybridCar(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
            Fuel = FuelType.Hybrid;
        }
        public HybridCar(string make, string model, int year, double costToMake)
        {
            Make = make;
            Model = model;
            Year = year;
            CostToMake = costToMake;
            Fuel = FuelType.Hybrid;
        }

        // Implement parent class methods
        public override double AverageCostPerMile(double[] dollarsPerFuel)
        {
            // Expects $/kWH and $/gallon as arguments
            double electricRange = MilesPerKWH * Capacity_KWH;
            double gasRange = MilesPerGallon * Capacity_Gallons;

            // Return the average cost per mile by interpolating
            // by electricRange and gasRange
            return ((electricRange * dollarsPerFuel[0] / MilesPerKWH) +
                (gasRange * dollarsPerFuel[1] / MilesPerGallon)) /
                (electricRange + gasRange);
        }

        public override double FuelRange()
        {
            // Return total range (electric + gas)
            return MilesPerKWH * Capacity_KWH + MilesPerGallon * Capacity_Gallons;
        }
    }
}
