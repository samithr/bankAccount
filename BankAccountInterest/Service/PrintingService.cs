using BankAccountInterest.Extension;
using BankAccountInterest.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankAccountInterest.Service
{
    public class PrintingService
    {
        public string ProcessPrintingStatement(string accountNumber, string dateString)
        {
            if (ValidateData(accountNumber, dateString))
            {
                return GetTransactionByAccountAndDate(accountNumber, dateString);
            }
            return "No transactions found";
        }

        private bool ValidateData(string accountNumber, string dateString)
        {
            if (AccountDataValidator.ValidateDateUpteMonth(dateString))
            {
                return AccountDataValidator.ValidateAccountNumber(accountNumber);
            }
            return false;
        }

        private string GetTransactionByAccountAndDate(string accountNumber, string dateString)
        {
            var transactionService = new TransactionService();
            var transactionsForAccount = transactionService.GetTransactionByAccountAndDate(accountNumber, dateString).ToList();
            return PrintTransactionHistory(transactionsForAccount);
        }

        private string PrintTransactionHistory(IEnumerable<AccountTransaction> allTransactions)
        {
            var response = new StringBuilder();
            if (allTransactions.Any())
            {
                foreach (var item in allTransactions)
                {
                    _ = response.Append($"{item.Date} - {item.TransactionId} - {item.Type} - {item.Amount}\n");

                }
            }
            return response.ToString();
        }
    }
}
