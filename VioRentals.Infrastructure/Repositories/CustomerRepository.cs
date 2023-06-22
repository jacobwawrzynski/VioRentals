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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
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
                    updateCustomer.MembershipTypeFK = customer.MembershipTypeFK;

                    // Do not include relations
                    //updateCustomer._Rentals = customer._Rentals;

                    // Do not include relations
                    // Update automatically after changing FK
                    //updateCustomer._MembershipType = customer._MembershipType;
                    
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
    }
}
