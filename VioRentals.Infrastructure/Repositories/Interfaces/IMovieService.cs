using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Infrastructure.Repositories.Interfaces
{
    public interface IMovieService
    {
        public Task<MovieEntity?> FindByIdAsync(int id);
        public Task<List<MovieEntity>> FindByTermAsync(string searchTerm);
        public Task<bool> SaveMovieAsync(MovieEntity movie);
        public Task<IEnumerable<MovieEntity>> FindAllAsync();
        public Task<bool> UpdateMovieAsync(MovieEntity movie);
        public Task<bool> DeleteMovieAsync(MovieEntity movie);
        public Task<int> CountMoviesAsync();
    }
}
