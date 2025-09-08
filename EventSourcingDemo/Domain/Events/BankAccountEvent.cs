namespace EventSourcingDemo.Domain.Events
{
    public abstract class BankAccountEvent
    {
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}

