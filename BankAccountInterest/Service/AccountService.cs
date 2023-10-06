using BankAccountInterest.Enum;
using BankAccountInterest.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankAccountInterest.Service
{
    public class AccountService
    {
        // Better to move away from the source code
        const string accountFile = @"Data\accounts.json";
        const string accountWriteFilePath = "..//..//.//..//Data\\accounts.json";

        public Account GetAccount(string accountNumber, string transactionType, decimal amount)
        {
            try
            {
                if (string.IsNullOrEmpty(accountNumber))
                {
                    return null;
                }
                else
                {
                    var accountItems = GetAllAccountFromData();
                    var account = accountItems.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber));
                    if (account != null)
                    {
                        return account;
                    }
                    else
                    {
                        if (transactionType.Equals(ExpetingInput.W.ToString()))
                        {
                            return null;
                        }
                        else
                        {
                            return CreateNew(accountNumber, amount, accountItems);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Account> GetAllAccountFromData()
        {
            try
            {
                IEnumerable<Account> accountItems = null;
                if (File.Exists(accountFile))
                {
                    using StreamReader r = new StreamReader(accountFile);
                    string json = r.ReadToEnd();
                    accountItems = JsonConvert.DeserializeObject<IEnumerable<Account>>(json).ToList();
                }
                if (accountItems.Any())
                {
                    return accountItems;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Account CreateNew(string accountNumber, decimal amount, IEnumerable<Account> accountItems = null)
        {
            var newAccount = new Account()
            {
                AccountNumber = accountNumber,
                Balance = amount
            };
            var allAccounts = accountItems.ToList();
            allAccounts.Add(newAccount);
            var newAccountList = JsonConvert.SerializeObject(allAccounts, Formatting.Indented);
            File.WriteAllText(accountWriteFilePath, newAccountList);
            return newAccount;

        }

        public void UpdateAccount(string accountNumber, decimal amount, string transactionType)
        {
            try
            {
                var allAccountItems = GetAllAccountFromData();
                var account = allAccountItems.FirstOrDefault(account => account.AccountNumber.Equals(accountNumber));
                if (account != null)
                {
                    if (transactionType.Equals(ExpetingInput.D.ToString()))
                    {
                        account.Balance += amount;
                    }
                    else if (transactionType.Equals(ExpetingInput.W.ToString()))
                    {
                        account.Balance -= amount;
                    }
                    var newAccountList = JsonConvert.SerializeObject(allAccountItems, Formatting.Indented);
                    File.WriteAllText(accountWriteFilePath, newAccountList);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
