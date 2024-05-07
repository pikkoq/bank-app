using AutoMapper;
using Bank_app_api.Data;
using Bank_app_api.Models;
using Bank_app_api.Models.DTO;

namespace Bank_app_api.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> userList = new List<User>
        {
            new User(),
            new User {UserId = 1, Name = "Paweł"}
        };
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var user = _mapper.Map<User>(newUser);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(c => c.UserId == id);
                if (user is null)
                {
                    throw new Exception($"User with Id '{id}' not found.");
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Users.Select(c => _mapper.Map<GetUserDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            var serviceResponse = new ServiceResponse<List<GetUserDto>>();
            var dbUsers = await _context.Users.ToListAsync();
            serviceResponse.Data = dbUsers.Select(c => _mapper.Map<GetUserDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetSingleUser(int id)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            var dbUser = await _context.Users.FirstOrDefaultAsync(c => c.UserId == id);
            serviceResponse.Data = _mapper.Map<GetUserDto>(dbUser);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            var serviceResponse = new ServiceResponse<GetUserDto>();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(c => c.UserId == updatedUser.UserId);
                if(user is null)
                {
                    throw new Exception($"User with Id '{updatedUser.UserId}' not found.");
                }

                user.Name = updatedUser.Name;
                user.Surname = updatedUser.Surname;
                user.Login = updatedUser.Login;
                user.Password = updatedUser.Password;
                user.UserRole = updatedUser.UserRole;

                await _context.SaveChangesAsync();
                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
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
