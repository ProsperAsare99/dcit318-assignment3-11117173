using System;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Services;

namespace FinanceManagementSystem
{
    class FinanceApp
    {
        static void Main()
        {
            var processor = new TransactionProcessor();

            processor.AddTransaction(new Transaction(1, DateTime.Now, 250.00m, "Groceries"));
            processor.AddTransaction(new Transaction(2, DateTime.Now, 125.75m, "Utilities"));

            Console.WriteLine("\nTransactions:");
            processor.ListTransactions();

            Console.WriteLine($"\nTotal Amount: {processor.GetTotalAmount():C}");
        }
    }
}