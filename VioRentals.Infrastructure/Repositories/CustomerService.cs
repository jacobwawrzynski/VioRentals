using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class CustomerService : ICustomerService
    {
        private IRepository<CustomerEntity> _customerRepository;
        private IRepository<MembershipDetailsEntity> _membershipRepository;

        public CustomerService(IRepository<CustomerEntity> customerRepository,
                               IRepository<MembershipDetailsEntity> membershipRepository)
        {
            _customerRepository = customerRepository;
            _membershipRepository = membershipRepository;
        }

        public async Task<IEnumerable<CustomerEntity>> FindAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<CustomerEntity?> FindByIdAsync(int id)
        {
            return await _customerRepository.GetAsync(id);
        }

        public async Task<bool> SaveCustomerAsync(CustomerEntity customer)
        {
            try
            {   
                await _customerRepository.CreateAsync(customer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCustomerAsync(CustomerEntity customer)
        {
            try
            {
                await _customerRepository.UpdateAsync(customer);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CustomerEntity>> FindByTermAsync(string searchTerm)
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers
                .Where(c => c.Forename.Contains(searchTerm) || c.Surname.Contains(searchTerm))
                .ToList();
        }

        //public async Task<bool> AssignMembershipAsync(int membershipId, CustomerEntity customerEntity)
        //{
        //    var membership = await _membershipRepository.GetAsync(membershipId);
        //    membership._Customers.
        //    customerEntity._MembershipDetails = membership;

        //}

        public async Task<int> CountCustomersAsync()
        {
            return await _customerRepository.CountAsync();
        }
    }
}
