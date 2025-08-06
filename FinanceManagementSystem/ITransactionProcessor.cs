using FinanceManagementSystem.Models;       // for Transaction
using FinanceManagementSystem.Processors;   // for ITransactionProcessor

namespace FinanceManagementSystem.Interfaces
{
    using FinanceManagementSystem.Models;

    public interface ITransactionProcessor
    {
        void AddTransaction(Transaction transaction);
        void ListTransactions();
        decimal GetTotalAmount();
    }
}