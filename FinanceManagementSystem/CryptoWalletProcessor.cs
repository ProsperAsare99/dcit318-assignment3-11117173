using System;
using FinanceManagementSystem.Models;       // for Transaction
using FinanceManagementSystem.Processors;   // for ITransactionProcessor

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[CryptoWallet] Transferred {transaction.Amount:C} tagged as '{transaction.Category}'.");
    }
}