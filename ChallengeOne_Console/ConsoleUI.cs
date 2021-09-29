using ChallengeOne_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeOne_Console
{
    public class ConsoleUI
    {
        private MenuItemRepository _menuItemRepo = new MenuItemRepository();

        private string _dashes = "------------------------------";

        public void Run()
        {
            Populate();
            Run_MainMenu();
        }

        // Dummy data
        public void Populate()
        {
            List<string> ingredientListOne = new List<string>();
            ingredientListOne.Add("buns");
            ingredientListOne.Add("burger patty");
            ingredientListOne.Add("ketchup");
            MenuItem itemOne = new MenuItem("Hamburger", "Plain ol' hamburger", ingredientListOne, 4.99d);
            _menuItemRepo.CreateMenuItem(itemOne);

            List<string> ingredientListTwo = new List<string>();
            ingredientListTwo.Add("buns");
            ingredientListTwo.Add("burger patty");
            ingredientListTwo.Add("ketchup");
            ingredientListTwo.Add("cheese");
            MenuItem itemTwo = new MenuItem("Cheeseburger", "Wisconsin's finest innovation", ingredientListTwo, 5.99d);
            _menuItemRepo.CreateMenuItem(itemTwo);

            List<string> ingredientListThree = new List<string>();
            ingredientListThree.Add("buns");
            ingredientListThree.Add("veggie patty");
            MenuItem itemThree = new MenuItem("Veggie burger", "Vegan friendly", ingredientListThree, 5.99d);
            _menuItemRepo.CreateMenuItem(itemThree);
        }

        // Main menu
        public void Run_MainMenu()
        {
            bool keepLooping = true;
            while(keepLooping)
            {
                Console.Clear();
                PrintLogo();

                Console.WriteLine("\n");
                PrintTitle("Welcome to Komodo Cafe's menu management tool. What would you like to do?");

                Console.WriteLine("1. View menu items.\n" +
                    "2. Create a new menu item.\n" +
                    "3. Update an existing menu item.\n" +
                    "4. Delete a new menu item.\n" +
                    "5. Quit.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        Run_MealViewMenu();
                        break;
                    case "2":
                        Run_CreationMenu();
                        break;
                    case "3":
                        Run_UpdateMenu();
                        break;
                    case "4":
                        Run_DeletionMenu();
                        break;
                    case "5":
                        // Quit
                        Environment.Exit(0);
                        return;
                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }

        // View menu items
        public void Run_MealViewMenu()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing menu items:");

                PrintMenuItemsInList(_menuItemRepo.GetAllMenuItems());

                Console.WriteLine("\n"+_dashes+"\n\nPress any key to return to the main menu.");
                Console.ReadLine();
                return;
            }
        }

        // Create menu item
        public void Run_CreationMenu()
        {
            Console.Clear();
            PrintTitle("Creating new menu items:");

            MenuItem newItem = AskUserForMenuInformation();
            if (!(newItem is null))
            {
                bool success = _menuItemRepo.CreateMenuItem(newItem);
                if (success)
                {
                    Console.WriteLine($"\nMenu item {newItem.MealName} has been created. Press any key to coninue.\n");
                }
                else
                {
                    Console.WriteLine($"\nMenu item {newItem.MealName} could not be created. Press any key to coninue.\n");
                }
                Console.ReadLine();
                return;
            }
        }

        public MenuItem AskUserForMenuInformation()
        {
            // Get meal
            Console.WriteLine("Step 1 of 4: Enter a name for the meal:");
            string mealName = Console.ReadLine();
            if (!ValidateStringResponse(mealName, true))
            {
                PrintErrorMessageForInput(mealName);
                return null;
            }

            // Get description
            Console.WriteLine("\nStep 2 of 4: Enter a description for the meal (optional):");
            string mealDescription = Console.ReadLine();
            if (!ValidateStringResponse(mealDescription, false))
            {
                PrintErrorMessageForInput(mealDescription);
                return null;
            }

            // Get ingredients
            Console.WriteLine("\nStep 3 of 4: Enter all ingredients separated by commas:");
            string ingredientStr = Console.ReadLine();
            if(!ValidateStringResponse(ingredientStr, true))
            {
                PrintErrorMessageForInput(ingredientStr);
                return null;
            }
            List<string> ingredients = SplitStringIntoIngredients(ingredientStr);
            if(ingredients is null || ingredients.Count==0)
            {
                return null;
            }

            // Get price
            Console.WriteLine("\nStep 4 of 4: Enter a price for the meal (in dollars):");
            string priceStr = Console.ReadLine();
            double price;
            if (!ValidateStringResponse(priceStr, true))
            {
                PrintErrorMessageForInput(priceStr);
                return null;
            }else
            {
                try
                {
                    price = double.Parse(priceStr.Trim('$'));
                }
                catch
                {
                    PrintErrorMessageForInput(priceStr);
                    return null;
                }
            }

            // Return MenuItem based on inputs
            MenuItem newItem = new MenuItem(mealName, mealDescription, ingredients, price);
            if(newItem is null)
            {
                PrintErrorMessageForInput(mealName);
                return null;
            }

            return newItem;
        }



        // Update existing menu item
        public void Run_UpdateMenu()
        {

        }



        // Delete existing menu item
        public void Run_DeletionMenu()
        {

        }


        // Helper methods (if any)
        public void PrintLogo()
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

        public void PrintTitle(string title)
        {
            Console.WriteLine(title + "\n\n" + _dashes + "\n");
        }
        
        public void PrintErrorMessageForInput(string input)
        {
            Console.WriteLine($"\nWe're sorry, '{input}' is not a valid input. Please try again.");
            Console.ReadLine();
        }

        public void PrintMenuItemsInList(List<MenuItem> listOfItems)
        {
            if(listOfItems is null || listOfItems.Count == 0)
            {
                Console.WriteLine("There are no menu items at this time.");
            }
            else
            {
                foreach(MenuItem item in listOfItems)
                {
                    Console.WriteLine("{0,-10}{1,-30}{2,-10}",
                        item.MealNumber,
                        item.MealName,
                        item.Price);
                }
            }
        }

        public bool InterpretYesNoInput(string input)
        {
            if(input is null || input == "")
            {
                return false;
            }
            
            if(input.Trim().ToLower() == "y" || input.Trim().ToLower() == "yes")
            {
                return true;
            }

            return false;
        }

        public bool ValidateStringResponse(string response, bool required)
        {
            if(response is null)
            {
                return false;
            }

            if(response == "" && required)
            {
                return false;
            }

            return true;
        }

        public List<string> SplitStringIntoIngredients(string input)
        {
            if(input is null || input == "")
            {
                return null;
            }

            List<string> ingredients = input.Split(',').ToList();
            List<string> formattedIngredients = new List<string>();

            if(ingredients is null || ingredients.Count == 0)
            {
                return null;
            }

            foreach(string ingredient in ingredients)
            {
                formattedIngredients.Add(ingredient.Trim().ToLower());
            }

            return formattedIngredients;

        }
    }
}
