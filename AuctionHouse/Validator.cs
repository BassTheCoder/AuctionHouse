using System;
using System.Text.RegularExpressions;

namespace AuctionHouse
{
    class Validator
    {
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

    }
}
