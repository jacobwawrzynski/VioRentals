using AutoMapper;
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
public class MoviesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public MoviesController(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Movies
    [HttpGet]
    public async Task<IEnumerable<MovieDto>> GetMovies()
    {
        return _mapper.Map<IEnumerable<MovieDto>>(_context.Movies.Include(c => c.Genre)
            .Where(m => m.NumberAvailable > 0));
    }

    // GET: api/Movies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MovieDto>> GetMovie(int id)
    {
        var movie = _context.Movies.SingleOrDefault(c => c.Id == id);

        if (movie == null)
            return NotFound();

        return Ok(_mapper.Map<Movie, MovieDto>(movie));
    }


    // POST: api/Movies
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<MovieDto>> CreateMovie(MovieDto movieDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var movie = _mapper.Map<MovieDto, Movie>(movieDto);
        _context.Movies.Add(movie);
        _context.SaveChanges();

        movieDto.Id = movie.Id;
        return CreatedAtAction("GetMovie", new { id = movieDto.Id }, movieDto);
    }

    // PUT: api/Movies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovie(int id, MovieDto movieDto)
    {
        if (!ModelState.IsValid)
            //return badrequest
            return BadRequest();
        //BadRequest(ModelState);
        var moviesInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
        if (moviesInDb == null)
            return NotFound();
        _mapper.Map(movieDto, moviesInDb);
        _context.SaveChanges();
        return Ok();
    }

    // DELETE: api/Movies/5
    [HttpDelete("{id}")]
    public ActionResult<Movie> DeleteMovie(int id)
    {
        var movie = _context.Movies.Find(id);
        if (movie == null) return NotFound();

        _context.Movies.Remove(movie);
        _context.SaveChanges();
        return Ok(movie);
    }
}