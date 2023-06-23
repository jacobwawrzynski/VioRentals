﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Data.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;
        public MovieService(AppDbContext context)
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

        public async Task<List<MovieEntity>> FindByTermAsync(string searchTerm)
        {
            return await _context.Movies
                .Where(m => m.Name.Contains(searchTerm) || m._Genre.Name.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<bool> SaveMovieAsync(MovieEntity movie)
        {
            try
            {
                if (movie is not null)
                {
                    await _context.AddAsync(movie);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateMovieAsync(int id, MovieEntity movie)
        {
            var updateMovie = await FindByIdAsync(id);
            try
            {
                if (updateMovie is not null
                && movie is not null)
                {
                    updateMovie.DateAdded = movie.DateAdded;
                    updateMovie.ReleaseDate = movie.ReleaseDate;
                    updateMovie.NumberInStock = movie.NumberInStock;
                    updateMovie.NumberAvailable = movie.NumberAvailable;
                    updateMovie.GenreFK = movie.GenreFK;

                    // Do not include relations
                    // Update automatically after changing FK
                    //updateMovie._Genre = movie._Genre;

                    _context.Movies.Update(updateMovie);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }
    }
}