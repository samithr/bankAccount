using BankAccountInterest.Enum;
using System;
using System.Globalization;

namespace BankAccountInterest.Extension
{
    public static class AccountDataValidator
    {
        public static bool ValidateDate(string dateString)
        {
            var date = DateTime.ParseExact(dateString, "yyyyMMdd", CultureInfo.InvariantCulture);
            return date.ToString("yyyyMMdd").Equals(dateString);
        }

        public static bool ValidateAccountNumber(string accountNumber)
        {
            return !string.IsNullOrEmpty(accountNumber);
        }

        public static bool ValidateTransactionType(string transactionType)
        {
            return transactionType.Equals(ExpetingInput.D.ToString()) || transactionType.Equals(ExpetingInput.W.ToString());
        }

        public static bool ValidateTransactionAmount(string amountString)
        {
            double.TryParse(amountString, out double amount);
            return amount > 0;
        }
    }
}
