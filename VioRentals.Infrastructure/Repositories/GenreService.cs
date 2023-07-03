using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<GenreEntity> _genreRepository;

        public GenreService(IRepository<GenreEntity> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreEntity>> FindAllAsync()
        {
            return await _genreRepository.GetAllAsync();
        }

        public async Task<GenreEntity> FindByIdAsync(int id)
        {
            return await _genreRepository.GetAsync(id);
        }
    }
}
