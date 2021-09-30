using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_ChallengeSix_Repository
{
    public class CarRepository
    {
        private List<Car> _listOfCars = new List<Car>();
        private int _nextCarNumber = 0;

        // Create
        public bool CreateCar(Car car)
        {
            if (car is null)
            {
                return false;
            }

            // Keep meal numbers unique
            car.CarID = _nextCarNumber;
            _nextCarNumber++;

            int before = _listOfCars.Count();
            _listOfCars.Add(car);
            int after = _listOfCars.Count();

            if (before < after)
            {
                return true;
            }

            return false;
        }

        // Read
        public List<Car> GetAllCars()
        {
            return _listOfCars;
        }

        public Car GetCarForID(int carID)
        {
            if (carID < 0 || _listOfCars.Count == 0)
            {
                return null;
            }

            foreach (Car car in _listOfCars)
            {
                if (car.CarID == carID)
                {
                    return car;
                }
            }

            return null;
        }

        // Update
        public bool UpdateCarForID(int carID, Car newCar)
        {
            if (carID < 0 || newCar is null || _listOfCars.Count == 0)
            {
                return false;
            }

            Car oldCar = GetCarForID(carID);
            if (oldCar is null || oldCar.Fuel != newCar.Fuel)
            {
                return false;
            }

            oldCar.CarID = newCar.CarID;
            oldCar.Make = newCar.Make;
            oldCar.Model = newCar.Model;
            oldCar.Year = newCar.Year;
            oldCar.Fuel = newCar.Fuel;
            oldCar.CostToMake = newCar.CostToMake;

            // Handle any properties unique to fuel type
            switch(oldCar.Fuel)
            {
                case FuelType.Electric:
                    ((ElectricCar)oldCar).MilesPerKWH = ((ElectricCar)newCar).MilesPerKWH;
                    ((ElectricCar)oldCar).Capacity_KWH = ((ElectricCar)newCar).Capacity_KWH;
                    break;
                case FuelType.GasPowered:
                    ((GasCar)oldCar).MilesPerGallon = ((GasCar)newCar).MilesPerGallon;
                    ((GasCar)oldCar).Capacity_Gallons = ((GasCar)newCar).Capacity_Gallons;
                    break;
                case FuelType.Hybrid:
                    ((HybridCar)oldCar).MilesPerKWH = ((HybridCar)newCar).MilesPerKWH;
                    ((HybridCar)oldCar).Capacity_KWH = ((HybridCar)newCar).Capacity_KWH;
                    ((HybridCar)oldCar).MilesPerGallon = ((HybridCar)newCar).MilesPerGallon;
                    ((HybridCar)oldCar).Capacity_Gallons = ((HybridCar)newCar).Capacity_Gallons;
                    break;
                default:
                    return false;
            }

            return true;
        }

        // Delete
        public bool DeleteCarForID(int carID)
        {
            Car car = GetCarForID(carID);
            if (car is null || _listOfCars.Count == 0)
            {
                return false;
            }

            int before = _listOfCars.Count();
            _listOfCars.Remove(car);
            int after = _listOfCars.Count();

            if (before > after)
            {
                return true;
            }

            return false;
        }

        // Helper methods (if any)
    }
}
