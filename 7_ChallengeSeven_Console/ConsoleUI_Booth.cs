using _7_ChallengeSeven_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Console
{
    public class ConsoleUI_Booth : ConsoleUI_FormattingHelpers
    {
        private Party _party;

        // Mandatory constructor
        public ConsoleUI_Booth(Party party)
        {
            _party = party;
        }

        // Create
        public void Menu_CreateBooth()
        {
            Console.Clear();
            PrintTitle("Creating new booth:");

            Booth newBooth = AskUserForBooth();
            if (!(newBooth is null))
            {
                bool success = _party.AddBooth(newBooth);
                if (success)
                {
                    Console.WriteLine($"\nBooth {newBooth.Name} has been created. Press any key to continue.\n");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"\nBooth could not be created. Press any key to continue.\n");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"\nBooth could not be created. Press any key to continue.\n");
                Console.ReadLine();
                return;
            }

            // Go into update mode for that new booth
            GoBack();
            GoToNextPage("Viewing Specific Booth");
            Menu_ViewOrUpdate_Specific(_party.Booths.Count - 1);
            GoBack();
        }

        public Booth AskUserForBooth()
        {
            Console.Write("Step 1 of 1: ");
            string boothName = AskUser_StringInput("Enter the name of the booth:");
            if (boothName is null) { return null; }

            Booth newBooth = new Booth(boothName);
            return newBooth;
        }


        // Update existing
        public void Menu_ViewOrUpdate_All()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing booths:");

                PrintBoothsInList(_party.Booths);

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a booth number to view booth " +
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
                            int boothID = int.Parse(response.Trim());
                            GoToNextPage("Viewing Specific Booth");
                            Menu_ViewOrUpdate_Specific(boothID);
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

        private void Menu_ViewOrUpdate_Specific(int boothID)
        {            
            if(_party.Booths is null || _party.Booths.Count == 0)
            {
                Console.WriteLine($"\nWe're sorry, there are no booths to view. Press any key to continue.");
                Console.ReadLine();
                return;
            }
            
            Booth booth = _party.Booths[boothID];
            if (booth is null)
            {
                PrintErrorMessageForInput($"Booth number: {boothID}");
                return;
            }

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Viewing booth:");

                Console.WriteLine("{0,-15}{1,-20}", "Booth number:", boothID);
                Console.WriteLine("{0,-15}{1,-20}", "Name:", booth.Name);

                // Print any products
                Console.Write("{0,-15}", "Products:");
                if (booth.Products is null || booth.Products.Count == 0)
                {
                    Console.WriteLine("{0,-20}", "There are no products for this booth.");
                }
                else
                {
                    Console.WriteLine();
                    for (int i = 0; i < booth.Products.Count; i++)
                    {
                        Product product = booth.Products[i];
                        Console.WriteLine("{0,-12}{1, -3}{2,-20}", "", $"#{i}", product.Name);
                    }
                }

                Console.WriteLine("\n{0,-15}{1,-20:n0}", "Total tickets:", booth.TicketsExchanged());
                Console.WriteLine("{0,-15}{1,-20:c2}", "Total cost:", booth.TotalCost());

                Console.WriteLine("\n" + CONST_DASHES + "\n\nWhat would you like to do?\n" +
                    "1. Update name.\n" +
                    "2. Add a product.\n" +
                    "3. View existing products.\n" +
                    "4. Remove products.\n" +
                    "5. Return to previous menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Update name
                        string boothName = AskUser_StringInput("Enter the name of the booth:");
                        if (boothName is null) { break; }
                        booth.Name = boothName;
                        UpdateBooth(boothID, booth);
                        break;

                    case "2":
                        // Add product
                        GoToNextPage("Creating Product");
                        ConsoleUI_Product consoleUI_Product = new ConsoleUI_Product(booth);
                        consoleUI_Product.Menu_CreateProduct();
                        GoBack();
                        break;

                    case "3":
                        // See products
                        GoToNextPage("Viewing Existing Products");
                        ConsoleUI_Product consoleUI_Product2 = new ConsoleUI_Product(booth);
                        consoleUI_Product2.Menu_ViewOrUpdate_All();
                        GoBack();
                        break;

                    case "4":
                        // Remove products
                        if (booth.Products is null || booth.Products.Count == 0)
                        {
                            Console.WriteLine($"\nWe're sorry, there are no products to delete. Press any key to continue.");
                            Console.ReadLine();
                            break;
                        }

                        List<Product> productsToDelete = AskUser_ProductsForBooth(booth);
                        if (!(productsToDelete is null || productsToDelete.Count == 0))
                        {
                            bool success = true;
                            foreach (Product product in productsToDelete)
                            {
                                success = (success && booth.RemoveProduct(product));
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
                PrintTitle("Existing booths:");

                PrintBoothsInList(_party.Booths);

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter booth numbers to delete separated by commas " +
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
                            if (_party.Booths is null || _party.Booths.Count == 0)
                            {
                                Console.WriteLine($"\nWe're sorry, there are no booths to delete. Press any key to continue.");
                                Console.ReadLine();
                                break;
                            }

                            List<int> boothIDsToDelete = SplitStringIntoIDs(response);
                            if (!(boothIDsToDelete is null || boothIDsToDelete.Count == 0))
                            {
                                bool success = true;
                                
                                List<Booth> boothsToDelete = new List<Booth>();
                                foreach(int id in boothIDsToDelete)
                                {
                                    try
                                    {
                                        boothsToDelete.Add(_party.Booths[id]);
                                    }
                                    catch 
                                    {
                                        success = false;
                                    }
                                }

                                foreach (Booth booth in boothsToDelete)
                                {
                                    success = (success && _party.RemoveBooth(booth));
                                }

                                if (success)
                                {
                                    Console.WriteLine($"\nAll booths were successfully removed. Press any key to continue.");
                                }
                                else
                                {
                                    Console.WriteLine($"\nAt least one booth could not be removed. Press any key to continue.");
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
        private List<Product> AskUser_ProductsForBooth(Booth booth)
        {
            if (booth is null || booth.Products is null || booth.Products.Count == 0)
            {
                return null;
            }

            Console.WriteLine("\nEnter all product numbers separated by commas:");
            string productStr = Console.ReadLine();
            if (!ValidateStringResponse(productStr, true))
            {
                PrintErrorMessageForInput(productStr);
                return null;
            }
            List<int> productIDs = SplitStringIntoIDs(productStr);
            if (productIDs is null || productIDs.Count == 0)
            {
                return null;
            }

            List<Product> parsedProducts = new List<Product>();
            foreach (int id in productIDs)
            {
                Product newProduct = booth.Products[id];
                if (!(newProduct is null))
                {
                    parsedProducts.Add(newProduct);
                }
            }

            return parsedProducts;
        }

        private void PrintBoothsInList(List<Booth> listOfBooths)
        {
            if (listOfBooths is null || listOfBooths.Count == 0)
            {
                Console.WriteLine("There are no booths at this time.");
            }
            else
            {
                Console.WriteLine("{0,-5}{1,-25}{2,-15}{3,-10}{4,20}\n",
                        "#",
                        "Booth Name",
                        "# Products",
                        "Tickets",
                        "Total Cost");

                int i = 0;
                foreach (Booth booth in listOfBooths)
                {
                    // Make sure the purpose isn't too long
                    string formattedName = booth.Name;
                    if (formattedName.Length > 23)
                    {
                        formattedName = formattedName.Substring(0, 20) + "...";
                    }

                    Console.WriteLine("{0, -5}{1,-25}{2,-15}{3,-10}{4,20:c2}",
                        i,
                        formattedName,
                        booth.Products.Count,
                        booth.TicketsExchanged(),
                        booth.TotalCost());

                    i++;
                }
            }
        }

        private void UpdateBooth(int boothID, Booth newBooth)
        {
            if (newBooth is null || _party is null || _party.Booths is null || _party.Booths.Count == 0)
            {
                Console.WriteLine("\nBooth could not be updated. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            bool success = _party.UpdateBoothAtIndex(boothID, newBooth);
            if (success)
            {
                Console.WriteLine($"\nBooth is updated. Press any key to continue.");
            }
            else
            {
                Console.WriteLine($"\nBooth could not be updated. Press any key to continue.");
            }

            Console.ReadLine();
            return;
        }

    }
}
