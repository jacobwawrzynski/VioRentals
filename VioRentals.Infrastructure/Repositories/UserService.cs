using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.DTOs;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class UserService : IUserService
	{
		private IRepository<UserEntity> _userRepository;
		

		public UserService(IRepository<UserEntity> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<bool> SaveUserAsync(UserEntity user)
		{
			try
			{
                await _userRepository.CreateAsync(user);
				return true;
            }
            catch (Exception)
			{
				return false;
			}
		}

		public async Task<UserEntity?> FindByIdAsync(int id)
		{
			return await _userRepository.GetAsync(id);
		}

		public async Task<UserEntity?> FindByEmailAsync(string email)
		{
            var users = await _userRepository.GetAllAsync();
			return users
				.Where(u => u.Email == email)
				.FirstOrDefault();
		}

        public async Task<AuthenticateUserDto> AuthenticateAsync(LoginDto loginDto)
        {
			var user = await FindByEmailAsync(loginDto.Email);
			if (user is null || !VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
			{
				throw new Exception("Username or password incorrect");
			}
        }

        public async Task<IEnumerable<UserEntity>> FindAllAsync()
        {
			return await _userRepository.GetAllAsync();
        }

        public async Task<bool> UpdateUserAsync(UserEntity user)
        {
			try
			{
				await _userRepository.UpdateAsync(user);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }

        public async Task<bool> DeleteUserAsync(UserEntity user)
        {
			try
			{
				await _userRepository.DeleteAsync(user);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
        }

        public async Task<bool> RegisterAsync(RegisterDto registerDto)
        {
			var users = await _userRepository.GetAllAsync();
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

			await _userRepository.CreateAsync(user);
			return true;
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
