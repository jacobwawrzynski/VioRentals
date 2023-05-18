using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VioRentals.Data;
using VioRentals.Models;
using VioRentals.ViewModels;

namespace VioRentals.Controllers;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _context;
    private bool HasErrors = false;

    public MoviesController(ApplicationDbContext context)
    {
        _context = context;
    }

    public ViewResult Index(int page = 1, int pageSize = 10)
    {
        var totalPages = (int)Math.Ceiling((double)_context.Movies.Count() / pageSize);
        //check if user enters value higher than totalpages and set the value to the hightes pagenumber availabe
        if (page > totalPages)
        {
            page = totalPages;
            Response.Redirect("/Movies/Index?page=" + page + "&pageSize=" + pageSize);
        }
        else if (page < 1)
        {
            page = 1;
            Response.Redirect("/Movies/Index?page=" + page + "&pageSize=" + pageSize);
        }

        if (pageSize < 1)
        {
            pageSize = 10;
            page = 1;
            Response.Redirect("/Movies/Index?page=" + page + "&pageSize=" + pageSize);
        }
        else if (pageSize > 100)
        {
            pageSize = 100;
            Response.Redirect("/Movies/Index?page=" + page + "&pageSize=" + pageSize);
        }

        var movies = _context.Movies
            .Include(m => m.Genre)
            .OrderBy(m => m.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        //pass to view
        ViewBag.TotalPages = totalPages;
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;

        return View(movies);
    }

    public JsonResult Search(string searchTerm)
    {
        var result = _context.Movies.Where(m => m.Name.Contains(searchTerm) || m.Genre.Name.Contains(searchTerm))
            .Select(m => new
            {
                m.Id,
                m.Name,
                m.Genre,
                m.ReleaseDate,
                m.DateAdded,
                m.NumberInStock,
                m.NumberAvailable
            }).ToList();
        return Json(result);
    }

    [Authorize(Roles = "Admin, Employee")]
    public ViewResult New()
    {
        var genres = _context.Genres.ToList();

        var viewModel = new MovieFormViewModel
        {
            Genres = genres
        };

        return View("MovieForm", viewModel);
    }

    [Authorize(Roles = "Admin, Employee")]
    public ActionResult Edit(int id)
    {
        var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

        if (movie == null)
            return NotFound();

        var viewModel = new MovieFormViewModel(movie)
        {
            Genres = _context.Genres.ToList()
        };

        return View("MovieForm", viewModel);
    }


    public ActionResult Details(int id)
    {
        var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return View(movie);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin, Employee")]
    public ActionResult Save(Movie movie)
    {
        if (!ModelState.IsValid)
        {
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _context.Genres.ToList(),
                //show the error message
                HasErrors = true
            };
            return View("MovieForm", viewModel);
        }

        if (movie.Id == 0)
        {
            movie.DateAdded = DateTime.Now;
            _context.Movies.Add(movie);
        }
        else
        {
            var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);
            movieInDb.Name = movie.Name;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.NumberInStock = movie.NumberInStock;
            movieInDb.ReleaseDate = movie.ReleaseDate;
        }

        _context.SaveChanges();

        return RedirectToAction("Index", "Movies");
    }

    public ActionResult Random()
    {
        var movie = new Movie { Name = "Shrek!" };
        var customers = new List<Customer>
        {
            new() { Name = "Customer 1" },
            new() { Name = "Customer 2" }
        };

        var viewModel = new RandomMovieViewModel
        {
            Movie = movie,
            Customers = customers
        };

        return View(viewModel);
    }

    public JsonResult ValidateReleaseDate(DateTime? releaseDate)
    {
        if (releaseDate.HasValue && releaseDate.Value > DateTime.Today)
            return Json("Release Date cannot be in the future.");

        return Json(true);
    }
}