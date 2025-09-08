namespace EventSourcingDemo.Domain.Events
{
    public class MoneyWithdrawnEvent : BankAccountEvent
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public MoneyWithdrawnEvent(string accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}

