using AutoMapper;
using Bank_app_api.Data;
using Bank_app_api.Models;
using Bank_app_api.Models.DTO;

namespace Bank_app_api.Services.TransactionService
{
    public class TransactionService : ITransactionService
    {
        private static List<Transaction> transactionList = new List<Transaction>
        {
            new Transaction(),
            new Transaction {TransactionId = 1, Ammount = 100}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public TransactionService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetTransactionDto>>> AddTransaction(AddTransactionDto newTransaction)
        {
            var serviceResponse = new ServiceResponse<List<GetTransactionDto>>();
            var transaction = _mapper.Map<Transaction>(newTransaction);
            transaction.Date = DateTime.UtcNow;

            // Odjęcie kwoty transakcji od konta, z którego dokonano przelewu
            var fromAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == newTransaction.FromAccountNumber);
            if (fromAccount == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Konto źródłowe nie istnieje.";
                return serviceResponse;
            }
            fromAccount.Balance -= newTransaction.Ammount;

            // Dodanie kwoty transakcji do konta, na które dokonano przelewu
            var toAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == newTransaction.ToAccountNumber);
            if (toAccount == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Konto docelowe nie istnieje.";
                return serviceResponse;
            }
            toAccount.Balance += newTransaction.Ammount;

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            // Pobranie zaktualizowanych transakcji
            var dbTransactions = await _context.Transactions.ToListAsync();
            serviceResponse.Data = dbTransactions.Select(c => _mapper.Map<GetTransactionDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTransactionDto>>> AddTransactionAdmin(AddTransactionDto newTransaction)
        {
            var serviceResponse = new ServiceResponse<List<GetTransactionDto>>();
            var transaction = _mapper.Map<Transaction>(newTransaction);
            transaction.Date = DateTime.UtcNow;
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
            var dbTransactions = await _context.Transactions.ToListAsync();
            serviceResponse.Data = dbTransactions.Select(c => _mapper.Map<GetTransactionDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTransactionDto>>> DeleteTransaction(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetTransactionDto>>();

            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(c => c.TransactionId == id);
                if (transaction is null)
                {
                    throw new Exception($"Transaction with Id '{id}' not found.");
                }

                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
                serviceResponse.Data = await _context.Transactions.Select(c => _mapper.Map<GetTransactionDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTransactionDto>>> GetAllTransactions()
        {
            var serviceResponse = new ServiceResponse<List<GetTransactionDto>>();
            var dbTransactions = await _context.Transactions.ToListAsync();
            serviceResponse.Data = dbTransactions.Select(c => _mapper.Map<GetTransactionDto>(c)).ToList();
            return serviceResponse;
        }
        
        public async Task<ServiceResponse<GetTransactionDto>> GetSingleTransaction(int id)
        {
            var serviceResponse = new ServiceResponse<GetTransactionDto>();

            var dbTransaction = await _context.Transactions.FirstOrDefaultAsync(c => c.TransactionId == id);
            serviceResponse.Data = _mapper.Map<GetTransactionDto>(dbTransaction);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTransactionDto>>> GetTransactionsFromByAccount(string accountFrom)
        {
            var serviceRespone = new ServiceResponse<List<GetTransactionDto>>();

            var dbTransaction = await _context.Transactions
                .Where(a => a.FromAccountNumber == accountFrom)
                .ToListAsync();

            serviceRespone.Data = dbTransaction.Select(c => _mapper.Map<GetTransactionDto>(c)).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<List<GetTransactionDto>>> GetTransactionsToByAccount(string accountTo)
        {
            var serviceRespone = new ServiceResponse<List<GetTransactionDto>>();

            var dbTransaction = await _context.Transactions
                .Where(a => a.ToAccountNumber == accountTo)
                .ToListAsync();

            serviceRespone.Data = dbTransaction.Select(c => _mapper.Map<GetTransactionDto>(c)).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<GetTransactionDto>> UpdateTransaction(UpdateTransactionDto updatedTransaction)
        {
            var serviceResponse = new ServiceResponse<GetTransactionDto>();
            try
            {
                var transaction = await _context.Transactions.FirstOrDefaultAsync(c => c.TransactionId == updatedTransaction.TransactionId);
                if(transaction is null)
                {
                    throw new Exception($"Transaction with Id '{updatedTransaction.TransactionId}' not found.");
                }
                transaction.Ammount = updatedTransaction.Ammount;
                transaction.FromAccountNumber = updatedTransaction.FromAccountNumber;
                transaction.ToAccountNumber = updatedTransaction.ToAccountNumber;
                transaction.AccountBalanceAfterTransaction = updatedTransaction.AccountBalanceAfterTransaction;
                transaction.Title = updatedTransaction.Title;
                transaction.Description = updatedTransaction.Description;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetTransactionDto>(transaction);

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
