using System;
using System.Collections.Generic;

namespace WarehouseInventory
{
    // --- Marker Interface ---
    public interface IInventoryItem
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; set; }
    }

    // --- Electronic Product ---
    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public string Brand { get; }
        public int WarrantyMonths { get; }

        public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }
    }

    // --- Grocery Product ---
    public class GroceryItem : IInventoryItem
    {
        public int Id { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; }

        public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            ExpiryDate = expiryDate;
        }
    }

    // --- Custom Exceptions ---
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }

    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    // --- Generic Inventory Repository ---
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private Dictionary<int, T> _items = new Dictionary<int, T>();

        public void AddItem(T item)
        {
            if (_items.ContainsKey(item.Id))
                throw new DuplicateItemException($"Item with ID {item.Id} already exists.");

            _items[item.Id] = item;
        }

        public T GetItemById(int id)
        {
            if (!_items.ContainsKey(id))
                throw new ItemNotFoundException($"Item with ID {id} not found.");

            return _items[id];
        }

        public void RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
                throw new ItemNotFoundException($"Item with ID {id} not found.");

            _items.Remove(id);
        }

        public List<T> GetAllItems()
        {
            return new List<T>(_items.Values);
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
                throw new InvalidQuantityException("Quantity cannot be negative.");

            if (!_items.ContainsKey(id))
                throw new ItemNotFoundException($"Item with ID {id} not found.");

            _items[id].Quantity = newQuantity;
        }
    }

    // --- Warehouse Manager ---
    public class WareHouseManager
    {
        private InventoryRepository<ElectronicItem> _electronics = new InventoryRepository<ElectronicItem>();
        private InventoryRepository<GroceryItem> _groceries = new InventoryRepository<GroceryItem>();

        public void SeedData()
        {
            _electronics.AddItem(new ElectronicItem(1, "Smartphone", 20, "Infinix", 24));
            _electronics.AddItem(new ElectronicItem(2, "Laptop", 10, "Dell", 36));
            _electronics.AddItem(new ElectronicItem(3, "Air Conditioner", 5, "Samsung", 48));

            _groceries.AddItem(new GroceryItem(101, "Rice Bag", 50, DateTime.Now.AddMonths(6)));
            _groceries.AddItem(new GroceryItem(102, "Tomato Paste", 30, DateTime.Now.AddMonths(3)));
            _groceries.AddItem(new GroceryItem(103, "Bottled Water", 100, DateTime.Now.AddYears(1)));
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            var items = repo.GetAllItems();
            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}");
            }
        }

        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                repo.UpdateQuantity(id, item.Quantity + quantity);
                Console.WriteLine($"Updated quantity for {item.Name} to {item.Quantity + quantity}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating stock: {ex.Message}");
            }
        }

        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                repo.RemoveItem(id);
                Console.WriteLine($"Item with ID {id} successfully removed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
            }
        }

        public void AddDuplicateItemTest()
        {
            try
            {
                _electronics.AddItem(new ElectronicItem(1, "Duplicate Phone", 10, "Nokia", 12));
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Duplicate test: {ex.Message}");
            }
        }

        public void RemoveNonExistentItemTest()
        {
            RemoveItemById(_groceries, 999);
        }

        public void InvalidQuantityUpdateTest()
        {
            try
            {
                _groceries.UpdateQuantity(101, -5);
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Invalid quantity test: {ex.Message}");
            }
        }

        public void PrintGroceries() => PrintAllItems(_groceries);
        public void PrintElectronics() => PrintAllItems(_electronics);
    }

    // --- Main Execution ---
    class Program
    {
        static void Main()
        {
            var manager = new WareHouseManager();

            manager.SeedData();
            Console.WriteLine("\nGrocery Items:");
            manager.PrintGroceries();

            Console.WriteLine("\nElectronic Items:");
            manager.PrintElectronics();

            Console.WriteLine("\n--- Exception Handling Tests ---");
            manager.AddDuplicateItemTest();
            manager.RemoveNonExistentItemTest();
            manager.InvalidQuantityUpdateTest();

            Console.WriteLine("\nWarehouse system execution completed.");
        }
    }
}