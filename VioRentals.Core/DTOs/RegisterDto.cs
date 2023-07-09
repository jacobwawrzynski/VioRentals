using System.ComponentModel.DataAnnotations;

namespace VioRentals.Core.DTOs
{
    public class RegisterDto
    {
        //[EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        //[RegularExpression("^[A-Za-z]{2,50}")]
        [Required]
        public string Forename { get; set; }

        //[RegularExpression("^[A-Za-z]{2,50}$")]
        [Required]
        public string Lastname { get; set; }
    }
}
