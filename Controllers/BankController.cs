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
        public ActionResult GetBalance([FromQuery]int account_id) 
        { 
            var balance = _bank.GetBalance(account_id);

            return Ok(balance);
        }

        [HttpPost("event")]
        public ActionResult Event([FromBody]Event bankEvent)
        {
            switch (bankEvent.Type)
            {
                case "deposit":
                    bankEvent.Destination.Balance = 
                        _bank.Deposit(bankEvent.Destination.Id, bankEvent.Destination.Balance);
                    break;

                default:
                    break;
            }


            return Ok(bankEvent.Destination);
        }
    }
}
