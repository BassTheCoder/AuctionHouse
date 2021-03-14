using AuctionHouse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace AuctionHouse.Helpers
{
    class DatabaseManager
    {
        private static readonly string filePath = Directory.GetCurrentDirectory();
        private const string clientsListFileName = "clientslist.json";
        private const string itemsListFileName = "itemslist.json";

        public static void SaveToFile(Database database)
        {
            using StreamWriter clientsStreamWriter = new StreamWriter(filePath + "\\" + clientsListFileName);
            using StreamWriter itemsStreamWriter = new StreamWriter(filePath + "\\" + itemsListFileName);

            string clientsJson = JsonSerializer.Serialize(database.ClientsList);
            string itemsJson = JsonSerializer.Serialize(database.ItemsList);
            clientsStreamWriter.Write(clientsJson);
            clientsStreamWriter.Flush();
            itemsStreamWriter.Write(itemsJson);
            itemsStreamWriter.Flush();
        }

        public static void ReadClientsList(Database database)
        {
            List<Client> clients = new List<Client>();

            try
            {
                using StreamReader clientsStreamReader = new StreamReader(filePath + "\\" + clientsListFileName);
                string clientsJson = clientsStreamReader.ReadToEnd();
                clients = JsonSerializer.Deserialize<List<Client>>(clientsJson);
            }
            catch (Exception)
            {
                Console.WriteLine("\nList of clients not found. No data has been loaded. \nPress any key to continue.\n");
                Console.ReadKey();
            }

            database.ClientsList = clients;
        }

        public static void ReadItemsList(Database database)
        {
            List<Item> items = new List<Item>();

            try
            {
                using StreamReader itemsStreamReader = new StreamReader(filePath + "\\" + itemsListFileName);
                string itemsJson = itemsStreamReader.ReadToEnd();
                items = JsonSerializer.Deserialize<List<Item>>(itemsJson);
            }
            catch (Exception)
            {
                Console.WriteLine("\nList of items not found. No data has been loaded. \nPress any key to continue.\n");
                Console.ReadKey();
            }

            database.ItemsList = items;
        }

        public static void DisplayItem(Item item)
        {
            Console.WriteLine("Id: " + item.Id);
            Console.WriteLine("Name: " + item.Name);
            Console.WriteLine("Description: " + item.Description);
            Console.WriteLine("Starting price: " + item.StartingPrice);
            Console.WriteLine("Minimal price: " + item.MinPrice);
            Console.Write("Sale price: ");
            if (!item.IsSold)
            {
                Console.WriteLine("Not sold yet!");
            }
            else
            {
                Console.WriteLine(item.SalePrice + " PLN. Bought by " + item.Owner.Name + " " + item.Owner.Surname);
            }
            Console.WriteLine();
        }

        public static void DisplayItems(Database database)
        {
            Console.WriteLine();
            Console.WriteLine("List of items:");
            Console.WriteLine();
            foreach (Item item in database.ItemsList)
            {
                Console.WriteLine();
                DisplayItem(item);
                Console.WriteLine("------------------------");
                Console.WriteLine();
            }
            Console.WriteLine("===========================");
            Console.WriteLine();
        }

        public static void DisplayClients(Database database)
        {
            Console.WriteLine();
            Console.WriteLine("List of clients:");
            Console.WriteLine();
            foreach (Client client in database.ClientsList)
            {
                Console.WriteLine("Id: " + client.Id);
                Console.WriteLine("Name: " + client.Name);
                Console.WriteLine("Surname: " + client.Surname);
                Console.WriteLine($"Age: {DateTime.Now.Year - client.BirthDate.Year}");
                Console.Write("Is client adult: ");
                if (client.IsAdult)
                {
                    Console.WriteLine("Yes");
                }
                else
                {
                    Console.WriteLine("No");
                }
                Console.WriteLine();
                Console.WriteLine("------------------------");
                Console.WriteLine();
            }
            Console.WriteLine("===========================");
            Console.WriteLine();
        }
    }
}
