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
        private void Populate()
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
        private void Run_MainMenu()
        {
            bool keepLooping = true;
            while(keepLooping)
            {
                Console.Clear();
                PrintLogo();

                Console.WriteLine("\n");
                PrintTitle("Welcome to Komodo Cafe's menu management tool. What would you like to do?");

                Console.WriteLine("1. Create a new menu item.\n" +
                    "2. View or update existing menu items.\n" +
                    "3. Delete a menu item.\n" +
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

            MenuItem newItem = AskUser_MenuInformation();
            if (!(newItem is null))
            {
                bool success = _menuItemRepo.CreateMenuItem(newItem);
                if (success)
                {
                    Console.WriteLine($"\nMenu item {newItem.MealName} has been created. Press any key to continue.\n");
                }
                else
                {
                    Console.WriteLine($"\nMenu item {newItem.MealName} could not be created. Press any key to continue.\n");
                }
                Console.ReadLine();
                return;
            }
        }

        private MenuItem AskUser_MenuInformation()
        {
            Console.Write("Step 1 of 4: ");
            string mealName = AskUser_MealName();
            if(mealName is null) { return null; }

            Console.Write("\nStep 2 of 4: ");
            string mealDescription = AskUser_MealDescription();
            if (mealDescription is null) { return null; }

            Console.Write("\nStep 3 of 4: ");
            List<string> ingredients = AskUser_Ingredients();
            if (ingredients is null || ingredients.Count == 0) { return null; }

            Console.Write("\nStep 4 of 4: ");
            double price = AskUser_Price();
            if (price < 0) { return null; }

            // Return MenuItem based on inputs
            MenuItem newItem = new MenuItem(mealName, mealDescription, ingredients, price);
            if(newItem is null)
            {
                PrintErrorMessageForInput(mealName);
                return null;
            }

            return newItem;
        }

        private string AskUser_MealName()
        {
            // Get meal name
            Console.WriteLine("Enter a name for the meal:");
            string mealName = Console.ReadLine();
            if (!ValidateStringResponse(mealName, true))
            {
                PrintErrorMessageForInput(mealName);
                return null;
            }

            return mealName;
        }

        private string AskUser_MealDescription()
        {
            // Get meal description
            Console.WriteLine("Enter a description for the meal:");
            string mealDescription = Console.ReadLine();
            if (!ValidateStringResponse(mealDescription, true))
            {
                PrintErrorMessageForInput(mealDescription);
                return null;
            }

            return mealDescription;
        }

        private List<string> AskUser_Ingredients()
        {
            // Get ingredients
            Console.WriteLine("Enter all ingredients separated by commas:");
            string ingredientStr = Console.ReadLine();
            if (!ValidateStringResponse(ingredientStr, true))
            {
                PrintErrorMessageForInput(ingredientStr);
                return null;
            }
            List<string> ingredients = SplitStringIntoIngredients(ingredientStr);
            if (ingredients is null || ingredients.Count == 0)
            {
                return null;
            }

            return ingredients;
        }

        private double AskUser_Price()
        {
            // Get price
            Console.WriteLine("Enter a price for the meal (in dollars):");
            string priceStr = Console.ReadLine();
            double price;
            if (!ValidateStringResponse(priceStr, true))
            {
                PrintErrorMessageForInput(priceStr);
                return -1.0d;
            }
            else
            {
                try
                {
                    price = double.Parse(priceStr.Trim('$'));
                }
                catch
                {
                    PrintErrorMessageForInput(priceStr);
                    return -1.0d;
                }
            }

            return price;
        }

        // Update existing menu item
        private void Menu_ViewOrUpdate_All()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing menu items:");

                PrintMenuItemsInList(_menuItemRepo.GetAllMenuItems());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a meal number to view item " +
                    "or press enter to return to the main menu:\n");
                string response = Console.ReadLine();

                switch(response)
                {
                    case "":
                        // Return to main menu
                        return;
                    default:
                        try
                        {
                            int mealNumber = int.Parse(response.Trim());
                            Menu_ViewOrUpdate_Specific(mealNumber);
                        }
                        catch
                        {
                            PrintErrorMessageForInput(response);
                        }
                        break;
                }
            }
        }

        private void Menu_ViewOrUpdate_Specific(int mealNumber)
        {
            MenuItem item = _menuItemRepo.GetMenuItemForMealNumber(mealNumber);
            if(item is null)
            {
                PrintErrorMessageForInput($"Meal Number: {mealNumber}");
            }

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Viewing menu item:");

                Console.WriteLine("{0,-15}{1,-20}",$"Meal number:", item.MealNumber);
                Console.WriteLine("{0,-15}{1,-20}", $"Meal name:", item.MealName);
                Console.WriteLine("{0,-15}{1,-20}", $"Description:", item.Description);
                Console.WriteLine("{0,-15}{1,-20}", $"Price:", $"${item.Price}");
                Console.WriteLine("{0,-15}", "Ingredients:");
                if(item.Ingredients.Count == 0)
                {
                    Console.WriteLine("{0,-15}{1,-20}", "", "There are no ingredients at this time.");
                }
                else
                {
                    int i = 1;
                    foreach (string ingredient in item.Ingredients)
                    {
                        Console.WriteLine("{0,-15}{1,-20}", "", $"{i}. {ingredient}");
                        i++;
                    }
                }
                
                Console.WriteLine("\n" + _dashes + "\n\nWhat would you like to do?\n" +
                    "1. Update meal name.\n" +
                    "2. Update meal description.\n" +
                    "3. Update price.\n" +
                    "4. Add ingredients.\n" +
                    "5. Remove ingredients.\n" +
                    "6. Return to previous menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Update meal name
                        string mealName = AskUser_MealName();
                        if (!(mealName is null))
                        { 
                            item.MealName = mealName;
                            UpdateMenuItem(item.MealNumber, item);
                        }
                        break;

                    case "2":
                        // Update meal description
                        string mealDescription = AskUser_MealDescription();
                        if (!(mealDescription is null))
                        {
                            item.Description = mealDescription;
                            UpdateMenuItem(item.MealNumber, item);
                        }
                        break;

                    case "3":
                        // Update price
                        double price = AskUser_Price();
                        if (!(price < 0))
                        {
                            item.Price = price;
                            UpdateMenuItem(item.MealNumber, item);
                        }
                        break;

                    case "4":
                        // Add ingredients
                        List<string> ingredientsToAdd = AskUser_Ingredients();
                        if (!(ingredientsToAdd is null || ingredientsToAdd.Count == 0))
                        {
                            bool success = true;
                            foreach(string ingredient in ingredientsToAdd)
                            {
                                success = (success && _menuItemRepo.AddIngredientToMealNumber(item.MealNumber, ingredient));
                            }

                            if (success)
                            {
                                Console.WriteLine($"\nAll ingredients were successfully added. Press any key to continue.");
                            }
                            else
                            {
                                Console.WriteLine($"\nAt least one ingredient could not be added. Press any key to continue.");
                            }
                            Console.ReadLine();
                        }
                        break;

                    case "5":
                        // Remove ingredients
                        List<string> ingredientsToDelete = AskUser_Ingredients();
                        if (!(ingredientsToDelete is null || ingredientsToDelete.Count == 0))
                        {
                            bool success = true;
                            foreach (string ingredient in ingredientsToDelete)
                            {
                                success = (success && _menuItemRepo.RemoveIngredientFromMealNumber(item.MealNumber, ingredient));
                            }

                            if (success)
                            {
                                Console.WriteLine($"\nAll ingredients were successfully removed. Press any key to continue.");
                            }
                            else
                            {
                                Console.WriteLine($"\nAt least one ingredient could not be removed. Press any key to continue.");
                            }
                            Console.ReadLine();
                        }
                        break;

                    case "6":
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
                PrintTitle("Existing menu items:");

                PrintMenuItemsInList(_menuItemRepo.GetAllMenuItems());

                Console.WriteLine("\n" + _dashes + "\n\nEnter a meal number to delete item " +
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
                            int mealNumber = int.Parse(response.Trim());
                            bool success = _menuItemRepo.DeleteMenuItemForMealNumber(mealNumber);
                            if (success)
                            {
                                Console.WriteLine($"\nMenu item was successfully deleted. Press any key to continue.");
                            }
                            else
                            {
                                Console.WriteLine($"\nMenu item could not be deleted at this time. Press any key to continue.");
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
            Console.ForegroundColor = ConsoleColor.Blue;
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

        private void PrintMenuItemsInList(List<MenuItem> listOfItems)
        {
            if(listOfItems is null || listOfItems.Count == 0)
            {
                Console.WriteLine("There are no menu items at this time.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-25}{2,-10}\n",
                        "Meal #",
                        "Meal Name",
                        "Price");

                foreach (MenuItem item in listOfItems)
                {
                    Console.WriteLine("{0,-10}{1,-25}${2,-10}",
                        item.MealNumber,
                        item.MealName,
                        item.Price);
                }
            }
        }

        private bool InterpretYesNoInput(string input)
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

        private bool ValidateStringResponse(string response, bool required)
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

        private List<string> SplitStringIntoIngredients(string input)
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

        private void UpdateMenuItem(int mealNumber, MenuItem newItem)
        {
            if(newItem is null)
            {
                Console.WriteLine("\nMeal could not be updated. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            bool success = _menuItemRepo.UpdateMenuItemForMealNumber(mealNumber, newItem);
            if (success)
            {
                Console.WriteLine($"\nMeal {newItem.MealName} is updated. Press any key to continue.");
            }
            else
            {
                Console.WriteLine($"\nMeal {newItem.MealName} could not be updated. Press any key to continue.");
            }

            Console.ReadLine();
            return;
        }
    }
}
