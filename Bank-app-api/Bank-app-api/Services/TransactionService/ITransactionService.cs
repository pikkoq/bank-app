using Bank_app_api.Models;
using Bank_app_api.Models.DTO;

namespace Bank_app_api.Services.TransactionService
{
    public interface ITransactionService
    {
        Task<ServiceResponse<List<GetTransactionDto>>> GetAllTransactions();
        Task<ServiceResponse<GetTransactionDto>> GetSingleTransaction(int id);
        Task<ServiceResponse<List<GetTransactionDto>>> AddTransactionAdmin(AddTransactionDto newTransaction);
        Task<ServiceResponse<List<GetTransactionDto>>> AddTransaction(AddTransactionDto newTransaction);

        Task<ServiceResponse<GetTransactionDto>> UpdateTransaction(UpdateTransactionDto updatedTransaction);
        Task<ServiceResponse<List<GetTransactionDto>>> DeleteTransaction(int id);
        Task<ServiceResponse<List<GetTransactionDto>>> GetTransactionsFromByAccount(string accountFrom);
        Task<ServiceResponse<List<GetTransactionDto>>> GetTransactionsToByAccount(string accountTo);


    }
}
