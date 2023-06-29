using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
	public interface IMembershipService
	{
		public Task<IEnumerable<MembershipDetailsEntity>> FindAllAsync();
		public Task<MembershipDetailsEntity?> FindByIdAsync(int id);
	}
}
