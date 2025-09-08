
using EventSourcingDemo.Domain.Events;

namespace EventSourcingDemo.Domain
{
    public sealed class BankAccount
    {
        public string AccountId { get; private set; }
        public decimal Balance { get; private set; }
        public List<BankAccountEvent> Changes { get; } = [];

        public BankAccount(string accountId)
        {
            Apply(new AccountCreatedEvent(accountId));
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            Apply(new MoneyDepositedEvent(AccountId, amount));
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            if (Balance < amount) throw new InvalidOperationException("Insufficient funds");
            Apply(new MoneyWithdrawnEvent(AccountId, amount));
        }

        private void Apply(BankAccountEvent @event)
        {
            switch (@event)
            {
                case AccountCreatedEvent e:
                    AccountId = e.AccountId;
                    Balance = 0;
                    break;
                case MoneyDepositedEvent e:
                    Balance += e.Amount;
                    break;
                case MoneyWithdrawnEvent e:
                    Balance -= e.Amount;
                    break;
            }
            Changes.Add(@event);
        }

        public static BankAccount Rehydrate(string accountId, IEnumerable<BankAccountEvent> events)
        {
            var account = new BankAccount(accountId);
            account.Changes.Clear();
            foreach (var e in events)
            {
                account.Apply(e);
            }
            account.Changes.Clear(); 
            return account;
        }
    }
}

