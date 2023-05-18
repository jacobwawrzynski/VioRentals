using System.ComponentModel.DataAnnotations;
using VioRentals.Models;

namespace VioRentals.ViewModels;

public class MovieFormViewModel
{
    internal bool HasErrors;

    public MovieFormViewModel()
    {
        Id = 0;
    }

    public MovieFormViewModel(Movie movie)
    {
        Id = movie.Id;
        Name = movie.Name;
        GenreId = movie.GenreId;
        ReleaseDate = movie.ReleaseDate;
        NumberInStock = movie.NumberInStock;
    }

    public IEnumerable<Genre> Genres { get; set; }
    public int Id { get; set; }

    [Required] [StringLength(255)] public string? Name { get; set; }

    [Display(Name = "Genre")] [Required] public byte? GenreId { get; set; }

    [Display(Name = "Release Date")]
    [Required]
    public DateTime? ReleaseDate { get; set; }

    [Range(1, 20)]
    [Display(Name = "Number in Stock")]
    [Required(ErrorMessage = "Please enter customer's name.")]
    public byte? NumberInStock { get; set; }

    public string Title => Id != 0 ? "Edit Movie" : "New Movie";
}