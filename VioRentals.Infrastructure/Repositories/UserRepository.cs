using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Infrastructure.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly AppDbContext _context;

		public UserRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<bool> AddUserAsync(UserEntity user)
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

		public async Task<UserEntity?> FindByAsync(int id)
		{
			var user = await _context.Users.FindAsync(id);
			
			if (user is not null) 
			{
				return user;
			}
			return null;
		}
	}
}
