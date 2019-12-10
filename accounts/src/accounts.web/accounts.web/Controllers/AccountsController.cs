using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using accounts.core;
using accounts.DAL.models;
using accounts.core.Dto;

namespace accounts.web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManager _accountManager;
        public const int AccountNoLength = 18;

        public AccountsController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        // GET: api/Accounts
        [HttpGet]
        public IEnumerable<Account> Get()
        {
            return _accountManager.GetAll();          
        }

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Accounts
        [HttpPost]
        public IActionResult Post(string accountNo)
        {
            //if (string.IsNullOrEmpty(accountNo))
            //    return BadRequest("Account no is empty.");

            //if (accountNo.Length != AccountNoLength)
            //    return BadRequest("Account no is invalid.");

            Account account = new Account
            {
                AccountNo = accountNo
            };
            _accountManager.CreateAccount(account);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Deposit(AccountDepositDto depositDto)
        {
            _accountManager.Deposit(depositDto);
            return Ok();
        }

        [HttpPost("[action]")]
        public IActionResult Transfer(AccountTransferDto transferDto)
        {
            _accountManager.Transfer(transferDto);
            return Ok();
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
