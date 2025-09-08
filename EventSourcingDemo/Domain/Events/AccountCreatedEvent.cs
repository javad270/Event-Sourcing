namespace EventSourcingDemo.Domain.Events
{
    public class AccountCreatedEvent : BankAccountEvent
    {
        public string AccountId { get; set; }
        public AccountCreatedEvent(string accountId)
        {
            AccountId = accountId;
        }
    }
}

