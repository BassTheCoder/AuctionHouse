using AuctionHouse.Helpers;
using AuctionHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AuctionHouse
{
    class RunAuction
    {
        public static void Run(Database database)
        {
            Auction auction = new Auction();
            foreach (Client eligibleClient in database.ClientsList)
            {
                if (eligibleClient.IsAdult)
                    auction.EligibleClients.Add(eligibleClient);
            }
            Console.ReadKey();
            
            DatabaseManager.DisplayItems(database);
            Console.WriteLine();
            int auctionItemId = Validator.GetInt();
            while (!Validator.ValidateMinMaxInt(auctionItemId, 1, database.ItemsList.Count))
            {
                Console.WriteLine("Choose an item for the auction (id): ");
                auctionItemId = Validator.GetInt();
            }
        }
    }
}
