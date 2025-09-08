# EventSourcingDemo

A simple ASP.NET Core Web API project demonstrating the **Event Sourcing** design pattern using a bank account domain.

---

## What is Event Sourcing?

Event Sourcing is a design pattern where **state changes are stored as a sequence of events**. Instead of persisting only the current state, every change (event) is recorded. The current state is reconstructed by replaying these events.

**Benefits:**
- Full audit trail of all changes
- Easy to debug and reason about
- Enables features like temporal queries and event replay

---

## What Does This App Do?

This app simulates a simple bank account system with the following features:
- **Create an account**
- **Deposit money**
- **Withdraw money**
- **Get current balance**

All actions are stored as events. The balance is calculated by replaying all events for an account.

---

## Project Structure

```
/EventSourcingDemo
  /Controllers
    - BankAccountController.cs   # API endpoints
  /Domain
    - Events.cs                  # Event definitions
    - BankAccount.cs             # Aggregate root
  /Infrastructure
    - InMemoryEventStore.cs      # In-memory event store
  Program.cs                     # App entry point
  README.md                      # This file
```

---

## How It Works

1. **Events**
   - `AccountCreatedEvent`: Account is created
   - `MoneyDepositedEvent`: Money is deposited
   - `MoneyWithdrawnEvent`: Money is withdrawn

2. **Aggregate (BankAccount)**
   - Applies events to mutate state
   - Emits new events for each action
   - Can be rehydrated from a list of events

3. **Event Store**
   - Stores all events in memory (for demo purposes)
   - Retrieves events by account ID

4. **API Controller**
   - Exposes endpoints to create accounts, deposit, withdraw, and get balance

---

## API Endpoints

- **Create Account**
  - `POST /api/bankaccount/create/{accountId}`
- **Deposit**
  - `POST /api/bankaccount/deposit/{accountId}?amount=100`
- **Withdraw**
  - `POST /api/bankaccount/withdraw/{accountId}?amount=50`
- **Get Balance**
  - `GET /api/bankaccount/balance/{accountId}`

---

## Example Usage

1. **Create an account:**
   ```sh
   curl -X POST http://localhost:5000/api/bankaccount/create/myaccount
   ```
2. **Deposit money:**
   ```sh
   curl -X POST "http://localhost:5000/api/bankaccount/deposit/myaccount?amount=200"
   ```
3. **Withdraw money:**
   ```sh
   curl -X POST "http://localhost:5000/api/bankaccount/withdraw/myaccount?amount=50"
   ```
4. **Get balance:**
   ```sh
   curl http://localhost:5000/api/bankaccount/balance/myaccount
   ```

---

## Why Use Event Sourcing?

- **Auditability:** Every change is recorded as an event.
- **Debugging:** You can replay events to reproduce bugs or understand state changes.
- **Extensibility:** New features (like notifications or projections) can be built by subscribing to events.

---

## Notes
- This demo uses an in-memory event store for simplicity. In production, use a persistent store (like a database).
- The code is intentionally simple for educational purposes.

---

## License
MIT
