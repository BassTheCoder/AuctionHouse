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

            if (auction.EligibleItems.Count > 0)
            {
                Console.WriteLine();
                auction.DisplayEligibleItems();
                Console.WriteLine();
                Console.WriteLine("Start an auction? Y/N\n");
                IsAuctionRunning = Validator.YesNoValidator(Console.ReadLine());

                if (IsAuctionRunning)
                {
                    Console.Clear();
                    Console.WriteLine();
                    auction.StartDate = DateTime.Now;
                    Console.WriteLine("Auction has started on " + auction.StartDate);
                    Console.WriteLine();
                    bool shouldContinue = true;

                    while (IsAuctionRunning && shouldContinue)
                    {
                        if (auction.EligibleItems.Count > 0 && IsAuctionRunning)
                        {
                            Item itemToAuction = auction.EligibleItems.FirstOrDefault();
                            shouldContinue = AuctionItem(itemToAuction, auction.EligibleClients, auction, database);
                        }
                        else
                        {
                            auction.EndDate = DateTime.Now;
                            Console.WriteLine("\nAuction has ended on " + auction.EndDate + " and it lasted " + (auction.EndDate - auction.StartDate));
                            Console.WriteLine("\nPress any key to continue");
                            Console.ReadKey();
                            shouldContinue = false;
                            IsAuctionRunning = false;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("\nNo eligible items left to auction.\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        private bool AuctionItem(Item item, List<Client> clients, Auction auction, Database database)
        {

            bool isItemAuctionRunning = true;
            bool isStopInvoked = false;
            int clientId = 0;
            int currentBid = 0;
            int bidsInRow = 1;
            int highestBid = 0;
            Client previousBidder = null;

            Console.WriteLine("Starting an auction for item:\n\n");
            DatabaseManager.DisplayItem(item);
            Console.WriteLine();

            while (isItemAuctionRunning)
            {
                while (!Validator.ValidateMinMaxInt(clientId, 1, clients.LastOrDefault().Id) && isItemAuctionRunning)
                {
                    Console.Write("Give ID of a person to place a bid (");
                    DisplayClientsIds(clients);
                    Console.WriteLine("):");
                    IntResponse response = Validator.GetIntOrStop();
                    if (response.IsStopInvoked)
                    {
                        isStopInvoked = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nStop command has been invoked. The auction has been aborted.\n");
                        Console.ResetColor();
                        isItemAuctionRunning = false;
                    }
                    else
                    {
                        clientId = response.Result;
                    }
                }

                Client currentBidder = clients.FirstOrDefault(c => c.Id == clientId);

                if (currentBidder != null)
                {
                    Console.WriteLine("\n" + currentBidder.Name + " " + currentBidder.Surname + " is placing a bid. How much PLN?");
                    while (!Validator.ValidateMinMaxInt(currentBid, highestBid + 1) && isItemAuctionRunning)
                    {
                        Console.WriteLine("Bid must be higher than current highest bid - " + highestBid + " PLN.");

                        IntResponse currentBidResult = Validator.PlaceBid();
                        if (currentBidResult.IsStopInvoked)
                        {
                            isStopInvoked = true;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nStop command has been invoked. The auction has been aborted.\n");
                            Console.ResetColor();
                            isItemAuctionRunning = false;
                        }
                        else
                        {
                            currentBid = currentBidResult.Result;
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
                        if (currentBidder == previousBidder)
                        {
                            bidsInRow++;
                        }
                        else
                        {
                            bidsInRow = 1;
                        }

                        if (bidsInRow >= 3 && currentBid > item.MinPrice)
                        {
                            SellItem(item, currentBidder, currentBid);
                            auction.EligibleItems.Remove(item);
                            DatabaseManager.SaveToFile(database);
                            Console.ReadKey();
                            isItemAuctionRunning = false;
                        }
                        previousBidder = currentBidder;
                        currentBid = 0;
                        clientId = 0;
                    }
                }
                else if ( currentBidder == null && !isStopInvoked)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThere is no eligible client with such id.\n");
                    Console.ResetColor();
                    clientId = 0;
                }
            }

            if (auction.EligibleItems.Count > 0)
            {
                Console.WriteLine("\nDo you want to proceed with the auction? Y/N\n");
                return Validator.YesNoValidator(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("\nNo eligible items left to auction.\nPress any key to continue...");
                Console.ReadKey();
                return false;
            }

        }

        private void SellItem(Item item, Client client, int price)
        {
            item.IsSold = true;
            item.SaleDate = DateTime.Now;
            item.SalePrice = price;
            item.Owner = client;
            Console.WriteLine("\nThe " + item.Name + " has been sold to " + client.Name + " " + client.Surname + " for " + price + " PLN !!!\n");
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
