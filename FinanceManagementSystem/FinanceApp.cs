using System;
using System.Collections.Generic;
using FinanceManagementSystem.Models;

public class FinanceApp
{
    private List<Transaction> _transactions = new();

    public void Run()
    {
        // Step i: Initialize account
        var savingsAccount = new SavingsAccount("ACC-000111999", 1000m);

        // Step ii: Create sample transactions
        var t1 = new Transaction(1, DateTime.Now, 150m, "Groceries");
        var t2 = new Transaction(2, DateTime.Now, 200m, "Utilities");
        var t3 = new Transaction(3, DateTime.Now, 300m, "Entertainment");

        // Step iii: Process transactions via different processors
        ITransactionProcessor mobileProcessor = new MobileMoneyProcessor();
        ITransactionProcessor bankProcessor = new BankTransferProcessor();
        ITransactionProcessor cryptoProcessor = new CryptoWalletProcessor();

        mobileProcessor.Process(t1);
        bankProcessor.Process(t2);
        cryptoProcessor.Process(t3);

        // Step iv: Apply transactions to SavingsAccount
        savingsAccount.ApplyTransaction(t1);
        savingsAccount.ApplyTransaction(t2);
        savingsAccount.ApplyTransaction(t3);

        // Step v: Track all transactions
        _transactions.AddRange(new[] { t1, t2, t3 });

        Console.WriteLine("\n📋 Transaction log:");
        foreach (var tx in _transactions)
        {
            Console.WriteLine($"ID: {tx.Id}, Category: {tx.Category}, Amount: {tx.Amount:C}, Date: {tx.Date:d}");
        }
    }

    public static void Main()
    {
        var app = new FinanceApp();
        app.Run();
    }
}