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
        private readonly IBank _bankService;
        
        public BankController(ILogger<BankController> logger, IBank bankService)
        {
            _logger = logger;
            _bankService = bankService;
        }

        [HttpPost("reset")]
        public ActionResult Reset()
        {
            try
            {
                _bankService.Reset();
                return Ok("OK");
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("balance")]
        public ActionResult GetBalance([FromQuery]string account_id)
        {
            try
            {
                return Ok(_bankService.GetBalance(account_id));
            }
            catch (Exception ex)
            {
                return NotFound(0);
            }
        }

        [HttpPost("event")]
        public ActionResult Event([FromBody]Event bankEvent)
        {
            try
            {
                switch (bankEvent.Type)
                {
                    case "deposit":
                        return Created(string.Empty, new EventAnswer() { Destination = _bankService.Deposit(bankEvent.Destination, bankEvent.Amount) });

                    case "withdraw":
                        return Created(string.Empty, new EventAnswer() { Origin = _bankService.Withdraw(bankEvent.Origin, bankEvent.Amount) });

                    case "transfer":
                        IEnumerable<Account> result = _bankService.Transfer(bankEvent.Origin, bankEvent.Destination, bankEvent.Amount);
                        return Created(string.Empty, new EventAnswer() { Origin = result.ElementAt(0), Destination = result.ElementAt(1) });

                    default:
                        return BadRequest("Event is not valid");
                }
            }catch (Exception ex)
            {
                return NotFound(0);
            }
        }
    }
}
