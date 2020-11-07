using AuctionHouse.Helpers;
using AuctionHouse.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AuctionHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            Database database = new Database();
            DatabaseManager.ReadClientsList(database);
            DatabaseManager.ReadItemsList(database);

            while (true)
            {
                Console.WriteLine("=== Welcome to the Auction House! ===");
                Console.WriteLine();
                Console.WriteLine("[1] Start an auction");
                Console.WriteLine("[2] Add an item");
                Console.WriteLine("[3] Add a client");
                Console.WriteLine("[4] Show the list of items");
                Console.WriteLine("[5] Show the list of clients");
                Console.WriteLine("[0] Exit");

                int input = Validator.GetInt();
                while (!Validator.ValidateMinMaxInt(input, 0, 5))
                {
                    Console.WriteLine("Pick an option [0-5]");
                    input = Validator.GetInt();
                }

                switch (input)
                {
                    case 1:

                        RunAuction.Run(database);
                        break;

                    case 2:
                        database.AddItem();
                        DatabaseManager.SaveToFile(database);
                        break;

                    case 3:
                        database.AddClient();
                        DatabaseManager.SaveToFile(database);
                        break;

                    case 4:
                        DatabaseManager.DisplayItems(database);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 5:
                        DatabaseManager.DisplayClients(database);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 0:
                        Environment.Exit(0);
                        break;
                }
            }

        }
    }
}
