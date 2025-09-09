using EventSourcingDemo.Domain.Events;

namespace EventSourcingDemo.Abstractions
{
    public interface IEventStore
    {
        void Save(string accountId, IEnumerable<BankAccountEvent> events);
        IEnumerable<BankAccountEvent> GetEvents(string accountId);
    }
}

