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
            Menu_ViewOrUpdate_Specific(_booth.Products.Count - 1);

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

                Console.WriteLine("\n" + _dashes + "\n\nEnter a product number to view product " +
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
                            Menu_ViewOrUpdate_Specific(productID);
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
                        Console.WriteLine("{0,-12}{1, -3}{2,-20}", "", i, ingredient.Name);
                    }
                }

                Console.WriteLine("\n" + _dashes + "\n\nWhat would you like to do?\n" +
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

                        break;

                    case "3":

                        break;

                    case "4":

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

                Console.WriteLine("\n" + _dashes + "\n\nEnter product numbers to delete separated by commas " +
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
        private List<Booth> AskUser_BoothsForParty(Party party)
        {
            if (party is null || party.Booths is null || party.Booths.Count == 0)
            {
                return null;
            }

            Console.WriteLine("Enter all booth numbers separated by commas:");
            string boothsStr = Console.ReadLine();
            if (!ValidateStringResponse(boothsStr, true))
            {
                PrintErrorMessageForInput(boothsStr);
                return null;
            }
            List<int> boothIDs = SplitStringIntoIDs(boothsStr);
            if (boothIDs is null || boothIDs.Count == 0)
            {
                return null;
            }

            List<Booth> parsedBooths = new List<Booth>();
            foreach (int id in boothIDs)
            {
                Booth newBooth = party.Booths[id];
                if (!(newBooth is null))
                {
                    parsedBooths.Add(newBooth);
                }
            }

            return parsedBooths;
        }

        private void PrintProductsInList(List<Product> listOfProducts)
        {
            if (listOfProducts is null || listOfProducts.Count == 0)
            {
                Console.WriteLine("There are no products at this time.");
            }
            else
            {
                Console.WriteLine("{0,-5}{1,-25}{2,-15}{3,-10}{4,-20}\n",
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

                    Console.WriteLine("{0, -5}{1,-25}{2,-15}{3,-10}{4,-20}",
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
