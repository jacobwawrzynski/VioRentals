using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VioRentals.Core.DTOs;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

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

        public ViewResult GetCreate() 
        {
            return View("Create");
        }

        public async Task<ActionResult> GetEditAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.GetAsync("Movies/edit/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return View("Edit", response.Content);
                }
            }
            return NotFound();
        }

        public async Task<ActionResult> GetDetailsAsync(int id)
        {
            var movie = await _movieService.FindByIdAsync(id);

            if (movie is not null)
            {
                return View("Details", movie);
            }
            return NotFound();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([FromForm] MovieDto movieDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var content = JsonContent.Create(movieDto);
                var response = await client.PostAsync("Movies/create", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest();
        }

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync([FromForm] MovieDto movieDto)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var content = JsonContent.Create(movieDto);
                var response = await client.PatchAsync("Movies/edit", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.DeleteAsync("Customers/delete/{id}");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return BadRequest();
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
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
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

            //pass to view
            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.GetAsync("Movies/all");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var movies = JsonConvert.DeserializeObject<IEnumerable<MovieEntity>>(responseContent);
                    return View(movies);
                }
            }
            return BadRequest();
        }
    }
}
