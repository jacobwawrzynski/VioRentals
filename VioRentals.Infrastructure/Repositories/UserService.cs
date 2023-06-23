using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class UserService : IUserService
	{
		private readonly AppDbContext _context;
		public UserService(AppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> SaveUserAsync(UserEntity user)
		{
			try
			{
				if (user is not null)
				{
					await _context.Users.AddAsync(user);
					await _context.SaveChangesAsync();
					return true;
				}

				return false;
			}
			catch (Exception)
			{
				throw;
			}
		}

		public async Task<UserEntity?> FindByIdAsync(int id)
		{
			return await _context.Users.FindAsync(id);
		}

		public async Task<UserEntity?> FindByEmailAsync(string email)
		{
            return await _context.Users
				.Where(u => u.Email == email)
				.FirstOrDefaultAsync();
			
		}
	}
}
