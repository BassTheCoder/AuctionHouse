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
                            Console.WriteLine("\nAuction has ended on " + auction.EndDate + " and it lasted " + (auction.EndDate - auction.StartDate));
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
            bool isItemAuctionRunning = true;
            int clientId = 0;
            int currentBid = 0;
            int bidsInRow = 0;
            int highestBid = 0;

            while (isItemAuctionRunning)
            {
                Console.WriteLine("Starting an auction for item:\n\n");
                DatabaseManager.DisplayItem(item);
                Console.WriteLine();

                while (!Validator.ValidateMinMaxInt(clientId, 1, clients[clients.Count - 1].Id))
                {
                    Console.Write("Give ID of a person to place a bid (");
                    DisplayClientsIds(clients);
                    Console.WriteLine("):");
                    clientId = Validator.GetInt();
                }

                Client currentBidder = clients.FirstOrDefault(c => c.Id == clientId);

                if (currentBidder != null)
                {
                    Console.WriteLine("\n" + currentBidder.Name + " " + currentBidder.Surname + " is placing a bid. How much PLN?");
                    while (!Validator.ValidateMinMaxInt(currentBid, highestBid + 1) && isItemAuctionRunning)
                    {
                        Console.WriteLine("Bid must be higher than current highest bid - " + highestBid + " PLN.");

                        BidResult currentBidResult = Validator.PlaceBid();
                        if (currentBidResult.IsSuccess)
                        {
                            currentBid = currentBidResult.Result;
                        }
                        else
                        {
                            isItemAuctionRunning = false;
                        }    
                        
                    }
                    if (isItemAuctionRunning)
                    {
                        highestBid = currentBid;
                        Console.WriteLine("\n" + currentBidder.Name + " " + currentBidder.Surname + " succesfully placed a bid with " + currentBid + " PLN.");
                        if (highestBid < item.MinPrice)
                        {
                            Console.WriteLine("\nCurrent highest bid is " + highestBid + " PLN. Minimal price (" + item.MinPrice + " PLN) not reached yet!");
                        }
                        currentBid = 0;
                        clientId = 0;
                    }
                }
                else
                {
                    Console.WriteLine("\nThere is no eligible client with such id.\n");
                    clientId = 0;
                }
            }

            Console.WriteLine("\nDo you want to start an auction for the next item? Y/N\n");
            return Validator.YesNoValidator(Console.ReadLine());
        }

        private void DisplayClientsIds(List<Client> clientsList)
        {
            foreach (Client client in clientsList)
            {
                if (client == clientsList[clientsList.Count - 1])
                {
                    Console.Write(client.Id);
                }
                else
                {
                Console.Write(client.Id + ", ");
                }
            }
        }
    }
}
