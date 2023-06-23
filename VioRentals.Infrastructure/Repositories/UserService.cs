using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
	}
}
