using Microsoft.AspNetCore.Mvc;
using EventSourcingDemo.Domain;
using EventSourcingDemo.Abstractions;

namespace EventSourcingDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankAccountController : ControllerBase
    {
        private readonly IEventStore _eventStore;
        public BankAccountController(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        [HttpPost("create/{accountId}")]
        public IActionResult Create(string accountId)
        {
            var account = new BankAccount(accountId);
            _eventStore.Save(accountId, account.Changes);
            return Ok(new { accountId });
        }

        [HttpPost("deposit/{accountId}")]
        public IActionResult Deposit(string accountId, [FromQuery] decimal amount)
        {
            var events = _eventStore.GetEvents(accountId);
            var account = BankAccount.Rehydrate(accountId, events);
            account.Deposit(amount);
            _eventStore.Save(accountId, account.Changes);
            return Ok(new { accountId, account.Balance });
        }

        [HttpPost("withdraw/{accountId}")]
        public IActionResult Withdraw(string accountId, [FromQuery] decimal amount)
        {
            var events = _eventStore.GetEvents(accountId);
            var account = BankAccount.Rehydrate(accountId, events);
            try
            {
                account.Withdraw(amount);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            _eventStore.Save(accountId, account.Changes);
            return Ok(new { accountId, account.Balance });
        }

        [HttpGet("balance/{accountId}")]
        public IActionResult GetBalance(string accountId)
        {
            var events = _eventStore.GetEvents(accountId);
            if (!events.Any())
                return NotFound("Account not found");
            var account = BankAccount.Rehydrate(accountId, events);
            return Ok(new { accountId, account.Balance });
        }
    }
}

