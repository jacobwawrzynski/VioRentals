using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface IGenreService
    {
        public Task<IEnumerable<GenreEntity>> FindAllAsync();
        public Task<GenreEntity> FindByIdAsync(int id);
    }
}
