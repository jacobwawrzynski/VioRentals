﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<IEnumerable<CustomerEntity>> FindAllAsync();
        public Task<CustomerEntity?> FindByIdAsync(int id);
        public Task<List<CustomerEntity>> FindByTermAsync(string searchTerm);
        public Task<bool> SaveCustomerAsync(CustomerEntity customer);
        public Task<bool> UpdateCusotmerAsync(int id, CustomerEntity customer);
        public Task<IEnumerable<MembershipTypeEntity>> GetAllMembershipTypesAsync();
    }
}
