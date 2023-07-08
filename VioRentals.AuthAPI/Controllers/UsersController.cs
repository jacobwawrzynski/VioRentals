using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VioRentals.Core.DTOs;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(LoginDto model)
        {
            var response = await _userService.AuthenticateAsync(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            await _userService.RegisterAsync(model);
            return Ok(new { message = "Registration successful" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.FindAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            return Ok(user);
        }

        // TO DO

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, RegisterDto model)
        //{
        //    var user = await _userService.FindByIdAsync(id);
        //    if (user is null)
        //    {
        //        return BadRequest(new { message = "User does not exist"});
        //    }
        //    await _userService.UpdateUserAsync(user);
        //    return Ok(new { message = "User updated successfully" });
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.FindByIdAsync(id);
            if (user is null)
            {
                return BadRequest(new { message = "User does not exist" });
            }
            await _userService.DeleteUserAsync(user);
            return Ok(new { message = "User deleted successfully" });
        }
    }
}
