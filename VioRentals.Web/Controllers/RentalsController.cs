//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using VioRentals.Data;

//namespace VioRentals.Controllers;

//[Authorize(Roles = "Admin, Employee")]
//public class RentalsController : Controller
//{
//    private readonly ApplicationDbContext _context;

//    public RentalsController(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public IActionResult RentalList()
//    {
//        var rentals = _context.Rentals
//            .Include(r => r.Movie)
//            .Include(r => r.Customer)
//            .ToList();

//        return View(rentals);
//    }


//    public IActionResult New()
//    {
//        return View();
//    }

//    [HttpPost]
//    public IActionResult ReturnRental(int rentalId)
//    {
//        var rental = _context.Rentals.SingleOrDefault(r => r.Id == rentalId);
//        if (rental == null)
//            return NotFound();

//        if (rental.Returned)
//            return BadRequest("Rental already returned");

//        rental.Returned = true;
//        rental.DateReturned = DateTime.Now;

//        var movie = _context.Movies.SingleOrDefault(m => m.Id == rental.MovieId);
//        movie.NumberAvailable++;

//        _context.SaveChanges();

//        return Ok();
//    }


//    public JsonResult Search(string searchTerm)
//    {
//        var result = _context.Customers.Where(c => c.Name.Contains(searchTerm) || c.Surname.Contains(searchTerm))
//            .Select(c => new
//            {
//                c.Id,
//                c.Name,
//                c.Surname
//            }).ToList();
//        return Json(result);
//    }
//}