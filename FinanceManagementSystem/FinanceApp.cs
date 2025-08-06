using System;
using System.Collections.Generic;
using FinanceManagementSystem.Models;       // for Transaction
using FinanceManagementSystem.Processors;   // for ITransactionProcessor

public class FinanceApp
{
    private readonly List<Transaction> _transactions = new();

    public void Run()
    {
        // Instantiate account
        var savingsAccount = new SavingsAccount("ACC12345", 1000m);

        // Create transactions
        var t1 = new Transaction(1, DateTime.Now, 150m, "Groceries");
        var t2 = new Transaction(2, DateTime.Now, 200m, "Utilities");
        var t3 = new Transaction(3, DateTime.Now, 300m, "Entertainment");

        // Process each transaction
        ITransactionProcessor mobile = new MobileMoneyProcessor();
        ITransactionProcessor bank = new BankTransferProcessor();
        ITransactionProcessor crypto = new CryptoWalletProcessor();

        mobile.Process(t1);
        bank.Process(t2);
        crypto.Process(t3);

        // Apply transactions to account
        savingsAccount.ApplyTransaction(t1);
        savingsAccount.ApplyTransaction(t2);
        savingsAccount.ApplyTransaction(t3);

        // Track all transactions
        _transactions.AddRange(new[] { t1, t2, t3 });

        Console.WriteLine("\n📋 Transaction Log:");
        foreach (var tx in _transactions)
        {
            Console.WriteLine($"ID: {tx.Id}, Category: {tx.Category}, Amount: {tx.Amount:C}, Date: {tx.Date:d}");
        }
    }
}