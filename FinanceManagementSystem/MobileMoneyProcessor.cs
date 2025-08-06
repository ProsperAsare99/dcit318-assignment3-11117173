using System;

public class MobileMoneyProcessor : ITransactionProcessor
{
    public void Process(Transaction transaction)
    {
        Console.WriteLine($"[MobileMoney] Sent {transaction.Amount:C} for '{transaction.Category}'.");
    }
}