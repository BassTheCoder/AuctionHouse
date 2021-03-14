using AuctionHouse.Helpers;
using AuctionHouse.Models;
using System;
using System.Text.RegularExpressions;

namespace AuctionHouse
{
    class Validator
    {
        public static int PickMenuOption(Database database, int input)
        {
            if (database.ClientsList.Count > 0 && database.ItemsList.Count > 0)
            {
                Console.WriteLine("\nPick an option [1-6]");
                input = Validator.GetInt();
                while (!Validator.ValidateMinMaxInt(input, 1, 6))
                {
                    Console.WriteLine("Pick an option [1-6]");
                    input = Validator.GetInt();
                }
                return input;
            }
            else
            {
                Console.WriteLine("\nWarning!");
                if (database.ClientsList.Count < 1)
                {
                    Console.WriteLine("List of clients is empty or missing. Option [1] is unavailable.");
                }
                if (database.ItemsList.Count < 1)
                {
                    Console.WriteLine("List of items is empty or missing. Option [1] is unavailable.");
                }
                Console.WriteLine("\nPick an option [2-6]");
                input = Validator.GetInt();
                while (!Validator.ValidateMinMaxInt(input, 2, 6))
                {
                    Console.WriteLine("Pick an option [2-6]");
                    input = Validator.GetInt();
                }
                return input;
            }
        }

        public static IntResponse PlaceBid()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        return new IntResponse { IsStopInvoked = true };
                    }
                    else
                    {
                        int number = int.Parse(input);
                        return new IntResponse { IsStopInvoked = false, Result = number };
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error. " + e.Message);
                }
            }
        }

        public static bool ValidateLettersString(string input)
        {
            string pattern = "^[a-zA-Z ]+$";
            Regex regex = new Regex(pattern);
            try
            {
                return regex.IsMatch(input);
            }
            catch
            {
                return false;
            }
        }

        public static StringResponse GetStringOrStop(string input, bool shouldContinue)
        {
            if (shouldContinue)
            {
                if (input.ToLower() == "stop")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nStop command has been invoked.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return new StringResponse { IsStopInvoked = true };
                }
                else
                {
                    return new StringResponse { IsStopInvoked = false, Result = input };
                }
            }
            else
            {
                return new StringResponse { IsStopInvoked = true };
            }
        }

        public static bool ValidateLettersAndNumbersString(string input)
        {
            string pattern = "^[a-zA-Z0-9 ]+$";
            Regex regex = new Regex(pattern);
            try
            {
                return regex.IsMatch(input);
            }
            catch
            {
                return false;
            }

        }

        public static bool ValidateZipcode(string input)
        {
            string pattern = "^[0-9]{5}$";
            Regex regex = new Regex(pattern);
            try
            {
                return regex.IsMatch(input);
            }
            catch
            {
                return false;
            }
        }

        public static int GetInt()
        {
            while (true)
            {
                try
                {
                    int number = int.Parse(Console.ReadLine());
                    return number;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error. " + e.Message);
                }
            }
        }

        public static IntResponse GetIntOrStop()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        return new IntResponse { IsStopInvoked = true };
                    }
                    else
                    {
                        int number = int.Parse(input);
                        return new IntResponse { IsStopInvoked = false, Result = number };
                    }                                        
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error. " + e.Message);
                }
            }
        }

        public static bool ValidateMinMaxInt(int input, int Min = 1, int Max = int.MaxValue)
        {
            if (input < Min || input > Max)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool YesNoValidator(string input)
        {
            while (true)
            {
                if (input.ToLower() == "y")
                    return true;
                else if (input.ToLower() == "n")
                    return false;
                else
                {
                    Console.WriteLine("Make a choice by typing \"Y\" or \"N\"");
                    input = Console.ReadLine();
                }
            }
        }
    }
}
