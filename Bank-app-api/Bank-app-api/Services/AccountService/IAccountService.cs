using Bank_app_api.Models;
using Bank_app_api.Models.DTO;

namespace Bank_app_api.Services.AccountService
{
    public interface IAccountService
    {
        Task<ServiceResponse<List<GetAccountDto>>> GetAllAccounts();
        Task<ServiceResponse<GetAccountDto>> GetSingleAccount(int id);
        Task<ServiceResponse<List<GetAccountDto>>> AddAccount(AddAccountDto newAccount);
        Task<ServiceResponse<GetAccountDto>> UpdateAccount(UpdateAccountDto updatedAccount);
        Task<ServiceResponse<List<GetAccountDto>>> DeleteAccount(int id);
        Task<ServiceResponse<List<GetAccountDto>>> GetAccountsByOwnerId(int ownerId);


    }
}
