using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerService
    {
        public Task<IEnumerable<CustomerEntity>> FindAllAsync();
        public Task<CustomerEntity?> FindByIdAsync(int id);
        public Task<List<CustomerEntity>> FindByTermAsync(string searchTerm);
        public Task<bool> SaveCustomerAsync(CustomerEntity customer);
        public Task<bool> UpdateCustomerAsync(CustomerEntity customer);
        //public Task<MembershipDetailsEntity> FindMembershipAsync(int id);
        public Task<int> CountCustomersAsync();
    }
}
