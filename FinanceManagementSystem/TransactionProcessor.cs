using System;
using System.Collections.Generic;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Interfaces;
using FinanceManagementSystem.Models;       // for Transaction
using FinanceManagementSystem.Processors;   // for ITransactionProcessor

namespace FinanceManagementSystem.Services
{
    public sealed class TransactionProcessor : ITransactionProcessor
    {
        private readonly List<Transaction> transactions = [];

        public void AddTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
        }

        public void ListTransactions()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions recorded.");
                return;
            }

            foreach (var t in transactions)
            {
                Console.WriteLine($"ID: {t.Id}, Date: {t.Date:d}, Amount: {t.Amount:C}, Category: {t.Category}");
            }
        }

        public decimal GetTotalAmount()
        {
            decimal total = 0;
            foreach (var t in transactions)
            {
                total += t.Amount;
            }
            return total;
        }
    }
}