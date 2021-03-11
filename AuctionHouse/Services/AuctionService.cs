using AuctionHouse.Helpers;
using AuctionHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuctionHouse
{
    class AuctionService
    {
        public bool IsAuctionRunning { get; set; } = true;

        public void RunAuction(Database database)
        {
            Auction auction = new Auction(database);

            Console.WriteLine();

            auction.DisplayEligibleItems();
            Console.WriteLine();
            Console.WriteLine("Start an auction? Y/N\n");
            IsAuctionRunning = Validator.YesNoValidator(Console.ReadLine());

            if (IsAuctionRunning)
            {
                Console.WriteLine();
                auction.StartDate = DateTime.Now;
                Console.WriteLine("Auction has started on " + auction.StartDate);
                Console.WriteLine();

                foreach (Item item in auction.EligibleItems)
                {
                    if (IsAuctionRunning)
                    {
                        bool shouldContinue = AuctionItem(item, auction.EligibleClients);
                        if (!shouldContinue)
                        {
                            auction.EndDate = DateTime.Now;
                            Console.WriteLine("\nAuction has ended on " + auction.EndDate + "and it lasted " + (auction.EndDate - auction.StartDate));
                            Console.WriteLine("\nPress any key to continue");
                            Console.ReadKey();
                            IsAuctionRunning = false;
                        }
                    }
                }
            }
        }

        private bool AuctionItem(Item item, List<Client> clients)
        {
            Console.WriteLine("Starting an auction for item:\n\n");
            DatabaseManager.DisplayItem(item);

            Console.WriteLine("Do you want to start an auction for the next item? Y/N\n");
            return Validator.YesNoValidator(Console.ReadLine());
        }
    }
}
