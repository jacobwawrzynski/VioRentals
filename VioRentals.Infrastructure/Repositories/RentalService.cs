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
    public class RentalService : IRentalService
    {
        private readonly AppDbContext _context;
        public RentalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RentalEntity>> FindAllAsync()
        {
            return await _context.Rentals
                .Include(r => r._Movie)
                .Include(r => r._Customer)
                .ToListAsync();
        }

        public async Task<RentalEntity?> FindByIdAsync(int id)
        {
            return await _context.Rentals.FindAsync(id);
        }
    }
}
