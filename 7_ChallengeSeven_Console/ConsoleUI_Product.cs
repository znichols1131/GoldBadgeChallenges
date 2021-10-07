using _7_ChallengeSeven_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Console
{
    public class ConsoleUI_Product : ConsoleUI_FormattingHelpers
    {
        private Booth _booth;

        // Mandatory constructor
        public ConsoleUI_Product(Booth booth)
        {
            _booth = booth;
        }

        // Create
        public void Menu_CreateProduct()
        {
            Console.Clear();
            PrintTitle("Creating new product:");

            Product newProduct = AskUserForProduct();
            if (!(newProduct is null))
            {
                bool success = _booth.AddProduct(newProduct);
                if (success)
                {
                    Console.WriteLine($"\nProduct {newProduct.Name} has been created. Press any key to continue.\n");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"\nProduct could not be created. Press any key to continue.\n");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"\nProduct could not be created. Press any key to continue.\n");
                Console.ReadLine();
                return;
            }

            // Go into update mode for that new product
            GoBack();
            GoToNextPage("Viewing Specific Product");
            Menu_ViewOrUpdate_Specific(_booth.Products.Count - 1);
            GoBack();
        }

        public Product AskUserForProduct()
        {
            Console.Write("Step 1 of 1: ");
            string productName = AskUser_StringInput("Enter the name of the product:");
            if (productName is null) { return null; }

            Product newProduct = new Product(productName);
            return newProduct;
        }


        // Update existing
        public void Menu_ViewOrUpdate_All()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing products:");

                PrintProductsInList(_booth.Products);

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a product number to view product " +
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
                            int productID = int.Parse(response.Trim());
                            GoToNextPage("Viewing Specific Product");
                            Menu_ViewOrUpdate_Specific(productID);
                            GoBack();
                        }
                        catch
                        {
                            PrintErrorMessageForInput(response);
                        }
                        break;
                }
            }
        }

        private void Menu_ViewOrUpdate_Specific(int productID)
        {
            if (_booth.Products is null || _booth.Products.Count == 0)
            {
                Console.WriteLine($"\nWe're sorry, there are no products to view. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            Product product = _booth.Products[productID];
            if (product is null)
            {
                PrintErrorMessageForInput($"Product number: {productID}");
                return;
            }

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Viewing product:");

                Console.WriteLine("{0,-15}{1,-20}", "Product number:", productID);
                Console.WriteLine("{0,-15}{1,-20}", "Name:", product.Name);

                // Print any products
                Console.Write("{0,-15}", "Ingredients:");
                if (product.Ingredients is null || product.Ingredients.Count == 0)
                {
                    Console.WriteLine("{0,-20}", "There are no ingredients for this product.");
                }
                else
                {
                    Console.WriteLine();
                    for (int i = 0; i < product.Ingredients.Count; i++)
                    {
                        Ingredient ingredient = product.Ingredients[i];
                        Console.WriteLine("{0,-12}{1, -3}{2,-20}", 
                            "",
                            $"#{i}", 
                            String.Format("{0} ({1:c2})", ingredient.Name, ingredient.Cost));
                    }
                }

                Console.WriteLine("\n{0,-15}{1,-20:n0}", "Total sold:", product.TicketsExchanged());
                Console.WriteLine("{0,-15}{1,-20:c2}", "Total cost:", product.TotalCost());

                Console.WriteLine("\n" + CONST_DASHES + "\n\nWhat would you like to do?\n" +
                    "1. Update name.\n" +
                    "2. Add an ingredient.\n" +
                    "3. Update an ingredient.\n" +
                    "4. Remove ingredients.\n" +
                    "5. Return to previous menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Update name
                        string productName = AskUser_StringInput("Enter the name of the product:");
                        if (productName is null) { break; }
                        product.Name = productName;
                        UpdateProduct(productID, product);
                        break;

                    case "2":
                        // Add an ingredient
                        Ingredient newIngredient = AskUser_Ingredient();
                        if (!(newIngredient is null))
                        {
                            bool success = product.AddIngredient(newIngredient);
                            if (success)
                            {
                                Console.WriteLine($"\nIngredient was successfully added. Press any key to continue.");
                            }
                            else
                            {
                                Console.WriteLine($"\nIngredient could not be added. Press any key to continue.");
                            }
                            Console.ReadLine();
                        }
                        break;

                    case "3":
                        // Update an ingredient
                        Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter an ingredient number to update.");
                        string response2 = Console.ReadLine();
                        int ingredientID;

                        if(response2 != "")
                        {
                            try
                            {
                                ingredientID = int.Parse(response2.Trim());
                                Ingredient newIngredient2 = AskUser_Ingredient();
                                if (!(newIngredient2 is null))
                                {
                                    bool success = product.UpdateIngredientAtIndex(ingredientID, newIngredient2);
                                    if (success)
                                    {
                                        Console.WriteLine($"\nIngredient was successfully updated. Press any key to continue.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"\nIngredient could not be updated. Press any key to continue.");
                                    }
                                    Console.ReadLine();
                                }
                            }
                            catch
                            {
                                PrintErrorMessageForInput(response2);
                                break;
                            }
                        }

                        break;

                    case "4":
                        // Remove ingredients
                        if (product.Ingredients is null || product.Ingredients.Count == 0)
                        {
                            Console.WriteLine($"\nWe're sorry, there are no ingredients to delete. Press any key to continue.");
                            Console.ReadLine();
                            break;
                        }

                        List<Ingredient> ingredientsToDelete = AskUser_IngredientsForProduct(product);
                        if (!(ingredientsToDelete is null || ingredientsToDelete.Count == 0))
                        {
                            bool success = true;
                            foreach (Ingredient ingredient in ingredientsToDelete)
                            {
                                success = (success && product.RemoveIngredient(ingredient));
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

                    case "5":
                        // Return to previous menu
                        return;
                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }


        // Delete existing
        public void Menu_Delete()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing products:");

                PrintProductsInList(_booth.Products);

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter product numbers to delete separated by commas " +
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
                            if (_booth.Products is null || _booth.Products.Count == 0)
                            {
                                Console.WriteLine($"\nWe're sorry, there are no products to delete. Press any key to continue.");
                                Console.ReadLine();
                                break;
                            }

                            List<int> productIDsToDelete = SplitStringIntoIDs(response);
                            if (!(productIDsToDelete is null || productIDsToDelete.Count == 0))
                            {
                                bool success = true;

                                List<Product> productsToDelete = new List<Product>();
                                foreach (int id in productIDsToDelete)
                                {
                                    try
                                    {
                                        productsToDelete.Add(_booth.Products[id]);
                                    }
                                    catch
                                    {
                                        success = false;
                                    }
                                }

                                foreach (Product product in productsToDelete)
                                {
                                    success = (success && _booth.RemoveProduct(product));
                                }

                                if (success)
                                {
                                    Console.WriteLine($"\nAll products were successfully removed. Press any key to continue.");
                                }
                                else
                                {
                                    Console.WriteLine($"\nAt least one product could not be removed. Press any key to continue.");
                                }
                                Console.ReadLine();
                            }
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
        private Ingredient AskUser_Ingredient()
        {
            Console.Write("\nStep 1 of 2: ");
            string name = AskUser_StringInput("Enter the ingredient name:");
            if (name is null) { return null; }

            Console.Write("\nStep 2 of 2: ");
            var cost = AskUser_DoubleInput("Enter the cost of the ingredient (in dollars):");
            if (!cost.HasValue) { return null; }

            Ingredient newIngredient = new Ingredient(name, (double)cost);
            return newIngredient;
        }

        private List<Ingredient> AskUser_IngredientsForProduct(Product product)
        {
            if (product is null || product.Ingredients is null || product.Ingredients.Count == 0)
            {
                return null;
            }

            Console.WriteLine("\nEnter all ingredient numbers separated by commas:");
            string ingredientStr = Console.ReadLine();
            if (!ValidateStringResponse(ingredientStr, true))
            {
                PrintErrorMessageForInput(ingredientStr);
                return null;
            }
            List<int> ingredientIDs = SplitStringIntoIDs(ingredientStr);
            if (ingredientIDs is null || ingredientIDs.Count == 0)
            {
                return null;
            }

            List<Ingredient> parsedIngredients = new List<Ingredient>();
            foreach (int id in ingredientIDs)
            {
                Ingredient newIngredient = product.Ingredients[id];
                if (!(newIngredient is null))
                {
                    parsedIngredients.Add(newIngredient);
                }
            }

            return parsedIngredients;
        }

        private void PrintProductsInList(List<Product> listOfProducts)
        {
            if (listOfProducts is null || listOfProducts.Count == 0)
            {
                Console.WriteLine("There are no products at this time.");
            }
            else
            {
                Console.WriteLine("{0,-5}{1,-25}{2,-15}{3,-10}{4,20}\n",
                        "#",
                        "Product Name",
                        "# Ingredients",
                        "Tickets",
                        "Total Cost");

                int i = 0;
                foreach (Product product in listOfProducts)
                {
                    // Make sure the purpose isn't too long
                    string formattedName = product.Name;
                    if (formattedName.Length > 23)
                    {
                        formattedName = formattedName.Substring(0, 20) + "...";
                    }

                    Console.WriteLine("{0, -5}{1,-25}{2,-15}{3,-10}{4,20:c2}",
                        i,
                        formattedName,
                        product.Ingredients.Count,
                        product.TicketsExchanged(),
                        product.TotalCost());

                    i++;
                }
            }
        }

        private void UpdateProduct(int productID, Product newProduct)
        {
            if (newProduct is null || _booth is null || _booth.Products is null || _booth.Products.Count == 0)
            {
                Console.WriteLine("\nProduct could not be updated. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            bool success = _booth.UpdateProductAtIndex(productID, newProduct);
            if (success)
            {
                Console.WriteLine($"\nProduct is updated. Press any key to continue.");
            }
            else
            {
                Console.WriteLine($"\nProduct could not be updated. Press any key to continue.");
            }

            Console.ReadLine();
            return;
        }


    }
}
