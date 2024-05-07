using AutoMapper;
using Bank_app_api.Models;
using Bank_app_api.Models.DTO;

namespace Bank_app_api
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetUserDto>();
            CreateMap<Account, GetAccountDto>();
            CreateMap<Transaction, GetTransactionDto>();
            CreateMap<AddAccountDto, Account>();
            CreateMap<AddTransactionDto, Transaction>();
            CreateMap<AddUserDto, User>();
            CreateMap<LoginDto, User>();
        }
    }
}
