using System;

public class BankTransferProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[BankTransfer] Processed {transaction.Amount:C} for '{transaction.Category}'.");
    }
}