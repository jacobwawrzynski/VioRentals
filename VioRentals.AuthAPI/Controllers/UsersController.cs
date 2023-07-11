using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using VioRentals.Core.DTOs;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories;
using VioRentals.Infrastructure.Repositories.Interfaces;
using System.Net.Http.Headers;

namespace VioRentals.AuthAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private IJwtUtils _jwtUtils;

        public UsersController(
            IUserService userService,
            IMapper mapper,
            IJwtUtils jwtUtils)
        {
            _userService = userService;
            _mapper = mapper;
            _jwtUtils = jwtUtils;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _userService.FindByEmailAsync(loginDto.Email);
            if (user is null || !VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Incorrect Email or Password");
            }

            var response = _mapper.Map<AuthenticateUserDto>(user);
            response.Token = _jwtUtils.GenerateToken(user);

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization =
            //    new AuthenticationHeaderValue("Bearer", response.Token);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var users = await _userService.FindAllAsync();
            if (users.Any(x => x.Email == registerDto.Email))
            {
                throw new Exception("Email already in use.");
            }

            CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new UserEntity
            {
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Forename = registerDto.Forename,
                Lastname = registerDto.Lastname
            };

            await _userService.SaveUserAsync(user);
            return Ok(new { message = "Registration successful" });
        }

        [VioRentals.AuthAPI.Attributes.Authorize]
        [HttpGet("all")]
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

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512(passwordSalt))
            {
                var computedHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hash = new HMACSHA512())
            {
                passwordSalt = hash.Key;
                passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
