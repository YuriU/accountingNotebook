using System;
using System.Collections.Generic;
using Api.Dtos.Account;
using Contracts.Accounting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IAccountingService _accountingService;

        public AccountController(ILogger<AccountController> logger, IAccountingService accountingService)
        {
            _logger = logger;
            _accountingService = accountingService;
        }

        [HttpGet("transactions")]
        public List<AccountTransaction> GetTransactionLog()
        {
            return _accountingService.GetTransactionLog();
        }
        
        [HttpGet("currentAmount")]
        public decimal GetCurrentAmount()
        {
            return _accountingService.GetCurrentAmount();
        }

        [HttpPut("putMoney")]
        public void PutMoney(PutMoneyRequest request)
        {
            _accountingService.PutMoney(request.CreditCardFrom, request.Amount);
        }
        
        [HttpPost("transferMoney")]
        public IActionResult TransferMoney(TransferMoneyDto request)
        {
            try
            {
                _accountingService.TransferMoney(request.CreditCardTo, request.Amount);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}