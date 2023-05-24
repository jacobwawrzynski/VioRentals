﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using VioRentals.Infrastructure.Data.Entities;
using VioRentals.Infrastructure.Repositories;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		public static UserDto _user = new UserDto();
		private readonly IMapper _mapper;
		private readonly IUserRepository _userRepository;

		public AuthController(IMapper mapper, IUserRepository userRepository)
		{
			_mapper = mapper;
			_userRepository = userRepository;
		}

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
			if (ModelState.IsValid)
			{
				CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

				_user.Email = register.Email;
				_user.PasswordHash = passwordHash;
				_user.PasswordSalt = passwordSalt;
				_user.Forename = register.Forename;
				_user.Lastname = register.Lastname;

				var createdUser = _mapper.Map<UserEntity>(_user);
				await _userRepository.AddUserAsync(createdUser);

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
