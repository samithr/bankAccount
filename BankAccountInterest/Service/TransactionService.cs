using BankAccountInterest.Enum;
using BankAccountInterest.Extension;
using BankAccountInterest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BankAccountInterest.Service
{
    public class TransactionService
    {
        // Better to move away from the source code        
        const string transactionFile = @"Data\transactions.json";
        const string transactionWriteFile = "..//..//.//..//Data\\transactions.json";

        public string PerformTransaction(string dateString, string accountNumber, string transactionType, string amountString)
        {
            if (ValidateData(dateString, accountNumber, transactionType, amountString))
            {
                var amount = decimal.Parse(amountString);
                var account = CheckAccount(accountNumber, transactionType, amount);
                if (account != null)
                {
                    if (transactionType.Equals(ExpetingInput.D.ToString()) || (transactionType.Equals(ExpetingInput.W.ToString()) && ValidateAccountBalance(account.Balance, amount)))
                    {
                        UpdateAccount(accountNumber, amount, transactionType);
                        return UpdateTransactions(dateString, accountNumber, amount, transactionType);
                    }
                    return "Insufficient account balance!";
                }
                return "Invalid transaction, Initial transaction cannot be withdraw type!";
            }
            return "Invalid input data!";
        }

        private bool ValidateData(string dateString, string accountNumber, string transactionType, string amountString)
        {
            if (AccountDataValidator.ValidateDate(dateString))
            {
                if (AccountDataValidator.ValidateAccountNumber(accountNumber))
                {
                    if (AccountDataValidator.ValidateTransactionType(transactionType))
                    {
                        return AccountDataValidator.ValidateDecimalAmount(amountString);
                    }
                }
            }
            return false;
        }

        private Account CheckAccount(string accountNumber, string transactionType, decimal amount)
        {
            var accountService = new AccountService();
            return accountService.GetAccount(accountNumber, transactionType, amount);
        }

        private bool ValidateAccountBalance(decimal accountBalance, decimal amount)
        {
            return accountBalance - amount > 0;
        }

        private void UpdateAccount(string accountNumber, decimal amount, string transactionType)
        {
            var accountService = new AccountService();
            accountService.UpdateAccount(accountNumber, amount, transactionType);
        }

        private IEnumerable<AccountTransaction> GetAllTransactions()
        {
            try
            {
                IEnumerable<AccountTransaction> transactionItems = null;
                if (File.Exists(transactionFile))
                {
                    using StreamReader r = new StreamReader(transactionFile);
                    string json = r.ReadToEnd();
                    transactionItems = JsonConvert.DeserializeObject<IEnumerable<AccountTransaction>>(json).ToList();
                }
                if (transactionItems.Any())
                {
                    return transactionItems;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string UpdateTransactions(string date, string accountNumber, decimal amount, string transactionType)
        {
            try
            {
                var allTransactions = GetAllTransactions();
                var transactionRelatedThisAccount = allTransactions
                    .Where(transaction => transaction.AccountNumber.Equals(accountNumber))
                    .Select(transaction => transaction.TransactionId)
                    .Count();
                var transactionId = GenerateTransactionId(date, transactionRelatedThisAccount);
                var newTransaction = new AccountTransaction()
                {
                    AccountNumber = accountNumber,
                    Amount = amount,
                    Date = date,
                    Type = transactionType[0],
                    TransactionId = transactionId
                };
                var newTransactions = allTransactions.ToList();
                newTransactions.Add(newTransaction);
                var newTransactionsList = JsonConvert.SerializeObject(newTransactions, Formatting.Indented);
                File.WriteAllText(transactionWriteFile, newTransactionsList);
                return TransactionHistory(accountNumber, newTransactions);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateTransactionId(string date, int transactionIdCount)
        {
            if (transactionIdCount > 0)
            {
                return $"{date}-0{transactionIdCount + 1}";
            }
            return $"{date}-01";
        }

        private string TransactionHistory(string accountNumber, IEnumerable<AccountTransaction> allTransactions)
        {
            var response = new StringBuilder();
            var transactionsRelatedToAccount = allTransactions.Where(transaction => transaction.AccountNumber.Equals(accountNumber)).ToList();
            if (transactionsRelatedToAccount.Any())
            {                
                foreach (var item in transactionsRelatedToAccount)
                {
                    _ = response.Append($"{item.Date} - {item.TransactionId} - {item.Type} - {item.Amount}\n");
                }
            }
            return response.ToString();
        }
    }
}
