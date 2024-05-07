using AutoMapper;
using Bank_app_api.Data;
using Bank_app_api.Models;
using Bank_app_api.Models.DTO;

namespace Bank_app_api.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private static List<Account> accountList = new List<Account>
        {
            new Account(),
            new Account {AccountId = 1, OwnerId = 2}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AccountService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetAccountDto>>> AddAccount(AddAccountDto newAccount)
        {
            var serviceResponse = new ServiceResponse<List<GetAccountDto>>();
            var account = _mapper.Map<Account>(newAccount);
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            var dbAccounts = await _context.Accounts.ToListAsync();
            serviceResponse.Data = dbAccounts.Select(c => _mapper.Map<GetAccountDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAccountDto>>> DeleteAccount(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetAccountDto>>();

            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountId == id);
                if (account is null)
                {
                    throw new Exception($"Account with Id '{id}' not found.");
                }

                _context.Accounts.Remove(account);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Accounts.Select(c => _mapper.Map<GetAccountDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetAccountDto>>> GetAllAccounts()
        {
            var serviceResponse = new ServiceResponse<List<GetAccountDto>>();
            var dbAccounts = await _context.Accounts.ToListAsync();
            serviceResponse.Data = dbAccounts.Select(c => _mapper.Map<GetAccountDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAccountDto>> GetSingleAccount(int id)
        {
            var serviceResponse = new ServiceResponse<GetAccountDto>();

            var dbAccount = await _context.Accounts.FirstOrDefaultAsync(c => c.OwnerId == id);
            serviceResponse.Data = _mapper.Map<GetAccountDto>(dbAccount);
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetAccountDto>>> GetAccountsByOwnerId(int ownerId)
        {
            var serviceResponse = new ServiceResponse<List<GetAccountDto>>();

            // Pobranie listy kont na podstawie ownerId
            var dbAccounts = await _context.Accounts
                .Where(a => a.OwnerId == ownerId)
                .ToListAsync();

            // Mapowanie i przypisanie listy kont do właściwości Data w ServiceResponse
            serviceResponse.Data = dbAccounts.Select(c => _mapper.Map<GetAccountDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetAccountDto>> UpdateAccount(UpdateAccountDto updatedAccount)
        {
            var serviceResponse = new ServiceResponse<GetAccountDto>();
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountId == updatedAccount.AccountId);
                if(account is null)
                {
                    throw new Exception($"Account with Id '{updatedAccount.AccountId}' not found.");
                }

                account.AccountNumber = updatedAccount.AccountNumber;
                account.Balance = updatedAccount.Balance;
                account.OwnerId = updatedAccount.OwnerId;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetAccountDto>(account);

            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
    }
}
