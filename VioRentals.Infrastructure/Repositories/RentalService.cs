using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class RentalService : IRentalService
    {
        private IRepository<RentalEntity> _rentalRepository;

        public RentalService(IRepository<RentalEntity> rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public async Task<IEnumerable<RentalEntity>> FindAllAsync()
        {
            return await _rentalRepository.GetAllAsync();
        }

        public async Task<RentalEntity?> FindByIdAsync(int id)
        {
            return await _rentalRepository.GetAsync(id);
        }
    }
}
