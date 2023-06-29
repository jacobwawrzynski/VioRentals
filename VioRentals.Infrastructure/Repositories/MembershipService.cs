using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
	public class MembershipService : IMembershipService
	{
		private readonly IRepository<MembershipDetailsEntity> _membershipRepository;

        public MembershipService(IRepository<MembershipDetailsEntity> membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        public async Task<IEnumerable<MembershipDetailsEntity>> FindAllAsync()
		{
			return await _membershipRepository.GetAllAsync();
		}

		public async Task<MembershipDetailsEntity?> FindByIdAsync(int id)
		{
			return await _membershipRepository.GetAsync(id);
		}
	}
}
