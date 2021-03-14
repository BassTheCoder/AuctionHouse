using AuctionHouse.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionHouse.Models
{
    class Database
    {
        public List<Item> ItemsList { get; set; }
        public List<Client> ClientsList { get; set; }
        public void AddClient()
        {
            Console.Clear();
            Client client = new Client() { Address = new Address() };
            int birthYear = 0;
            bool isLeapYear = false;
            int birthMonth = 0;
            int daysInMonth = 0;
            int birthDay = 0;
            bool shouldContinue = true;

            while (shouldContinue)
            {
                var lastClient = ClientsList.LastOrDefault();
                client.Id = lastClient != null ? lastClient.Id + 1 : 1;
                while (!Validator.ValidateLettersString(client.Name) && shouldContinue)
                {
                    Console.WriteLine("Client's name (letters only): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        client.Name = input;
                    }
                }
                while (!Validator.ValidateLettersString(client.Surname) && shouldContinue)
                {
                    Console.WriteLine("Client's surname (letters only): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        client.Surname = input;
                    }
                }
                while (!Validator.ValidateLettersString(client.Address.City) && shouldContinue)
                {
                    Console.WriteLine("Client's adress - City (letters only): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        client.Address.City = input;
                    }
                }
                while (!Validator.ValidateLettersString(client.Address.Street) && shouldContinue)
                {
                    Console.WriteLine("Client's adress - Street (letters only): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        client.Address.Street = input;
                    }
                }
                while (!Validator.ValidateLettersAndNumbersString(client.Address.HouseNumber) && shouldContinue)
                {
                    Console.WriteLine("Client's adress - House number (letters and numbers only): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        client.Address.HouseNumber = input;
                    }
                }
                while (!Validator.ValidateZipcode(client.Address.ZipCode) && shouldContinue)
                {
                    Console.WriteLine("Client's adress - Zip Code (5 digits): ");
                    string input = Console.ReadLine();
                    if (input.ToLower() == "stop")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        client.Address.ZipCode = input;
                    }
                }
                while (!Validator.ValidateMinMaxInt(birthYear, 1920, DateTime.Now.Year) && shouldContinue)
                {
                    Console.WriteLine("Client's birth date - Year (1920+):");
                    IntResponse response = Validator.GetIntOrStop();
                    if (response.IsStopInvoked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        birthYear = response.Result;
                    }
                }

                isLeapYear = (birthYear % 4 == 0) && (birthYear % 100 != 0) || (birthYear % 400 == 0);

                while (!Validator.ValidateMinMaxInt(birthMonth, 1, 12) && shouldContinue)
                {
                    Console.WriteLine("Client's birth date - Month (1-12):");
                    IntResponse response = Validator.GetIntOrStop();
                    if (response.IsStopInvoked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        birthMonth = response.Result;
                    }
                }

                if (birthMonth == 2)
                {
                    if (isLeapYear)
                    {
                        daysInMonth = 29;
                    }
                    else
                    {
                        daysInMonth = 28;
                    }
                }
                else if (birthMonth == 4 || birthMonth == 6 || birthMonth == 9 || birthMonth == 11)
                {
                    daysInMonth = 30;
                }
                else
                {
                    daysInMonth = 31;
                }

                while (!Validator.ValidateMinMaxInt(birthDay, 1, daysInMonth) && shouldContinue)
                {
                    Console.WriteLine("Client's birth date - Day (1-" + daysInMonth + "):");
                    IntResponse response = Validator.GetIntOrStop();
                    if (response.IsStopInvoked)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Stop command has been invoked. The auction has been aborted.");
                        Console.ResetColor();
                        shouldContinue = false;
                    }
                    else
                    {
                        birthDay = response.Result;
                    }
                }

                if (shouldContinue)
                {
                    client.BirthDate = new DateTime(birthYear, birthMonth, birthDay);
                    ClientsList.Add(client);
                    Console.WriteLine("\nSuccessfully added a client to the clients base.\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                    shouldContinue = false;
                }
                else
                {
                    Console.WriteLine("\nAdding a new client cancelled by a stop command.\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        public void AddItem()
        {
            bool shouldContinue = true;
            Item item = new Item();
            StringResponse stringResponse = new StringResponse();
            IntResponse intResponse = new IntResponse();

            var lastItem = ItemsList.LastOrDefault();
            item.Id = lastItem != null ? lastItem.Id + 1 : 1;
            Console.Clear();

            if (shouldContinue)
            {
                Console.WriteLine("Item's name: ");
                stringResponse = Validator.GetStringOrStop(Console.ReadLine(), shouldContinue);
                if (stringResponse.IsStopInvoked)
                {
                    shouldContinue = false;
                }
                else
                {
                    item.Name = stringResponse.Result;
                }
            }
            if (shouldContinue)
            {
                Console.WriteLine("\nShort description: ");
                stringResponse = Validator.GetStringOrStop(Console.ReadLine(), shouldContinue);
                if (stringResponse.IsStopInvoked)
                {
                    shouldContinue = false;
                }
                else
                {
                    item.Description = stringResponse.Result;
                }
            }
            while (!Validator.ValidateMinMaxInt(item.StartingPrice) && shouldContinue)
            {
                Console.WriteLine("\nStarting price [pln] (minimum 1): ");
                intResponse = Validator.GetIntOrStop();
                if (intResponse.IsStopInvoked)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nStop command has been invoked.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    shouldContinue = false;
                }
                else
                {
                    item.StartingPrice = intResponse.Result;
                }
            }
            while (!Validator.ValidateMinMaxInt(item.MinPrice) && shouldContinue)
            {
                Console.WriteLine("\nMinimum price [pln] (minimum 1): ");
                intResponse = Validator.GetIntOrStop();
                if (intResponse.IsStopInvoked)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nStop command has been invoked.");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    shouldContinue = false;
                }
                else
                {
                    item.MinPrice = intResponse.Result;
                }
            }
            if (shouldContinue)
            {
                item.SalePrice = 0;
                ItemsList.Add(item);
                Console.Clear();
            }
        }
    }
}
