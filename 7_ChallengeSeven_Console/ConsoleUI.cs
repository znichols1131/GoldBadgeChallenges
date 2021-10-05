using _7_ChallengeSeven_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Console
{
    public class ConsoleUI
    {
        private PartyRepository _partyRepo = new PartyRepository();

        private string _dashes = "------------------------------";

        public void Run()
        {
            Populate();
            Run_MainMenu();
        }

        // Dummy data
        private void Populate()
        {
            
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
                PrintTitle("Welcome to Komodo's Barbecue management tool. What would you like to do?");

                Console.WriteLine("1. Document a new barbecue party.\n" +
                    "2. View or update old barbecue parties.\n" +
                    "3. Delete an old barbecue party.\n" +
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

        // Create menu item
        private void Menu_Create()
        {
            Console.Clear();
            PrintTitle("Creating new menu items:");

            
        }

        

        // Update existing menu item
        private void Menu_ViewOrUpdate_All()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing menu items:");

                PrintMenuItemsInList(_partyRepo.GetAllParties());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a meal number to view item " +
                    "or press enter to return to the main menu:\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "":
                        // Return to main menu
                        return;
                    default:
                        
                        break;
                }
            }
        }

        private void Menu_ViewOrUpdate_Specific(int mealNumber)
        {
            
        }

        // Delete existing menu item
        private void Menu_Delete()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing menu items:");

                PrintMenuItemsInList(_partyRepo.GetAllParties());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a meal number to delete item " +
                    "or press enter to return to the main menu:\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "":
                        // Return to main menu
                        return;
                    default:

                        break;
                }
            }
        }


        // Helper methods (if any)
        private void PrintLogo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
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

        private void PrintMenuItemsInList(List<Party> listOfParties)
        {
            if (listOfParties is null || listOfParties.Count == 0)
            {
                Console.WriteLine("There are no barbecue parties at this time.");
            }
            else
            {
                Console.WriteLine("{0,-5}{1,-15}{2,-25}{3,-7}{4,-20}\n",
                        "Party #",
                        "Date",
                        "Purpose",
                        "Tickets Exchanged",
                        "Total Cost");

                foreach (Party party in listOfParties)
                {
                    Console.WriteLine("{0,-5}{1,-15}{2,-25}{3,-7:0,0}${4,-20:0,0.00}",
                        party.PartyID,
                        party.Date.ToString("MMMM dd, yyyy"),
                        party.Purpose,
                        party.TicketsExchanged(),
                        party.TotalCost());
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

        //private List<string> SplitStringIntoIngredients(string input)
        //{
        //    if (input is null || input == "")
        //    {
        //        return null;
        //    }

        //    List<string> ingredients = input.Split(',').ToList();
        //    List<string> formattedIngredients = new List<string>();

        //    if (ingredients is null || ingredients.Count == 0)
        //    {
        //        return null;
        //    }

        //    foreach (string ingredient in ingredients)
        //    {
        //        formattedIngredients.Add(ingredient.Trim().ToLower());
        //    }

        //    return formattedIngredients;

        //}

        //private void UpdateMenuItem(int mealNumber, MenuItem newItem)
        //{
        //    if (newItem is null)
        //    {
        //        Console.WriteLine("\nMeal could not be updated. Press any key to continue.");
        //        Console.ReadLine();
        //        return;
        //    }

        //    bool success = _menuItemRepo.UpdateMenuItemForMealNumber(mealNumber, newItem);
        //    if (success)
        //    {
        //        Console.WriteLine($"\nMeal {newItem.MealName} is updated. Press any key to continue.");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"\nMeal {newItem.MealName} could not be updated. Press any key to continue.");
        //    }

        //    Console.ReadLine();
        //    return;
        //}
    }
}
