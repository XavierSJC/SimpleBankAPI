using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBankAPI.Models;
using SimpleBankAPI.Services;

namespace SimpleBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly ILogger<BankController> _logger;
        private readonly IBank _bank;
        
        public BankController(ILogger<BankController> logger, IBank bank)
        {
            _logger = logger;
            _bank = bank;
        }

        [HttpPost("reset")]
        public ActionResult Reset()
        {
            _bank.Reset();
            return Ok("OK");
        }

        [HttpGet("balance")]
        public ActionResult GetBalance([FromQuery]string account_id) 
        { 
            return Ok(_bank.GetBalance(account_id));
        }

        [HttpPost("event")]
        public ActionResult Event([FromBody]Event bankEvent)
        {
            float finalBalance;

            switch (bankEvent.Type)
            {
                case "deposit":
                    finalBalance = 
                        _bank.Deposit(bankEvent.Destination, bankEvent.Amount);

                    return Ok(new EventBankAnswer() { Destination = new() { Id = bankEvent.Destination, Balance = finalBalance} });

                case "withdraw":
                    finalBalance =
                        _bank.Withdraw(bankEvent.Origin, bankEvent.Amount);

                    return Ok(finalBalance);

                case "transfer":
                    _bank.Transfer(bankEvent.Origin, bankEvent.Destination, bankEvent.Amount);

                    return Ok();

                default:
                    return BadRequest("Event is not valid");
            }
        }
    }
}
