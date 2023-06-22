using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        public Task<MovieEntity?> FindByIdAsync(int id);
        public Task<List<MovieEntity>> FindByTerm(string searchTerm);
        public Task<bool> SaveMovieAsync(MovieEntity movie);
        public Task<IEnumerable<MovieEntity>> FindAllAsync();
        public Task<bool> UpdateMovieAsync(int id, MovieEntity movie);
    }
}
