using System;
using System.Collections.Generic;
using System.Globalization;

namespace FinanceManagementSystem
{
    public record Transaction(
        int Id,
        DateTime Date,
        decimal Amount,
        string Category
    );

    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Bank transfer of ₵{transaction.Amount:N2} in category '{transaction.Category}' processed.");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Mobile money payment of ₵{transaction.Amount:N2} for '{transaction.Category}' completed.");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Crypto transaction of ₵{transaction.Amount:N2} categorized as '{transaction.Category}' executed.");
        }
    }

    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Transaction applied. New balance: ₵{Balance:N2}");
        }
    }

    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance)
            : base(accountNumber, initialBalance)
        {
        }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                Balance -= transaction.Amount;
                Console.WriteLine($"Transaction successful. Updated balance: ₵{Balance:N2}");
            }
        }
    }

    public class FinanceApp
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public void Run()
        {
            var savingsAccount = new SavingsAccount("ACC-2025-001", 1000m);

            var transaction1 = new Transaction(1, DateTime.Now, 150m, "Groceries");
            var transaction2 = new Transaction(2, DateTime.Now, 300m, "Utilities");
            var transaction3 = new Transaction(3, DateTime.Now, 600m, "Entertainment");

            ITransactionProcessor mobileProcessor = new MobileMoneyProcessor();
            ITransactionProcessor bankProcessor = new BankTransferProcessor();
            ITransactionProcessor cryptoProcessor = new CryptoWalletProcessor();

            mobileProcessor.Process(transaction1);
            bankProcessor.Process(transaction2);
            cryptoProcessor.Process(transaction3);

            savingsAccount.ApplyTransaction(transaction1);
            savingsAccount.ApplyTransaction(transaction2);
            savingsAccount.ApplyTransaction(transaction3);

            _transactions.AddRange(new[] { transaction1, transaction2, transaction3 });

            Console.WriteLine("\nTransaction Summary:");
            foreach (var tx in _transactions)
            {
                Console.WriteLine($"- {tx.Category}: ₵{tx.Amount:N2} on {tx.Date:d}");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var app = new FinanceApp();
            app.Run();

            Console.WriteLine("\nFinance app execution completed.");
        }
    }
}