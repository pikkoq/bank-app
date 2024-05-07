using Bank_app_api.Models;
using Bank_app_api.Models.DTO;
using Bank_app_api.Services.TransactionService;
using Bank_app_api.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Bank_app_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("GetAllTransactions")]
        public async Task<ActionResult<ServiceResponse<List<GetTransactionDto>>>> GetAllTransactions()
        {
            return Ok(await _transactionService.GetAllTransactions());
        }
        [HttpGet("GetSingleTransaction{id}")]
        public async Task<ActionResult<ServiceResponse<GetTransactionDto>>> GetSingleTransaction(int id)
        {
            return Ok(await _transactionService.GetSingleTransaction(id));
        }
        [HttpPost("AddTransactionAdmin")]
        public async Task<ActionResult<ServiceResponse<List<GetTransactionDto>>>> AddTransactionAdmin(AddTransactionDto newTransaction)
        {
            return Ok(await _transactionService.AddTransaction(newTransaction));
        }
        [HttpPost("AddTransaction")]
        public async Task<ActionResult<ServiceResponse<List<GetTransactionDto>>>> AddTransaction(AddTransactionDto newTransaction)
        {
            return Ok(await _transactionService.AddTransaction(newTransaction));
        }

        [HttpPut("UpdateTransaction")]
        public async Task<ActionResult<ServiceResponse<List<GetTransactionDto>>>> UpdateTransaction(UpdateTransactionDto updatedTransaction)
        {
            var response = await _transactionService.UpdateTransaction(updatedTransaction);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteTransaction{id}")]
        public async Task<ActionResult<ServiceResponse<GetTransactionDto>>> DeleteTransaction(int id)
        {
            var response = await _transactionService.DeleteTransaction(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpGet("GetTransactionsFromByNumber/{number}")]
        public async Task<ActionResult<ServiceResponse<List<GetTransactionDto>>>> GetTransactionsFromByNumber(string number)
        {
            return Ok(await _transactionService.GetTransactionsFromByAccount(number));
        }
        [HttpGet("GetTransactionsToByNumber/{number}")]
        public async Task<ActionResult<ServiceResponse<List<GetTransactionDto>>>> GetTransactionsToByNumber(string number)
        {
            return Ok(await _transactionService.GetTransactionsToByAccount(number));
        }
    }
}
