using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Data.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerEntity>> FindAllAsync()
        {
            return await _context.Customers
                .Include(c => c._MembershipDetails)
                .OrderBy(c => c.Surname)
                .ToListAsync();
        }

        public async Task<CustomerEntity?> FindByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<bool> SaveCustomerAsync(CustomerEntity customer)
        {
            try
            {
                if (customer is not null)
                {
                    await _context.Customers.AddAsync(customer);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCusotmerAsync(int id, CustomerEntity customer)
        {
            var updateCustomer = await FindByIdAsync(id);
            try
            {
                if (updateCustomer is not null
                && customer is not null)
                {
                    updateCustomer.Forename = customer.Forename;
                    updateCustomer.Surname = customer.Surname;
                    updateCustomer.DateOfBirth = customer.DateOfBirth;
                    updateCustomer.IsSubscribingToNewsletter = customer.IsSubscribingToNewsletter;
                    updateCustomer.MembershipDetailsFK = customer.MembershipDetailsFK;

                    // Do not include relations
                    //updateCustomer._Rentals = customer._Rentals;

                    // Do not include relations
                    // Update automatically after changing FK
                    //updateCustomer._MembershipDetails = customer._MembershipDetails;
                    
                    _context.Customers.Update(updateCustomer);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<List<CustomerEntity>> FindByTermAsync(string searchTerm)
        {
            return await _context.Customers
                .Where(c => c.Forename.Contains(searchTerm) || c.Surname.Contains(searchTerm))
                .ToListAsync();
        }

        //public async Task<IEnumerable<MembershipTypeEntity>> GetAllMembershipTypesAsync()
        //{
        //    return await _context.MembershipTypes.ToListAsync();
        //}
    }
}
