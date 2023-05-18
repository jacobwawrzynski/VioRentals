using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VioRentals.Data;
using VioRentals.Dtos;
using VioRentals.Models;

namespace VioRentals.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin, Employee")]
public class NewRentalsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public NewRentalsController()
    {
        _context = new ApplicationDbContext();
    }


    [HttpPost]
    public IActionResult CreateNewRental(NewRentalDto newRental)
    {
        if (newRental.MovieIds.Count == 0)
            return BadRequest("Movie unavailable.");
        var customer = _context.Customers.SingleOrDefault(
            c => c.Id == newRental.CustomerId);

        if (newRental.CustomerId == null)
            return BadRequest("Customer Id is not valid.");

        var movies = _context.Movies.Where(
            m => newRental.MovieIds.Contains(m.Id)).ToList();

        if (movies.Count() != newRental.MovieIds.Count)
            return BadRequest("One or more MovieIds are invalid.");


        foreach (var movie in movies)
        {
            if (movie.NumberAvailable == 0)
                return BadRequest("Movie is not available.");

            movie.NumberAvailable--;

            var rental = new Rental
            {
                Customer = customer,
                Movie = movie,
                DateRented = DateTime.Now
            };

            _context.Rentals.Add(rental);
        }

        _context.SaveChanges();

        return Ok();
    }


    //get all rentals
    // /api/NewRentals
    [HttpGet]
    public IActionResult GetRentals()
    {
        var rentals = _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Movie)
            .ToList();

        return Ok(rentals);
    }

    //get rental by id
    // /api/NewRentals/1
    [HttpGet("{id}")]
    public IActionResult GetRental(int id)
    {
        var rental = _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.Movie)
            .SingleOrDefault(r => r.Id == id);

        if (rental == null)
            return NotFound();

        return Ok(rental);
    }

    //delete rental by id
    // /api/NewRentals/1
    [HttpDelete("{id}")]
    public IActionResult DeleteRental(int id)
    {
        var rentalInDb = _context.Rentals.SingleOrDefault(r => r.Id == id);

        if (rentalInDb == null)
            return NotFound();

        _context.Rentals.Remove(rentalInDb);
        _context.SaveChanges();

        return Ok();
    }

    //update rental by id
    // /api/NewRentals/1
    [HttpPut("{id}")]
    public IActionResult UpdateRental(int id, Rental rental)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var rentalInDb = _context.Rentals.SingleOrDefault(r => r.Id == id);

        if (rentalInDb == null)
            return NotFound();

        rentalInDb.DateReturned = rental.DateReturned;

        _context.SaveChanges();

        return Ok();
    }
}