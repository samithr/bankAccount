namespace BankAccountInterest.Model
{
    public class AccountTransaction
    {
        public string AccountNumber { get; set; }
        public string Date { get; set; }
        public string TransactionId { get; set; }
        public char Type { get; set; }
        public decimal Amount { get; set; }
    }
}
