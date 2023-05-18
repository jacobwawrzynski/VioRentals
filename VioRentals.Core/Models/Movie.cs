using System.ComponentModel.DataAnnotations;

namespace VioRentals.Models;

public class Movie
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Please enter movie's name.")]
    [StringLength(255)]
    public string Name { get; set; }

    public Genre? Genre { get; set; }


    [Display(Name = "Genre")]
    [Required(ErrorMessage = "Please select genre for the movie.")]
    public byte? GenreId { get; set; }

    public DateTime DateAdded { get; set; }

    [Display(Name = "Release Date")]
    [ReleaseDate]
    [Required(ErrorMessage = "Please enter release date for the movie.")]

    public DateTime? ReleaseDate { get; set; }

    [Range(1, 20)]
    [Display(Name = "Number in Stock")]
    [Required(ErrorMessage = "Please enter a value for the number in stock.")]

    public byte? NumberInStock { get; set; }

    public byte NumberAvailable { get; set; }
}

//may add this to seperated file
public class ReleaseDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var movie = (Movie)validationContext.ObjectInstance;

        if (movie.ReleaseDate.HasValue && movie.ReleaseDate.Value > DateTime.Today)
            return new ValidationResult("Release Date cannot be in the future.");

        return ValidationResult.Success;
    }
}