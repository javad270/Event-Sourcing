namespace EventSourcingDemo.Domain.Events
{
    public class MoneyDepositedEvent : BankAccountEvent
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public MoneyDepositedEvent(string accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}

