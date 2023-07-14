using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VioRentals.Core.DTOs;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService,
            IMapper mapper,
            IGenreService genreService)
        {
            _mapper = mapper;
            _movieService = movieService;
            _genreService = genreService;
        }

        [HttpPost("create")]
        [VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<IActionResult> Create(MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<MovieEntity>(movieDto);
                await _movieService.SaveMovieAsync(movie);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("edit/{id}")]
        [VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _movieService.FindByIdAsync(id);

            if (movie is not null)
            {
                var movieDto = _mapper.Map<MovieDto>(movie);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch("edit")]
        [VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<IActionResult> Edit(MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<MovieEntity>(movieDto);
                await _movieService.UpdateMovieAsync(movie);
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("all")]
        //[VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<IActionResult> GetAll()
        {
            var movies = await _movieService.FindAllAsync();

            // PROBLEM WITH CIRCLE REFERENCE
            var genres = await _genreService.FindAllAsync();
            foreach (var mov in movies)
            {
                mov._Genre = genres
                    .Where(g => g.Id == mov.GenreFK)
                    .First();
            }

            return Ok(movies);
        }
    }
}
