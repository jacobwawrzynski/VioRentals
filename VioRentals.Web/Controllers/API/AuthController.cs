using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Unicode;
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
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

		public AuthController(IUserService userService, IMapper mapper, IConfiguration configuration)
		{
            _userService = userService;
            _mapper = mapper;
            _configuration = configuration;
        }

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromForm] LoginDto login)
		{
			if (ModelState.IsValid)
			{	
                var user = await _userService.FindByEmailAsync(login.Email);
				
				if (user is not null)
				{
					if (VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
					{
						//string token = CreateToken(user);
						//HttpContext.Session.SetString(token, user.Id.ToString());
						return RedirectToAction("Index", "Customers");
					}
				}
			}
			
			return BadRequest();
		}

        /// <summary>
        /// Creates JWT with HMACSHA512 signature
        /// </summary>
        /// <param name="user">User model</param>
        /// <returns>JSON Web Token</returns>
        //private string CreateToken(UserEntity user)
        //{
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, user.Email),
        //        new Claim("UserId", $"{user.Id}", ClaimValueTypes.Integer32)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
        //        _configuration.GetSection("AppSettings:Token").Value));

        //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //    var token = new JwtSecurityToken(
        //        claims: claims,
        //        expires: DateTime.Now.AddDays(1),
        //        signingCredentials: creds);

        //    //var identity = new ClaimsIdentity(claims, "Custom");
        //    //HttpContext.User = new ClaimsPrincipal(identity);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

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
