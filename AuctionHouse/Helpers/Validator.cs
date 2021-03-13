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
                    Console.WriteLine("List of clients is empty. Option [1] is impossible.");
                }
                if (database.ItemsList.Count < 1)
                {
                    Console.WriteLine("List of items is empty. Option [1] is impossible.");
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

        public static BidResult PlaceBid()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        return new BidResult { IsSuccess = false };
                    }
                    else
                    {
                        int number = int.Parse(input);
                        return new BidResult { IsSuccess = true, Result = number };
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
