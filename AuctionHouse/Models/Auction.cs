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
    }
}
