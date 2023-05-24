using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Infrastructure.Repositories
{
	public interface IUserRepository
	{
		public Task<bool> AddUserAsync(UserEntity user);
	}
}
