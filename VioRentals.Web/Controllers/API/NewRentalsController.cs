using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;
using VioRentals.Web.DTOs;

namespace VioRentals.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        private readonly ICustomerService _customerService;
        private readonly IMovieService _movieService;

        public NewRentalsController(
            IRentalService rentalService,
            ICustomerService customerService,
            IMovieService movieService)
        {
            _rentalService = rentalService;
            _customerService = customerService;
            _movieService = movieService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewRental(NewRentalDto newRental)
        {
            if (newRental.MovieIds.Count == 0)
            {
                return BadRequest("Movie unavailable.");
            }

            var customer = await _customerService.FindByIdAsync(newRental.CustomerId);

            if (customer is null)
            {
                return BadRequest("Customer is null.");
            }

            var movies = await _movieService.FindAllAsync();
            movies = movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            if (movies.Count() != newRental.MovieIds.Count)
            {
                return BadRequest("One or more MovieIds are invalid.");
            }

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                {
                    return BadRequest("Movie is not available.");
                }

                movie.NumberAvailable--;

                var rental = new RentalEntity
                {
                    _Customer = customer,
                    _Movie = movie,
                    DateRented = DateTime.Now,
                };

                // BAD APPROACH SINCE EVERY SAVE ALSO INVOKES SAVECHANGES()
                await _rentalService.SaveRentalAsync(rental);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRental(int id)
        {
            var rental = await _rentalService.FindByIdAsync(id);
            rental._Customer = await _customerService.FindByIdAsync(rental.CustomerFK);
            rental._Movie = await _movieService.FindByIdAsync(rental.MovieFK);

            return Ok(rental);
        }

        [HttpGet]
        public async Task<IActionResult> GetRentals()
        {
            var rentals = await _rentalService.FindAllAsync();

            foreach (var ren in rentals)
            {
                ren._Customer = await _customerService.FindByIdAsync(ren.CustomerFK);
                ren._Movie = await _movieService.FindByIdAsync(ren.MovieFK);
            }

            return Ok(rentals);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRental(int id, RentalEntity rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var rentalInDb = await _rentalService.FindByIdAsync(id);

            if (rentalInDb is null)
            {
                return NotFound();
            }

            rentalInDb.DateReturned = rental.DateReturned;

            await _rentalService.UpdateRentalAsync(rentalInDb);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRental(int id)
        {
            var rental = await _rentalService.FindByIdAsync(id);

            if (rental is null)
            {
                return NotFound();
            }

            await _rentalService.DeleteRentalAsync(rental);

            return Ok();
        }
    }
}
