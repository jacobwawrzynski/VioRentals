using System.ComponentModel.DataAnnotations;

namespace VioRentals.Models;

public class Rental
{
    public int Id { get; set; }

    [Required] public Customer Customer { get; set; }

    public Movie Movie { get; set; }

    [Required] public int MovieId { get; set; }

    public DateTime DateRented { get; set; }
    public DateTime? DateReturned { get; set; }
    public bool Returned { get; set; }
}