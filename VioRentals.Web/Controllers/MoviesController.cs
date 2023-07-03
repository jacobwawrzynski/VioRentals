using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public MoviesController(IMovieService movieService, IGenreService genreService, IMapper mapper)
        {
            _movieService = movieService;
            _genreService = genreService;
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
                var movieDto = _mapper.Map<MovieDto>(movie);
                return View("Edit", movieDto);
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailsAsync(int id)
        {
            var movie = await _movieService.FindByIdAsync(id);

            if (movie is not null)
            {
                return View("Details", movie);
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

        [HttpPost]
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

        [HttpPost]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var movie = await _movieService.FindByIdAsync(id);
            await _movieService.DeleteMovieAsync(movie);
            return RedirectToAction("Index");
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

        [HttpGet]
        public async Task<ViewResult> Index(int page = 1, int pageSize = 10)
        {
            var totalPages = (int)Math.Ceiling((double)await _movieService.CountMoviesAsync() / pageSize);
            //check if user enters value higher than totalpages and set the value to the hightes pagenumber availabe
            if (page > totalPages)
            {
                RedirectToAction("Index", new { page = totalPages, pageSize });
            }
            else if (page < 1)
            {
                RedirectToAction("Index", new { page = 1, pageSize });
            }

            if (pageSize < 1)
            {
                RedirectToAction("Index", new { page = 1, pageSize = 10 });
            }
            else if (pageSize > 100)
            {
                RedirectToAction("Index", new { page, pageSize = 100 });
            }

            // FIND BETTER SOLUTION (THIS WORKS BUT UGLY)
            var movies = await _movieService.FindAllAsync();
            var genres = await _genreService.FindAllAsync();

            foreach (var mov in movies)
            {
                mov._Genre = genres
                    .Where(g => g.Id == mov.GenreFK)
                    .First();
            }
            //pass to view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(movies);
        }
    }
}
