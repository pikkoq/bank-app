using Bank_app_api.Models;
using Bank_app_api.Models.DTO;
using Bank_app_api.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace Bank_app_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }
        [HttpGet("GetSingleUser{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetSingleUser(int id)
        {
            return Ok(await _userService.GetSingleUser(id));
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> AddUser(AddUserDto newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }
        [HttpPut("UpdateUser")]
        public async Task<ActionResult<ServiceResponse<List<GetUserDto>>>> UpdateUser(UpdateUserDto updatedUser)
        {
            var response = await _userService.UpdateUser(updatedUser);
            if(response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteUser{id}")]
        public async Task<ActionResult<ServiceResponse<GetUserDto>>> DeleteUser(int id)
        {
            var response = await _userService.DeleteUser(id);
            if (response.Data is null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
