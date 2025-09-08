using System.Collections.Concurrent;
using EventSourcingDemo.Domain.Events;

namespace EventSourcingDemo.Infrastructure
{
    public sealed class InMemoryEventStore
    {
        private readonly ConcurrentDictionary<string, List<BankAccountEvent>> _store = new();

        public void Save(string accountId, IEnumerable<BankAccountEvent> events)
        {
            var list = _store.GetOrAdd(accountId, _ => new List<BankAccountEvent>());
            list.AddRange(events);
        }

        public IEnumerable<BankAccountEvent> GetEvents(string accountId)
        {
            return _store.TryGetValue(accountId, out var list) ? list : Enumerable.Empty<BankAccountEvent>();
        }
    }
}

