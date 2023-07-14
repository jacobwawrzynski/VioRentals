using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly IMovieService _movieService;
        private readonly ICustomerService _customerService;

        public RentalsController(
            IRentalService rentalService,
            IMovieService movieService,
            ICustomerService customerService)
        {
            _rentalService = rentalService;
            _movieService = movieService;
            _customerService = customerService;
        }

        [HttpGet("all")]
        //[VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<IActionResult> Index()
        {
            var rentals = await _rentalService.FindAllAsync();

            foreach (var ren in rentals)
            {
                ren._Customer = await _customerService.FindByIdAsync(ren.CustomerFK);
                ren._Movie = await _movieService.FindByIdAsync(ren.MovieFK);
            }

            return Ok(rentals);
        }

        [HttpPost("return")]
        //[VioRentals.AuthAPI.Attributes.Authorize]
        public async Task<IActionResult> ReturnRentalAsync(RentalEntity rental)
        {
            if (rental is null)
            {
                return NotFound();
            }

            if (rental.IsReturned)
            {
                return BadRequest("Rental already returned");
            }

            rental.IsReturned = true;
            rental.DateReturned = DateTime.Now;

            var movie = await _movieService.FindByIdAsync(rental.MovieFK);
            movie.NumberAvailable++;

            await _rentalService.UpdateRentalAsync(rental);
            await _movieService.UpdateMovieAsync(movie);

            return Ok();
        }
    }
}
