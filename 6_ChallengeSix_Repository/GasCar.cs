using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_ChallengeSix_Repository
{
    public class GasCar : Car
    {
        // Parent class properties are in Car class

        // Unique properties
        public double MilesPerGallon { get; set; }
        public double Capacity_Gallons { get; set; }


        // Constructors
        public GasCar() { }

        public GasCar(string make, string model, int year)
        {
            Make = make;
            Model = model;
            Year = year;
            Fuel = FuelType.GasPowered;
        }
        public GasCar(string make, string model, int year, double costToMake)
        {
            Make = make;
            Model = model;
            Year = year;
            CostToMake = costToMake;
            Fuel = FuelType.GasPowered;
        }

        // Implement parent class methods
        public override double AverageCostPerMile(double[] dollarsPerFuel)
        {
            // Expects $/kWH and $/gallon as arguments
            return dollarsPerFuel[1] / MilesPerGallon;
        }

        public override double FuelRange()
        {
            return MilesPerGallon * Capacity_Gallons;
        }
    }
}
