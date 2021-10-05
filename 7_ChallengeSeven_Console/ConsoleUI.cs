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
        private string _dateFormat = "MMM dd, yyyy";

        public void Run()
        {
            Populate();
            Run_MainMenu();
        }

        // Dummy data
        private void Populate()
        {
            _partyRepo.CreateParty(new Party("Zach's birthday", DateTime.Parse("09/07/2020")));
            _partyRepo.CreateParty(new Party("Bruce's retirement", DateTime.Parse("01/14/2021")));
            _partyRepo.CreateParty(new Party("Beesly's adoption", DateTime.Parse("02/07/2021")));
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
                        Menu_CreateParty();
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
        private void Menu_CreateParty()
        {
            Console.Clear();
            PrintTitle("Creating new barbecue party:");

            Party newParty = AskUserForParty();
            if (!(newParty is null))
            {
                bool success = _partyRepo.CreateParty(newParty);
                if (success)
                {
                    Console.WriteLine($"\nParty {newParty.Purpose} on {newParty.Date.ToString(_dateFormat)} has been created. Press any key to continue.\n");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"\nParty could not be created. Press any key to continue.\n");
                    Console.ReadLine();
                    return;
                }
            }else
            {
                Console.WriteLine($"\nParty could not be created. Press any key to continue.\n");
                Console.ReadLine();
                return;
            }

            // Go into update mode for that new party
            Menu_ViewOrUpdate_Specific(newParty.PartyID);

        }

        private Party AskUserForParty()
        {
            Console.Write("Step 1 of 2: ");
            string purpose = AskUser_StringInput("Enter the purpose for the barbecue party:");
            if (purpose is null) { return null; }

            Console.Write("\nStep 2 of 2: ");
            var date = AskUser_DateInput("Enter the party date (MM/DD/YYYY):");     // How to check if it's null since DateTime isn't nullable
            if (!date.HasValue) { return null; }

            Party newParty = new Party(purpose, (DateTime)date);
            return newParty;
        }
        private string AskUser_StringInput(string prompt)
        {
            if(prompt is null || prompt == "")
            {
                return null;
            }

            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            if (!ValidateStringResponse(response, true))
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            return response;
        }

        private DateTime? AskUser_DateInput(string prompt)
        {
            if (prompt is null || prompt == "")
            {
                return null;
            }

            Console.WriteLine(prompt);
            string response = Console.ReadLine();
            if (!ValidateDateResponse(response, true))
            {
                PrintErrorMessageForInput(response);
                return null;
            }

            DateTime date;
            date = DateTime.Parse(response);
            return date;
        }


        // Update existing menu item
        private void Menu_ViewOrUpdate_All()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing barbecue parties:");

                PrintMenuItemsInList(_partyRepo.GetAllParties());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a party number to view barbecue party " +
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
                            int partyID = int.Parse(response.Trim());
                            Menu_ViewOrUpdate_Specific(partyID);
                        }
                        catch
                        {
                            PrintErrorMessageForInput(response);
                        }
                        break;
                }
            }
        }

        private void Menu_ViewOrUpdate_Specific(int partyID)
        {
            Party party = _partyRepo.GetPartyForID(partyID);
            if (party is null)
            {
                PrintErrorMessageForInput($"Party ID: {partyID}");
            }

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Viewing barbecue party:");

                Console.WriteLine("{0,-20}{1,-20}", "Party ID:", party.PartyID);
                Console.WriteLine("{0,-20}{1,-20}", "Date:", party.Date.ToString(_dateFormat));
                Console.WriteLine("{0,-20}{1,-20}", "Purpose:", party.Purpose);

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

                        break;

                    case "2":

                        break;

                    case "3":

                        break;

                    case "4":

                        break;

                    case "5":

                        break;

                    case "6":
                        
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

        // Delete existing menu item
        private void Menu_Delete()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing barbecue parties:");

                PrintMenuItemsInList(_partyRepo.GetAllParties());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a party number to delete barbecue party " +
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
                            int partyID = int.Parse(response.Trim());
                            bool success = _partyRepo.DeletePartyForID(partyID);

                            if (success)
                            {
                                Console.WriteLine($"\nParty was successfully deleted. Press any key to continue.");
                            }
                            else
                            {
                                Console.WriteLine($"\nParty could not be deleted at this time. Press any key to continue.");
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
                Console.WriteLine("{0,-10}{1,-15}{2,-25}{3,-10}{4,-20}\n",
                        "Party #",
                        "Date",
                        "Purpose",
                        "Tickets",
                        "Total Cost");

                foreach (Party party in listOfParties)
                {
                    Console.WriteLine("{0,-10}{1,-15}{2,-25}{3,-10:N0}${4,-20:0,0.00}",
                        party.PartyID,
                        party.Date.ToString(_dateFormat),
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

        private bool ValidateDateResponse(string response, bool required)
        {
            if(ValidateStringResponse(response, required))
            {
                try
                {
                    DateTime date = DateTime.Parse(response);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return false;
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
