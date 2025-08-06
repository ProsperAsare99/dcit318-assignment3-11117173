using FinanceManagementSystem.Models;       // for Transaction
using FinanceManagementSystem.Processors;   // for ITransactionProcessorpublic class Program
{
    public static void Main()
    {
        var app = new FinanceApp();
        app.Run();
    }
}