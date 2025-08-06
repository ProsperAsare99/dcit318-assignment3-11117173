using FinanceManagementSystem.Models;       // for Transaction
using FinanceManagementSystem.Processors;   // for ITransactionProcessor

namespace FinanceManagementSystem.Models
{
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);
}