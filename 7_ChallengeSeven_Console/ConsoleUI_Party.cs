using _7_ChallengeSeven_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Console
{
    public class ConsoleUI_Party : ConsoleUI_FormattingHelpers
    {
        private PartyRepository _partyRepo;

        // Mandatory constructor
        public ConsoleUI_Party(PartyRepository partyRepo)
        {
            _partyRepo = partyRepo;
        }
        
        // Create party
        public void Menu_CreateParty()
        {
            Console.Clear();
            PrintTitle("Creating new barbecue party:");

            Party newParty = AskUserForParty();
            if (!(newParty is null))
            {
                bool success = _partyRepo.CreateParty(newParty);
                if (success)
                {
                    Console.WriteLine($"\nParty {newParty.Purpose} on {newParty.Date.ToString(CONST_DATE_FORMAT)} has been created. Press any key to continue.\n");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"\nParty could not be created. Press any key to continue.\n");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.WriteLine($"\nParty could not be created. Press any key to continue.\n");
                Console.ReadLine();
                return;
            }

            // Go into update mode for that new party
            GoBack();
            GoToNextPage("Viewing Specific Party");
            Menu_ViewOrUpdate_Specific(newParty.PartyID);
            GoBack();
        }

        public Party AskUserForParty()
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


        // Update existing party
        public void Menu_ViewOrUpdate_All()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing barbecue parties:");

                PrintPartiesInList(_partyRepo.GetAllParties());

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter a party number to view barbecue party " +
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
                            GoToNextPage("Viewing Specific Party");
                            Menu_ViewOrUpdate_Specific(partyID);
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

        private void Menu_ViewOrUpdate_Specific(int partyID)
        {
            Party party = _partyRepo.GetPartyForID(partyID);
            if (party is null)
            {
                PrintErrorMessageForInput($"Party ID: {partyID}");
                return;
            }

            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Viewing barbecue party:");

                Console.WriteLine("{0,-15}{1,-20}", "Party ID:", party.PartyID);
                Console.WriteLine("{0,-15}{1,-20}", "Date:", party.Date.ToString(CONST_DATE_FORMAT));
                Console.WriteLine("{0,-15}{1,-20}", "Purpose:", party.Purpose);

                // Print any booths
                Console.Write("{0,-15}", "Booths:");
                if (party.Booths is null || party.Booths.Count == 0)
                {
                    Console.WriteLine("{0,-20}", "There are no booths for this party.");
                }
                else
                {
                    Console.WriteLine();
                    for (int i = 0; i < party.Booths.Count; i++)
                    {
                        Booth booth = party.Booths[i];
                        Console.WriteLine("{0,-12}{1, -3}{2,-20}", "", $"#{i}", booth.Name);
                    }
                }

                Console.WriteLine("\n{0,-15}{1,-20:n0}", "Total tickets:", party.TicketsExchanged());
                Console.WriteLine("{0,-15}{1,-20:c2}", "Total cost:", party.TotalCost());

                Console.WriteLine("\n" + CONST_DASHES + "\n\nWhat would you like to do?\n" +
                    "1. Update date.\n" +
                    "2. Update purpose.\n" +
                    "3. Add a booth.\n" +
                    "4. View existing booths.\n" +
                    "5. Remove existing booths.\n" +
                    "6. Return to previous menu.\n");
                string response = Console.ReadLine();

                switch (response)
                {
                    case "1":
                        // Update date
                        var date = AskUser_DateInput("Enter the party date (MM/DD/YYYY):");     // How to check if it's null since DateTime isn't nullable
                        if (!date.HasValue) { break; }
                        party.Date = (DateTime)date;
                        UpdateParty(partyID, party);
                        break;

                    case "2":
                        // Update description
                        string purpose = AskUser_StringInput("Enter the purpose for the barbecue party:");
                        if (!ValidateStringResponse(purpose, true)) { break; }
                        party.Purpose = purpose;
                        UpdateParty(partyID, party);
                        break;

                    case "3":
                        // Add booth
                        GoToNextPage("Creating Booth");
                        ConsoleUI_Booth consoleUI_Booth = new ConsoleUI_Booth(party);
                        consoleUI_Booth.Menu_CreateBooth();
                        GoBack();
                        break;

                    case "4":
                        // Update booth
                        GoToNextPage("Viewing Existing Booths");
                        ConsoleUI_Booth consoleUI_Booth2 = new ConsoleUI_Booth(party);
                        consoleUI_Booth2.Menu_ViewOrUpdate_All();
                        GoBack();
                        break;

                    case "5":
                        // Remove booths
                        if (party.Booths is null || party.Booths.Count == 0)
                        {
                            Console.WriteLine($"\nWe're sorry, there are no booths to delete. Press any key to continue.");
                            Console.ReadLine();
                            break;
                        }

                        List<Booth> boothsToDelete = AskUser_BoothsForParty(party);
                        if (!(boothsToDelete is null || boothsToDelete.Count == 0))
                        {
                            bool success = true;
                            foreach (Booth booth in boothsToDelete)
                            {
                                success = (success && party.RemoveBooth(booth));
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


        // Delete existing party
        public void Menu_Delete()
        {
            bool keepLooping = true;
            while (keepLooping)
            {
                Console.Clear();
                PrintTitle("Existing barbecue parties:");

                PrintPartiesInList(_partyRepo.GetAllParties());

                Console.WriteLine("\n" + CONST_DASHES + "\n\nEnter all party numbers to delete separated by commas " +
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
                            if (_partyRepo.GetAllParties() is null || _partyRepo.GetAllParties().Count == 0)
                            {
                                Console.WriteLine($"\nWe're sorry, there are no barbecue parties to delete. Press any key to continue.");
                                Console.ReadLine();
                                break;
                            }

                            List<int> partiesIDsToDelete = SplitStringIntoIDs(response);
                            if (!(partiesIDsToDelete is null || partiesIDsToDelete.Count == 0))
                            {
                                bool success2 = true;
                                foreach (int id in partiesIDsToDelete)
                                {
                                    success2 = (success2 && _partyRepo.DeletePartyForID(id));
                                }

                                if (success2)
                                {
                                    Console.WriteLine($"\nAll barbecue parties were successfully removed. Press any key to continue.");
                                }
                                else
                                {
                                    Console.WriteLine($"\nAt least one barbecue party could not be removed. Press any key to continue.");
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
        private void UpdateParty(int partyID, Party newParty)
        {
            if (newParty is null)
            {
                Console.WriteLine("\nParty could not be updated. Press any key to continue.");
                Console.ReadLine();
                return;
            }

            bool success = _partyRepo.UpdatePartyForID(partyID, newParty);
            if (success)
            {
                Console.WriteLine($"\nParty is updated. Press any key to continue.");
            }
            else
            {
                Console.WriteLine($"\nParty could not be updated. Press any key to continue.");
            }

            Console.ReadLine();
            return;
        }

        private List<Booth> AskUser_BoothsForParty(Party party)
        {
            if (party is null || party.Booths is null || party.Booths.Count == 0)
            {
                return null;
            }

            Console.WriteLine("\nEnter all booth numbers separated by commas:");
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

        private void PrintPartiesInList(List<Party> listOfParties)
        {
            if (listOfParties is null || listOfParties.Count == 0)
            {
                Console.WriteLine("There are no barbecue parties at this time.");
            }
            else
            {
                Console.WriteLine("{0,-10}{1,-15}{2,-25}{3,-10}{4,20}\n",
                        "Party #",
                        "Date",
                        "Purpose",
                        "Tickets",
                        "Total Cost");

                foreach (Party party in listOfParties)
                {
                    // Make sure the purpose isn't too long
                    string formattedPurpose = party.Purpose;
                    if (formattedPurpose.Length > 23)
                    {
                        formattedPurpose = formattedPurpose.Substring(0, 20) + "...";
                    }

                    Console.WriteLine("{0,-10}{1,-15}{2,-25}{3,-10:N0}{4,20:c2}",
                        party.PartyID,
                        party.Date.ToString(CONST_DATE_FORMAT),
                        formattedPurpose,
                        party.TicketsExchanged(),
                        party.TotalCost());
                }
            }
        }

    }
}
