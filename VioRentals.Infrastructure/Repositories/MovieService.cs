using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class MovieService : IMovieService
    {
        private IRepository<MovieEntity> _movieRepository;

        public MovieService(IRepository<MovieEntity> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieEntity>> FindAllAsync()
        {
            return await _movieRepository.GetAllAsync();    
        }

        public async Task<MovieEntity?> FindByIdAsync(int id)
        {
            return await _movieRepository.GetAsync(id);
        } 

        public async Task<List<MovieEntity>> FindByTermAsync(string searchTerm)
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies
                .Where(m => m.Name.Contains(searchTerm) || m._Genre.Name.Contains(searchTerm))
                .ToList();
        }

        public async Task<bool> SaveMovieAsync(MovieEntity movie)
        {
            try
            {
                await _movieRepository.CreateAsync(movie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMovieAsync(MovieEntity movie)
        {
            try
            {
                await _movieRepository.UpdateAsync(movie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
