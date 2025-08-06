using FinanceManagementSystem.Models;       // for Transaction
using FinanceManagementSystem.Processors;   // for ITransactionProcessor

public sealed class SavingsAccount : Account
{
    public SavingsAccount(string accountNumber, decimal initialBalance)
        : base(accountNumber, initialBalance) {}

    public override void ApplyTransaction(Transaction transaction)
    {
        if (transaction.Amount > Balance)
        {
            Console.WriteLine("Insufficient funds");
        }
        else
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"[SavingsAccount] Applied {transaction.Amount:C}. New balance: {Balance:C}");
        }
    }
}