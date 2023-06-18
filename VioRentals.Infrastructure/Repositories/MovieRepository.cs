using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Data.Entities;

namespace VioRentals.Infrastructure.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieEntity>> FindAllAsync()
        {
            return await _context.Movies
                .Include(m => m._Genre)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<MovieEntity?> FindByIdAsync(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<List<MovieEntity>> FindByTerm(string searchTerm)
        {
            return await _context.Movies
                .Where(m => m.Name.Contains(searchTerm) || m._Genre.Name.Contains(searchTerm))
                .ToListAsync();
        }

        public Task<bool> SaveMovieAsync(MovieEntity movie)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
