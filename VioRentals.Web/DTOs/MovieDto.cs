namespace VioRentals.Web.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public DateTime? ReleaseDate { get; set; }
        public byte NumberInStock { get; set; }
        public byte NumberAvailable { get; set; }

        // ADD GENRE ENUM

        public int? GenreFK { get; set; }
    }
}
