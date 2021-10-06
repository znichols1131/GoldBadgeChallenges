using _6_ChallengeSix_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_ChallengeSix_Console
{
    public class ConsoleUI
    {
        private CarRepository _carRepo = new CarRepository();

        private string _dashes = "------------------------------";

        public void Run()
        {
            Populate();
            Run_MainMenu();
        }

        // Dummy data
        private void Populate()
        {
            ElectricCar eCar = new ElectricCar("Tesla", "Model S", 2021, 90000.00);
            eCar.MilesPerKWH = 3.678956d;
            eCar.Capacity_KWH = 110.085578626d;
            _carRepo.CreateCar(eCar);

            GasCar gCar = new GasCar("Hyundai", "Tucson", 2016, 14500.00);
            gCar.MilesPerGallon = 26.0d;
            gCar.Capacity_Gallons = 16.4d;
            _carRepo.CreateCar(gCar);

            HybridCar hCar = new HybridCar("Ford", "Fusion", 2019, 23000);
            hCar.MilesPerKWH = 2.77777777778d;
            hCar.MilesPerGallon = 23.0d;
            hCar.Capacity_KWH = 9.0d;
            hCar.Capacity_Gallons = 16.5d;
            _carRepo.CreateCar(hCar);
        }

        // Main menu
        private void Run_MainMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintLogo();

                Console.WriteLine("\n");
                PrintTitle("Welcome to Komodo's Green Plan tool. What would you like to do?");

                Console.WriteLine("1. Create a new car.\n" +
                    "2. View or update existing cars.\n" +
                    "3. Delete a car.\n" +
                    "4. Quit.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        Menu_Create();
                        break;
                    case "2":
                        Menu_ViewOrUpdate_All();
                        break;
                    case "3":
                        Menu_Delete();
                        break;
                    case "4":
                        // Quit
                        Environment.Exit(0);
                        return;
                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }

        // Create party
        private void Menu_Create()
        {
            Console.Clear();
            PrintTitle("Creating a new car:");

            Car newCar = AskUser_CarInformation();
            if (!(newCar is null))
            {
                bool success = _carRepo.CreateCar(newCar);
                if (success)
                {
                    Console.WriteLine($"\nCar {newCar.Make} {newCar.Model} has been created. Press any key to continue.\n");
                }
                else
                {
                    Console.WriteLine($"\nCar {newCar.Make} {newCar.Model} could not be created. Press any key to continue.\n");
                }
                Console.ReadLine();
                return;
            }
        }

        private Car AskUser_CarInformation()
        {
            Console.Write("Step 1 of 7: ");
            FuelType fuel = AskUser_FuelType();
            if (fuel == FuelType.Null) { return null; }

            Console.WriteLine();
            Console.Write("Step 2 of 7: ");
            string make = AskUser_Make();
            if (make is null) { return null; }

            Console.Write("\nStep 3 of 7: ");
            string model = AskUser_Model();
            if (model is null) { return null; }

            Console.Write("\nStep 4 of 7: ");
            int year = AskUser_Year();
            if (year == 0) { return null; }

            Console.Write("\nStep 5 of 7: ");
            double costToMake = AskUser_CostToMake();
            if (costToMake < 0) { return null; }

            Car newCar;
            double mileage;
            double capacity;

            // Handle properties unique to each car type
            switch(fuel)
            {
                case FuelType.Electric:
                    newCar = new ElectricCar(make, model, year, costToMake);

                    Console.Write("\nStep 6 of 7: ");
                    mileage = AskUser_Mileages(fuel)[0];
                    if (mileage < 0) { return null; }
                    ((ElectricCar)newCar).MilesPerKWH = mileage;

                    Console.Write("\nStep 7 of 7: ");
                    capacity = AskUser_Capacities(fuel)[0];
                    if (capacity < 0) { return null; }
                    ((ElectricCar)newCar).Capacity_KWH = capacity;

                    return newCar;

                case FuelType.GasPowered:
                    newCar = new GasCar(make, model, year, costToMake);

                    Console.Write("\nStep 6 of 7: ");
                    mileage = AskUser_Mileages(fuel)[0];
                    if (mileage < 0) { return null; }
                    ((GasCar)newCar).MilesPerGallon = mileage;

                    Console.Write("\nStep 7 of 7: ");
                    capacity = AskUser_Capacities(fuel)[0];
                    if (capacity < 0) { return null; }
                    ((GasCar)newCar).Capacity_Gallons = capacity;

                    return newCar;

                case FuelType.Hybrid:
                    newCar = new HybridCar(make, model, year, costToMake);

                    Console.Write("\nStep 6 of 7: ");
                    double[] mileages = AskUser_Mileages(fuel);
                    if (mileages is null || mileages.Length < 2) { return null; }
                    ((HybridCar)newCar).MilesPerKWH = mileages[0];
                    ((HybridCar)newCar).MilesPerGallon = mileages[1];

                    Console.Write("\nStep 7 of 7: ");
                    double[] capacities = AskUser_Capacities(fuel);
                    if (capacities is null || capacities.Length < 2) { return null; }
                    ((HybridCar)newCar).Capacity_KWH = capacities[0];
                    ((HybridCar)newCar).Capacity_Gallons = capacities[1];

                    return newCar;

                default:
                    return null;
            }

            return newCar;
        }

        private FuelType AskUser_FuelType()
        {
            // Get fuel type
            Console.WriteLine("What type of car would you like to create?\n" +
                "1. Electric.\n" +
                "2. Gas-powered.\n" +
                "3. Hybrid.\n");
            string fuelStr = Console.ReadLine();
            if (!ValidateStringResponse(fuelStr, true))
            {
                PrintErrorMessageForInput(fuelStr);
                return FuelType.Null;
            }

            FuelType fuel;
            switch(fuelStr.Trim().ToLower())
            {
                case "1":
                    return FuelType.Electric;
                case "2":
                    return FuelType.GasPowered;
                case "3":
                    return FuelType.Hybrid;
                default:
                    return FuelType.Null;
            }

            return FuelType.Null;
        }

        private string AskUser_Make()
        {
            // Get car make
            Console.WriteLine("Enter the car's make:");
            string make = Console.ReadLine();
            if (!ValidateStringResponse(make, true))
            {
                PrintErrorMessageForInput(make);
                return null;
            }

            return make;
        }

        private string AskUser_Model()
        {
            // Get car model
            Console.WriteLine("Enter the car's model:");
            string model = Console.ReadLine();
            if (!ValidateStringResponse(model, true))
            {
                PrintErrorMessageForInput(model);
                return null;
            }

            return model;
        }

        private int AskUser_Year()
        {
            // Get year
            Console.WriteLine("Enter car's year:");
            string yearStr = Console.ReadLine();
            int year;
            if (!ValidateStringResponse(yearStr, true))
            {
                PrintErrorMessageForInput(yearStr);
                return -1;
            }
            else
            {
                try
                {
                    year = int.Parse(yearStr);
                }
                catch
                {
                    PrintErrorMessageForInput(yearStr);
                    return -1;
                }
            }

            return year;
        }

        private double AskUser_CostToMake()
        {
            // Get cost to make
            Console.WriteLine("Enter a production cost in dollars:");
            string costStr = Console.ReadLine();
            double cost;
            if (!ValidateStringResponse(costStr, true))
            {
                PrintErrorMessageForInput(costStr);
                return -1.0d;
            }
            else
            {
                try
                {
                    cost = double.Parse(costStr.Trim('$'));
                }
                catch
                {
                    PrintErrorMessageForInput(costStr);
                    return -1.0d;
                }
            }

            return cost;
        }

        private double[] AskUser_Mileages(FuelType fuel)
        {
            // Get mileages
            switch(fuel)
            {
                case FuelType.Electric:
                    Console.WriteLine("Enter car mileage in miles per kWh:");
                    break;
                case FuelType.GasPowered:
                    Console.WriteLine("Enter car mileage in miles per gallon:");
                    break;
                case FuelType.Hybrid:
                    Console.WriteLine("Enter mileages (miles per kWh, miles per gallon) separated by commas:");
                    break;
                default:
                    return null;
            }

            string mileageStr = Console.ReadLine();
            if (!ValidateStringResponse(mileageStr, true))
            {
                PrintErrorMessageForInput(mileageStr);
                return null;
            }
            double[] mileages = SplitStringIntoDoubles(mileageStr);
            if (mileages is null || mileages.Length == 0)
            {
                return null;
            }

            return mileages;
        }

        private double[] AskUser_Capacities(FuelType fuel)
        {
            // Get capacities
            switch (fuel)
            {
                case FuelType.Electric:
                    Console.WriteLine("Enter fuel capacity in kWh:");
                    break;
                case FuelType.GasPowered:
                    Console.WriteLine("Enter fuel capacity in gallons:");
                    break;
                case FuelType.Hybrid:
                    Console.WriteLine("Enter fuel capacities (kWh, gallons) separated by commas:");
                    break;
                default:
                    return null;
            }

            string capacityStr = Console.ReadLine();
            if (!ValidateStringResponse(capacityStr, true))
            {
                PrintErrorMessageForInput(capacityStr);
                return null;
            }
            double[] capacities = SplitStringIntoDoubles(capacityStr);
            if (capacities is null || capacities.Length == 0)
            {
                return null;
            }

            return capacities;
        }


        // Update existing parties
        private void Menu_ViewOrUpdate_All()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing cars:");

                PrintCarsInList(_carRepo.GetAllCars());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a car ID to view item " +
                    "or press enter to return to the main menu:\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "":
                        // Return to main menu
                        return;
                    default:
                        try
                        {
                            int carID = int.Parse(response.Trim());
                            Menu_ViewOrUpdate_Specific(carID);
                        }
                        catch
                        {
                            PrintErrorMessageForInput(response);
                        }
                        break;
                }
            }
        }

        private void Menu_ViewOrUpdate_Specific(int carID)
        {
            Car car = _carRepo.GetCarForID(carID);
            if (car is null)
            {
                PrintErrorMessageForInput($"Car ID: {carID}");
            }

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Viewing car:");

                Console.WriteLine("{0,-20}{1,-20}", $"Car ID:", car.CarID);
                Console.WriteLine("{0,-20}{1,-20}", $"Make:", car.Make);
                Console.WriteLine("{0,-20}{1,-20}", "Model:", car.Model);
                Console.WriteLine("{0,-20}{1,-20}", "Year:", car.Year);

                switch (car.Fuel)
                {
                    case FuelType.Electric:
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Type:", "Electric");
                        break;
                    case FuelType.GasPowered:
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Type:", "Gas");
                        break;
                    case FuelType.Hybrid:
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Type:", "Hybrid");
                        break;
                    default:
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Type:", "N/A");
                        break;
                }

                Console.WriteLine("{0,-20}${1,-20:0,0.00}", "Production Cost:", car.CostToMake);

                switch (car.Fuel)
                {
                    case FuelType.Electric:
                        Console.WriteLine("{0,-20}{1,-20}", "Electric Specs", "");
                        Console.WriteLine("{0,-20}{1,-20}", "Mileage:", String.Format("{0:0.00}", ((ElectricCar)car).MilesPerKWH) + " miles/kWh");
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Capacity:", String.Format("{0:0.0}", ((ElectricCar)car).Capacity_KWH) + " kWh");
                        break;
                    case FuelType.GasPowered:
                        Console.WriteLine("{0,-20}{1,-20}", "Gas Specs", "");
                        Console.WriteLine("{0,-20}{1,-20}", "Mileage:", String.Format("{0:0.00}", ((GasCar)car).MilesPerGallon) + " miles/gallon");
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Capacity:", String.Format("{0:0.0}",((GasCar)car).Capacity_Gallons) + " gallons");
                        break;
                    case FuelType.Hybrid:
                        Console.WriteLine("{0,-20}{1,-20}", "Electric Specs", "");
                        Console.WriteLine("{0,-20}{1,-20}", "Mileage:", String.Format("{0:0.00}", ((HybridCar)car).MilesPerKWH) + " miles/kWh");
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Capacity:", String.Format("{0:0.0}", ((HybridCar)car).Capacity_KWH) + " kWh");
                        Console.WriteLine();
                        Console.WriteLine("{0,-20}{1,-20}", "Gas Specs:", "");
                        Console.WriteLine("{0,-20}{1,-20}", "Mileage:", String.Format("{0:0.00}",((HybridCar)car).MilesPerGallon) + " miles/gallon");
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Capacity:", String.Format("{0:0.0}", ((HybridCar)car).Capacity_Gallons) + " gallons");
                        break;
                    default:
                        Console.WriteLine("{0,-20}{1,-20}", "Fuel Specs:", "N/A");
                        break;
                }

                Console.WriteLine("\n" + _dashes + "\n\nWhat would you like to do?\n" +
                    "1. Update make.\n" +
                    "2. Update model.\n" +
                    "3. Update year.\n" +
                    "4. Update production cost.\n" +
                    "5. Update mileages.\n" +
                    "6. Update capacities.\n" +
                    "7. Return to previous menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Update make
                        string make = AskUser_Make();
                        if (!(make is null))
                        {
                            car.Make = make;
                            UpdateCar(car.CarID, car);
                        }
                        break;

                    case "2":
                        // Update model
                        string model = AskUser_Model();
                        if (!(model is null))
                        {
                            car.Model = model;
                            UpdateCar(car.CarID, car);
                        }
                        break;

                    case "3":
                        // Update year
                        int year = AskUser_Year();
                        if (!(year < 0))
                        {
                            car.Year = year;
                            UpdateCar(car.CarID, car);
                        }
                        break;

                    case "4":
                        // Update cost to make
                        double costToMake = AskUser_CostToMake();
                        if (!(costToMake < 0))
                        {
                            car.CostToMake = costToMake;
                            UpdateCar(car.CarID, car);
                        }
                        break;

                    case "5":
                        // Update mileages
                        double[] mileages = AskUser_Mileages(car.Fuel);
                        if (!(mileages is null) && !(mileages.Length < 0))
                        {
                            // Handle properties unique to each car type
                            switch (car.Fuel)
                            {
                                case FuelType.Electric:
                                    if (mileages[0] < 0) { break; }
                                    ((ElectricCar)car).MilesPerKWH = mileages[0];
                                    UpdateCar(car.CarID, car);
                                    break;

                                case FuelType.GasPowered:
                                    if (mileages[0] < 0) { break; }
                                    ((GasCar)car).MilesPerGallon = mileages[0];
                                    UpdateCar(car.CarID, car);
                                    break;

                                case FuelType.Hybrid:
                                    if (mileages[0] < 0) { break; }
                                    ((HybridCar)car).MilesPerKWH = mileages[0];
                                    ((HybridCar)car).MilesPerGallon = mileages[1];
                                    UpdateCar(car.CarID, car);
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;

                    case "6":
                        // Update capacities
                        // Update mileages
                        double[] capacities = AskUser_Capacities(car.Fuel);
                        if (!(capacities is null) && !(capacities.Length < 0))
                        {
                            // Handle properties unique to each car type
                            switch (car.Fuel)
                            {
                                case FuelType.Electric:
                                    if (capacities[0] < 0) { break; }
                                    ((ElectricCar)car).Capacity_KWH = capacities[0];
                                    UpdateCar(car.CarID, car);
                                    break;

                                case FuelType.GasPowered:
                                    if (capacities[0] < 0) { break; }
                                    ((GasCar)car).Capacity_Gallons = capacities[0];
                                    UpdateCar(car.CarID, car);
                                    break;

                                case FuelType.Hybrid:
                                    if (capacities[0] < 0) { break; }
                                    ((HybridCar)car).Capacity_KWH = capacities[0];
                                    ((HybridCar)car).Capacity_Gallons = capacities[1];
                                    UpdateCar(car.CarID, car);
                                    break;

                                default:
                                    break;
                            }
                        }
                        break;

                    case "7":
                        // Return to main menu
                        return;
                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }


        // Delete existing party
        private void Menu_Delete()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing cars:");

                PrintCarsInList(_carRepo.GetAllCars());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a car ID to delete car " +
                    "or press enter to return to the main menu:\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "":
                        // Return to main menu
                        return;
                    default:
                        try
                        {
                            int carID = int.Parse(response.Trim());
                            bool success = _carRepo.DeleteCarForID(carID);

                            if (success)
                            {
                                Console.WriteLine($"\nCar was successfully deleted. Press any key to continue.");
                            }
                            else
                            {
                                Console.WriteLine($"\nCar could not be deleted at this time. Press any key to continue.");
                            }
                            Console.ReadLine();
                        }
                        catch
                        {
                            PrintErrorMessageForInput(response);
                        }
                        break;
                }
            }
        }


        // Helper methods (if any)
        private void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" __  ___   ______    .___  ___.   ______    _______   ______   ");
            Console.WriteLine("|  |/  /  /  __  \\   |   \\/   |  /  __  \\  |       \\ /  __  \\  ");
            Console.WriteLine("|  '  /  |  |  |  |  |  \\  /  | |  |  |  | |  .--.  |  |  |  | ");
            Console.WriteLine("|     <  |  |  |  |  |  |\\/|  | |  |  |  | |  |  |  |  |  |  |");
            Console.WriteLine("|  .   \\ |  `--'  |  |  |  |  | |  `--'  | |  '--'  |  `--'  | ");
            Console.WriteLine("|__|\\ __\\ \\______/   |__|  |__|  \\______/  |_______/ \\______/");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void PrintTitle(string title)
        {
            Console.WriteLine(title + "\n\n" + _dashes + "\n");
        }

        private void PrintErrorMessageForInput(string input)
        {
            Console.WriteLine($"\nWe're sorry, '{input}' is not a valid input. Please try again.");
            Console.ReadLine();
        }

        private void PrintCarsInList(List<Car> carList)
        {
            if (carList is null || carList.Count == 0)
            {
                Console.WriteLine("There are no cars at this time.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-15}{2,-15}{3,-10}${4,-15}",
                        "ID",
                        "Make",
                        "Model",
                        "Year",
                        "Cost to Make");

                foreach (Car car in carList)
                {
                    Console.WriteLine("{0,-10}{1,-15}{2,-15}{3,-10}${4,-15:0,0.00}",
                        car.CarID,
                        car.Make,
                        car.Model,
                        car.Year,
                        car.CostToMake);
                }
            }
        }

        private bool InterpretYesNoInput(string input)
        {
            if (input is null || input == "")
            {
                return false;
            }

            if (input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
            {
                return true;
            }

            return false;
        }

        private double[] SplitStringIntoDoubles(string input)
        {
            if (input is null || input == "")
            {
                return null;
            }

            List<string> stringValues = input.Split(',').ToList();
            List<double> formattedValues = new List<double>();

            if (stringValues is null || stringValues.Count == 0)
            {
                return null;
            }

            try
            {
                foreach (string value in stringValues)
                {
                    formattedValues.Add(double.Parse(value.Trim().ToLower()));
                }
            }
            catch
            {
                return null;
            }
            

            return formattedValues.ToArray();

        }

        private bool ValidateStringResponse(string response, bool required)
        {
            if (response is null)
            {
                return false;
            }

            if (response == "" && required)
            {
                return false;
            }

            return true;
        }

        private void UpdateCar(int carID, Car newCar)
        {
            if (newCar is null)
            {
                Console.WriteLine("\nCar could not be updated. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            bool success = _carRepo.UpdateCarForID(carID, newCar);
            if (success)
            {
                Console.WriteLine($"\nMeal {newCar.Make} {newCar.Model} is updated. Press any key to continue.");
            }
            else
            {
                Console.WriteLine($"\nMeal {newCar.Make} {newCar.Model} could not be updated. Press any key to continue.");
            }

            Console.ReadLine();
            return;
        }
    }
}
