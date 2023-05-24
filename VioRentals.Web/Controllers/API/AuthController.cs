using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public static UserDto _user = new UserDto();

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login(LoginDto user)
		{
			if (!_user.Email.Equals(user.Email))
			{
				return BadRequest("User not found.");
			}

			if (!VerifyPasswordHash(user.Password, _user.PasswordHash, _user.PasswordSalt))
			{
				return BadRequest("Wrong password");
			}

			return Ok(user.Email);
		}

		[HttpPost("register")]
		public async Task<ActionResult<UserDto>> Register(RegisterDto register)
		{
			CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

			_user.Email = register.Email;
			_user.PasswordHash = passwordHash;
			_user.PasswordSalt = passwordSalt;
			_user.Forename = register.Forename;
			_user.Lastname = register.Lastname;

			return Ok(_user);
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
