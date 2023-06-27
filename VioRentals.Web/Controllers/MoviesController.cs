using Microsoft.AspNetCore.Mvc;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAsync() 
        {
            return View("MovieForm");
        }

    }
}
