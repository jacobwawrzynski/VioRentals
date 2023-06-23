using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers.API
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public static UserDto _userDto = new UserDto();
		private readonly IMapper _mapper;
		private readonly IUserService _userService;

		public AuthController(IUserService userService, IMapper mapper)
		{
            _userService = userService;
            _mapper = mapper;
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login([FromForm] LoginDto login)
		{
			if (ModelState.IsValid)
			{
                var user = await _userService.FindByEmailAsync(login.Email);
				
				if (user is not null)
				{
					if (VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
					{
						return Ok(user);
					}
				}
			}
			
			return BadRequest();
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register([FromForm] RegisterDto register)
		{
			if (ModelState.IsValid)
			{
				CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

				_userDto.Email = register.Email;
				_userDto.PasswordHash = passwordHash;
				_userDto.PasswordSalt = passwordSalt;
				_userDto.Forename = register.Forename;
				_userDto.Lastname = register.Lastname;

				var mappedUser = _mapper.Map<UserEntity>(_userDto);
				await _userService.SaveUserAsync(mappedUser);

				return Ok("User created successfully");
			}

			return BadRequest(ModelState);
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hash = new HMACSHA512())
			{
				passwordSalt = hash.Key;
				passwordHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
			}
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hash = new HMACSHA512(passwordSalt))
			{
				var computedHash = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
				return computedHash.SequenceEqual(passwordHash);
			}
		}
	}
}
