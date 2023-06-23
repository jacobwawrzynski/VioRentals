using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface IRentalService
    {
        public Task<IEnumerable<RentalEntity>> FindAllAsync();
        public Task<RentalEntity?> FindByIdAsync(int id);
    }
}
