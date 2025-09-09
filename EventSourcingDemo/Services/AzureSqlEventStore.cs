using System.Text.Json;
using Dapper;
using EventSourcingDemo.Abstractions;
using EventSourcingDemo.Domain.Events;
using Microsoft.Data.SqlClient;

namespace EventSourcingDemo.Services
{
    public class AzureSqlEventStore : IEventStore
    {
        private readonly string _connectionString;
        public AzureSqlEventStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Save(string accountId, IEnumerable<BankAccountEvent> events)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            foreach (var @event in events)
            {
                var eventType = @event.GetType().AssemblyQualifiedName;
                var eventData = JsonSerializer.Serialize(@event, @event.GetType());
                var occurredAt = @event.OccurredAt;
                connection.Execute(
                    "INSERT INTO Events (AccountId, EventType, EventData, OccurredAt) VALUES (@AccountId, @EventType, @EventData, @OccurredAt)",
                    new { AccountId = accountId, EventType = eventType, EventData = eventData, OccurredAt = occurredAt }
                );
            }
        } 

        public IEnumerable<BankAccountEvent> GetEvents(string accountId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var rows = connection.Query(
                "SELECT EventType, EventData FROM Events WHERE AccountId = @AccountId ORDER BY Id ASC",
                new { AccountId = accountId }
            );
            var events = new List<BankAccountEvent>();
            foreach (var row in rows)
            {
                var type = Type.GetType((string)row.EventType);
                if (type == null) continue;
                var @event = (BankAccountEvent?)JsonSerializer.Deserialize((string)row.EventData, type);
                if (@event != null)
                    events.Add(@event);
            }
            return events;
        }
    }
}
