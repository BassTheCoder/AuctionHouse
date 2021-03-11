using AuctionHouse.Helpers;
using System;
using System.Collections.Generic;

namespace AuctionHouse.Models
{
    class Auction
    {
        public List<Item> EligibleItems { get; set; }
        public List<Client> EligibleClients { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Auction(Database database)
        {
            EligibleClients = new List<Client>();
            EligibleItems = new List<Item>();
            LoadDataForAuction(database);
        }

        public bool LoadDataForAuction(Database database)
        {
            Console.WriteLine("Loading list of eligible clients...");
            try
            {
                foreach (Client client in database.ClientsList)
                {
                    if (client.IsAdult)
                        this.EligibleClients.Add(client);
                }
                Console.WriteLine("Loaded list of eligible clients with " + EligibleClients.Count + " clients.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load list of eligible clients!");
                return false;
            }

            Console.WriteLine("Loading list of eligible items...");
            try
            {
                foreach (Item item in database.ItemsList)
                {
                    if (!item.IsSold)
                        this.EligibleItems.Add(item);
                }
                Console.WriteLine("Loaded list of eligible items with " + EligibleItems.Count + " items.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load list of eligible items!");
                return false;
            }

            return true;
        }

        public void DisplayEligibleItems()
        {
            Console.WriteLine("Items eligible to auction:\n");
            foreach (Item item in EligibleItems)
            {
                DatabaseManager.DisplayItem(item);
            }
        }
    }
}
