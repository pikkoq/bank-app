using Bank_app_api.Models;
using Bank_app_api.Models.DTO;

namespace Bank_app_api.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetAllUsers();
        Task<ServiceResponse<GetUserDto>> GetSingleUser(int id);
        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);
        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);
        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);


    }
}
