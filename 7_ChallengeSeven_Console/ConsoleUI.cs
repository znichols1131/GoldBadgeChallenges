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
            Party party1 = new Party("Zach's birthday", DateTime.Parse("09/07/2020"));
            Booth booth1 = new Booth("Hamburger booth");
            Booth booth2 = new Booth("Ice cream booth");
            party1.Booths.Add(booth1);
            party1.Booths.Add(booth2);

            _partyRepo.CreateParty(party1);
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
                        _consoleUI_Party.Menu_CreateParty();
                        break;
                    case "2":
                        _consoleUI_Party.Menu_ViewOrUpdate_All();
                        break;
                    case "3":
                        _consoleUI_Party.Menu_Delete();
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

        // Helper methods (if any)

    }
}
