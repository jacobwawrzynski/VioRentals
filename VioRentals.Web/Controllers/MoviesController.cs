using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpGet]
        public ViewResult GetCreate() 
        {
            return View("Create");
        }

        [HttpGet]
        public async Task<ActionResult> GetEditAsync(int id)
        {
            var movie = await _movieService.FindByIdAsync(id);

            if (movie is not null)
            {
                return View("Edit", movie);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<MovieEntity>(movieDto);
                await _movieService.SaveMovieAsync(movie);
                return RedirectToAction("Index");
            }

            return RedirectToAction("GetCreateAsync", movieDto);
        }

        [HttpPatch]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([FromForm] MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<MovieEntity>(movieDto);
                await _movieService.UpdateMovieAsync(movie);
                return RedirectToAction("Index");
            }

            return RedirectToAction("GetEditAsync", movieDto);
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailsAsync(int id)
        {
            var movie = await _movieService.FindByIdAsync(id);

            if (movie is not null)
            {
                return View(movie);
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<JsonResult> SearchAsync(string searchTerm)
        {
            var movie = await _movieService.FindByTermAsync(searchTerm);
            var result = movie.Select(m => new
            {
                m.Id,
                m.Name,
                m.ReleaseDate,
                m.DateAdded,
                m.NumberInStock,
                m.NumberAvailable
            }).ToList();
            return Json(result);
        }

        // TO CONSIDER
        // Random
        // ValidationReleaseDate
    }
}
