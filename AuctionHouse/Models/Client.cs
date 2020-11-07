using System;

namespace AuctionHouse.Models
{
    class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Address Address { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdult { get => DateTime.Today.Year - BirthDate.Year > 18 ? true : false ; }
    }
}
