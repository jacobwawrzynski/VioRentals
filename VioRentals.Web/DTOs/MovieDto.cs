namespace VioRentals.Web.DTOs
{
    public class MovieDto
    {
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public byte NumberInStock { get; set; }
        public byte NumberAvailable { get; set; }
        public int? GenreKF { get; set; }
    }
}
