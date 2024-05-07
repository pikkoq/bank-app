using Bank_app_api.Models;
using Bank_app_api.Models.DTO;
using Bank_app_api.Services.AccountService;
using Microsoft.AspNetCore.Mvc;

namespace Bank_app_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("GetAllAccounts")]
        public async Task<ActionResult<ServiceResponse<List<GetAccountDto>>>> GetAllAccounts()
        {
            return Ok(await _accountService.GetAllAccounts());
        }
        [HttpGet("GetSingleAccount{id}")]
        public async Task<ActionResult<ServiceResponse<GetAccountDto>>> GetSingleAccount(int id)
        {
            return Ok(await _accountService.GetSingleAccount(id));
        }
        [HttpPost("AddAccount")]
        public async Task<ActionResult<ServiceResponse<List<GetAccountDto>>>> AddAccount(AddAccountDto newAccount)
        {
            return Ok(await _accountService.AddAccount(newAccount));
        }

        [HttpPut("UpdateAccount")]
        public async Task<ActionResult<ServiceResponse<List<GetAccountDto>>>> UpdateAccount(UpdateAccountDto updatedAccount)
        {
            var response = await _accountService.UpdateAccount(updatedAccount);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteAccount{id}")]
        public async Task<ActionResult<ServiceResponse<GetAccountDto>>> DeleteAccount(int id)
        {
            var response = await _accountService.DeleteAccount(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetAccountsByOwnerId/{ownerId}")]
        public async Task<ActionResult<ServiceResponse<List<GetAccountDto>>>> GetAccountsByOwnerId(int ownerId)
        {
            return Ok(await _accountService.GetAccountsByOwnerId(ownerId));
        }

    }
}
