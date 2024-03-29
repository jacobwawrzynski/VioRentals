﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface IRentalService
    {
        public Task<IEnumerable<RentalEntity>> FindAllAsync();
        public Task<RentalEntity?> FindByIdAsync(int id);
        public Task<bool> UpdateRentalAsync(RentalEntity rental);
        public Task<bool> DeleteRentalAsync(RentalEntity rental);
        public Task<bool> SaveRentalAsync(RentalEntity rental);
    }
}
