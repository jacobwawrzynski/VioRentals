using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Web.Controllers
{
    public class RentalsController : Controller
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

        public async Task<ActionResult> RentalList()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var response = await client.GetAsync("Rentals/all");
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var rentals = JsonConvert.DeserializeObject<IEnumerable<RentalEntity>>(responseContent);
                    return View(rentals);
                }
            }
            return BadRequest();

            //var rentals = await _rentalService.FindAllAsync();

            //foreach (var ren in rentals)
            //{
            //    ren._Customer = await _customerService.FindByIdAsync(ren.CustomerFK);
            //    ren._Movie = await _movieService.FindByIdAsync(ren.MovieFK);
            //}

            //return View(rentals);
        }

        [HttpGet]
        public ViewResult New()
        {
            return View();
        }

        public async Task<IActionResult> ReturnRentalAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var rental = await _rentalService.FindByIdAsync(id);
                client.BaseAddress = new Uri("https://localhost:7071/api/");
                var content = JsonContent.Create(rental);
                var response = await client.PatchAsync("Rentals/return", content);
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("Login", "Home");
                }
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("RentalList", "Rentals");
                }
            }
            return BadRequest();

            //var rental = await _rentalService.FindByIdAsync(id);
            //if (rental is null)
            //{
            //    return NotFound();
            //}

            //if (rental.IsReturned)
            //{
            //    return BadRequest("Rental already returned");
            //}

            //rental.IsReturned = true;
            //rental.DateReturned = DateTime.Now;

            //var movie = await _movieService.FindByIdAsync(rental.MovieFK);
            //movie.NumberAvailable++;

            //await _rentalService.UpdateRentalAsync(rental);
            //await _movieService.UpdateMovieAsync(movie);

            //return Ok();
        }

        [HttpGet]
        public async Task<JsonResult> SearchAsync(string searchTerm)
        {
            var customers = await _customerService.FindByTermAsync(searchTerm);
            var result = customers.Select(c => new
            {
                c.Id,
                c.Forename,
                c.Surname
            });
            return Json(result);
        }
    }
}
