using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_ChallengeSeven_Console
{
    public abstract class ConsoleUI_FormattingHelpers
    {
        public string CONST_DASHES = "------------------------------";
        public string CONST_DATE_FORMAT = "MMM dd, yyyy";
        private static List<string> _navigationPages = new List<string>();   // I don't want a new instance with every ConsoleUI_ class


        // Helper methods (if any)
        public void PrintLogo()
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

        public void PrintTitle(string title)
        {
            string navStr = GetNavigationString();
            if(!(navStr is null || navStr == "Home"))
            {
                Console.WriteLine(GetNavigationString() + "\n\n");
            }
            Console.WriteLine(title + "\n\n" + CONST_DASHES + "\n");
        }

        public void PrintErrorMessageForInput(string input)
        {
            Console.WriteLine($"\nWe're sorry, '{input}' is not a valid input. Please try again.");
            Console.ReadLine();
        }

        public bool InterpretYesNoInput(string input)
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

        public bool ValidateStringResponse(string response, bool required)
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

        public bool ValidateDateResponse(string response, bool required)
        {
            if (ValidateStringResponse(response, required))
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

        public List<int> SplitStringIntoIDs(string rawInput)
        {
            if (rawInput is null || rawInput == "")
            {
                return null;
            }

            List<string> listOfInputs = rawInput.Split(',').ToList();

            if (listOfInputs is null || listOfInputs.Count == 0)
            {
                return null;
            }

            List<int> listOfIDs = new List<int>();
            foreach (string input in listOfInputs)
            {
                try
                {
                    listOfIDs.Add(int.Parse(input));
                }
                catch { }
            }

            return listOfIDs;

        }

        public string AskUser_StringInput(string prompt)
        {
            if (prompt is null || prompt == "")
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

        public double? AskUser_DoubleInput(string prompt)
        {
            if (prompt is null || prompt == "")
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

            try
            {
                return double.Parse(response.Trim('$'));
            }
            catch
            {
                return null;
            }

            return null;
        }

        public DateTime? AskUser_DateInput(string prompt)
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


        // Navigation bar methods
        public void GoToNextPage(string newPageName)
        {
            if(newPageName is null)
            {
                return;
            }

            _navigationPages.Add(newPageName);
            return;
        }

        public void GoBack()
        {
            if (_navigationPages is null || _navigationPages.Count == 0)
            {
                return;
            }

            // Remove latest page
            _navigationPages.RemoveAt(_navigationPages.Count - 1);
            return;
        }

        public string GetNavigationString()
        {
            if (_navigationPages is null)
            {
                return null;
            }
            else if(_navigationPages.Count == 0)
            {
                _navigationPages.Add("Home");
                return _navigationPages[0];
            }

            string formattedOutput = _navigationPages[_navigationPages.Count - 1];
            
            if(_navigationPages.Count == 1)
            {
                return formattedOutput;
            }

            int maxLength = 75;
            for(int i = _navigationPages.Count-2; i>=0; i--)
            {
                try
                {
                    if(formattedOutput.Length + 3 + _navigationPages[i].Length <= maxLength)
                    {
                        formattedOutput = $"{_navigationPages[i]} > {formattedOutput}";
                    }else
                    {
                        formattedOutput = $"... > {formattedOutput}";
                        return formattedOutput;
                    }
                }
                catch
                {
                    return formattedOutput;
                }
            }

            if(formattedOutput == "")
            {
                return null;
            }

            return formattedOutput;
        }
    }
}
