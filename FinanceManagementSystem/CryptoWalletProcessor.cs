using System;

public class CryptoWalletProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[CryptoWallet] Transferred {transaction.Amount:C} tagged as '{transaction.Category}'.");
    }
}