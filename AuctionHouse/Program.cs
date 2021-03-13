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
            AuctionService auctionService = new AuctionService();
            DatabaseManager.ReadClientsList(database);
            DatabaseManager.ReadItemsList(database);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Welcome to the Auction House! ===");
                Console.WriteLine();
                Console.WriteLine("[1] Start an auction");
                Console.WriteLine("[2] Add an item");
                Console.WriteLine("[3] Add a client");
                Console.WriteLine("[4] Show the list of items");
                Console.WriteLine("[5] Show the list of clients");
                Console.WriteLine("[6] Exit");

                int input = -1;

                input = Validator.PickMenuOption(database, input);

                switch (input)
                {
                    case 1:
                        Console.Clear();
                        auctionService.RunAuction(database);
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
                        Console.Clear();
                        DatabaseManager.DisplayItems(database);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 5:
                        Console.Clear();
                        DatabaseManager.DisplayClients(database);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    case 6:
                        Environment.Exit(0);
                        break;
                }
            }

        }
    }
}
