using System;
using System.Collections.Generic;
using System.Linq;

namespace AuctionHouse.Models
{
    class Database
    {
        public List<Item> ItemsList { get; set; }
        public List<Client> ClientsList { get; set; }
        public Client AddClient()
        {
            Console.Clear();
            Client client = new Client();
            client.Address = new Address();
            int birthDay = 0;
            int birthMonth = 0;
            int birthYear = 0;

            var lastClient = ClientsList.LastOrDefault();
            client.Id = lastClient != null ? lastClient.Id + 1 : 1;
            while (!Validator.ValidateLettersString(client.Name))
            {
                Console.WriteLine("Client's name (letters only): ");
                client.Name = Console.ReadLine();
            }
            while (!Validator.ValidateLettersString(client.Surname))
            {
                Console.WriteLine("Client's surname (letters only): ");
                client.Surname = Console.ReadLine();
            }
            while (!Validator.ValidateLettersString(client.Address.City))
            {
                Console.WriteLine("Client's adress - City (letters only): ");
                client.Address.City = Console.ReadLine();
            }
            while (!Validator.ValidateLettersString(client.Address.Street))
            {
                Console.WriteLine("Client's adress - Street (letters only): ");
                client.Address.Street = Console.ReadLine();
            }
            while (!Validator.ValidateLettersAndNumbersString(client.Address.HouseNumber))
            {
                Console.WriteLine("Client's adress - House number (letters and numbers only): ");
                client.Address.HouseNumber = Console.ReadLine();
            }
            while (!Validator.ValidateMinMaxInt(client.Address.ZipCode))
            {
                Console.WriteLine("Client's adress - Zip Code: ");
                client.Address.ZipCode = Validator.GetInt();
            }
            while (!Validator.ValidateMinMaxInt(birthDay, 1, 31))
            {
                Console.WriteLine("Client's birth date - Day: ");
                birthDay = Validator.GetInt();
            }
            while (!Validator.ValidateMinMaxInt(birthMonth, 1, 12))
            {
                Console.WriteLine("Client's birth date - Month: ");
                birthMonth = Validator.GetInt();
            }
            while (!Validator.ValidateMinMaxInt(birthYear, 1, DateTime.Now.Year))
            {
                Console.WriteLine("Client's birth date - Year: ");
                birthYear = Validator.GetInt();
            }
            client.BirthDate = new DateTime(birthYear, birthMonth, birthDay);

            ClientsList.Add(client);
            Console.Clear();
            return client;
        }

        public Item AddItem()
        {
            Item item = new Item();

            var lastItem = ItemsList.LastOrDefault();
            item.Id = lastItem != null ? lastItem.Id + 1 : 1;
            Console.Clear();
            Console.WriteLine("Item's name: ");
            item.Name = Console.ReadLine();
            Console.WriteLine("\nShort description: ");
            item.Description = Console.ReadLine();
            while (!Validator.ValidateMinMaxInt(item.StartingPrice))
            {
                Console.WriteLine("\nStarting price [pln] (minimum 1): ");
                item.StartingPrice = Validator.GetInt();
            }
            while (!Validator.ValidateMinMaxInt(item.MinPrice))
            {
                Console.WriteLine("\nMinimum price [pln] (minimum 1): ");
                item.MinPrice = Validator.GetInt();
            }
            item.SalePrice = 0;

            ItemsList.Add(item);
            Console.Clear();
            return item;
        }

    }
}
