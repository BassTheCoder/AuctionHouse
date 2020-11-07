using System;

namespace AuctionHouse.Models
{
    class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StartingPrice { get; set; }
        public int MinPrice { get; set; }
        public int SalePrice { get; set; }
        public DateTime? SaleDate { get; set; }
        public Client Owner { get; set; }
        public bool IsSold { get; set; }

        /*
         * public string Name { get; set; }
         * kompilator widzi jak:
         * 
         * private string Name;
         * public string Name
         * {
         *  get => _name;
         *  set => _name = value;
         * }
         */
    }
}
