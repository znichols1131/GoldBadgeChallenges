using _7_ChallengeSeven_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Console
{
    public class ConsoleUI : ConsoleUI_FormattingHelpers
    {
        private PartyRepository _partyRepo = new PartyRepository();
        private ConsoleUI_Party _consoleUI_Party;

        public void Run()
        {
            _consoleUI_Party = new ConsoleUI_Party(_partyRepo);

            Populate();
            Run_MainMenu();
        }

        // Dummy data
        private void Populate()
        {
            // Define ingredients
            Ingredient bun = new Ingredient("Bun", 0.25);
            Ingredient burgerPatty = new Ingredient("Burger patty", 2.50);
            Ingredient hotDogWeiner = new Ingredient("Hot dog weiner", 0.75);
            Ingredient veggiePatty = new Ingredient("Veggie patty", 1.50);
            Ingredient cheese = new Ingredient("Cheese", 0.15);
            Ingredient condiments = new Ingredient("Condiments", 0.10);
            Ingredient utensils = new Ingredient("Utensils", 0.20);
            Ingredient popcornKernels = new Ingredient("Popcorn kernels", 0.35);
            Ingredient iceCream = new Ingredient("Ice cream", 0.75);
            Ingredient iceCreamToppings = new Ingredient("Ice cream toppings", 0.50);

            // Define products
            Product hamburger = new Product("Hamburger");
            hamburger.Ingredients.Add(bun.Clone());
            hamburger.Ingredients.Add(burgerPatty.Clone());
            hamburger.Ingredients.Add(condiments.Clone());
            hamburger.Ingredients.Add(utensils.Clone());

            //Product cheeseburger = new Product("Cheeseburger");
            //cheeseburger.Ingredients.Add(bun.Clone());
            //cheeseburger.Ingredients.Add(burgerPatty.Clone());
            //cheeseburger.Ingredients.Add(cheese.Clone());
            //cheeseburger.Ingredients.Add(condiments.Clone());
            //cheeseburger.Ingredients.Add(utensils.Clone());

            Product veggieBurger = new Product("Veggie burger");
            veggieBurger.Ingredients.Add(bun.Clone());
            veggieBurger.Ingredients.Add(veggiePatty.Clone());
            veggieBurger.Ingredients.Add(condiments.Clone());
            veggieBurger.Ingredients.Add(utensils.Clone());

            Product hotDog = new Product("Hot dog");
            hotDog.Ingredients.Add(bun.Clone());
            hotDog.Ingredients.Add(hotDogWeiner.Clone());
            hotDog.Ingredients.Add(condiments.Clone());
            hotDog.Ingredients.Add(utensils.Clone());

            Product popcorn = new Product("Bag of popcorn");
            popcorn.Ingredients.Add(popcornKernels.Clone());
            popcorn.Ingredients.Add(utensils.Clone());

            Product iceCreamCone = new Product("Ice cream cone");
            iceCreamCone.Ingredients.Add(iceCream.Clone());
            iceCreamCone.Ingredients.Add(iceCreamToppings.Clone());
            iceCreamCone.Ingredients.Add(utensils.Clone());

            // Define booths
            Booth hamburgerBooth = new Booth("Hamburger booth");
            hamburgerBooth.AddProduct(hamburger.Clone());
            //hamburgerBooth.AddProduct(cheeseburger.Clone());
            hamburgerBooth.AddProduct(veggieBurger.Clone());
            hamburgerBooth.AddProduct(hotDog.Clone());

            Booth treatsBooth = new Booth("Treats booth");
            treatsBooth.AddProduct(popcorn.Clone());
            treatsBooth.AddProduct(iceCreamCone.Clone());

            // Define parties
            Party zachParty = new Party("Zach's birthday", DateTime.Parse("09/07/2020"));
            zachParty.Booths.Add(hamburgerBooth.Clone());
            zachParty.Booths.Add(treatsBooth.Clone());
            _partyRepo.CreateParty(zachParty);

            Party bruceParty = new Party("Bruce's retirement", DateTime.Parse("01/14/2021"));
            bruceParty.Booths.Add(hamburgerBooth.Clone());
            bruceParty.Booths.Add(treatsBooth.Clone()); 
            _partyRepo.CreateParty(bruceParty);

            Party beeslyParty = new Party("Beesly's adoption", DateTime.Parse("02/07/2021"));
            beeslyParty.Booths.Add(hamburgerBooth.Clone().Clone());
            beeslyParty.Booths.Add(treatsBooth.Clone().Clone()); 
            _partyRepo.CreateParty(beeslyParty);

            // Randomize the number of products sold
            _partyRepo.RandomizeAllParties(500);
        }

        public void RandomizeParties()
        {
            _partyRepo.RandomizeAllParties(500);
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
                    "4. Quit.\n\n" +
                    "5. Randomize parties (for testing only).\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        GoToNextPage("Create Party");
                        _consoleUI_Party.Menu_CreateParty();
                        GoBack();
                        break;
                    case "2":
                        GoToNextPage("View Existing Parties");
                        _consoleUI_Party.Menu_ViewOrUpdate_All();
                        GoBack();
                        break;
                    case "3":
                        GoToNextPage("Delete Party");
                        _consoleUI_Party.Menu_Delete();
                        GoBack();
                        break;
                    case "4":
                        // Quit
                        Environment.Exit(0);
                        return;
                    case "5":
                        // Randomize parties for testing purposes
                        RandomizeParties();
                        Console.WriteLine("\nParties successfully randomized. Press any key to continue.");
                        Console.ReadLine();
                        break;
                    default:
                        PrintErrorMessageForInput(response);
                        break;
                }
            }
        }

        // Helper methods (if any)

    }
}
