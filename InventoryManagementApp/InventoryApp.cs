using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace InventorySystem
{
    // --- Marker Interface ---
    public interface IInventoryEntity
    {
        int Id { get; }
    }

    // --- Immutable Inventory Record ---
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;

    // --- Generic Inventory Logger ---
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private List<T> _log = new List<T>();
        private readonly string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item)
        {
            _log.Add(item);
        }

        public List<T> GetAll()
        {
            return _log;
        }

        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(_log, new JsonSerializerOptions { WriteIndented = true });
                using (StreamWriter writer = new StreamWriter(_filePath))
                {
                    writer.Write(json);
                }
                Console.WriteLine("Inventory saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving inventory: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("Inventory file not found.");
                    return;
                }

                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string json = reader.ReadToEnd();
                    var items = JsonSerializer.Deserialize<List<T>>(json);
                    if (items != null)
                    {
                        _log = items;
                        Console.WriteLine("Inventory loaded successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading inventory: {ex.Message}");
            }
        }
    }

    // --- Integration Layer ---
    public class InventoryApp
    {
        private InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string filePath)
        {
            _logger = new InventoryLogger<InventoryItem>(filePath);
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(101, "Stapler", 12, DateTime.Now));
            _logger.Add(new InventoryItem(102, "Notebook", 30, DateTime.Now));
            _logger.Add(new InventoryItem(103, "Desk Lamp", 7, DateTime.Now));
            _logger.Add(new InventoryItem(104, "Extension Cord", 18, DateTime.Now));
            _logger.Add(new InventoryItem(105, "Whiteboard Marker", 50, DateTime.Now));
        }

        public void SaveData()
        {
            _logger.SaveToFile();
        }

        public void LoadData()
        {
            _logger.LoadFromFile();
        }

        public void PrintAllItems()
        {
            var items = _logger.GetAll();
            Console.WriteLine("\nInventory Items:");
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Added: {item.DateAdded:g}");
            }
        }
    }

    // --- Main Application ---
    class Program
    {
        static void Main()
        {
            string filePath = "inventory.json";

            var app = new InventoryApp(filePath);
            app.SeedSampleData();
            app.SaveData();

            // Simulate fresh session
            app = new InventoryApp(filePath);
            app.LoadData();
            app.PrintAllItems();
        }
    }
}