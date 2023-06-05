using System.ComponentModel.DataAnnotations;

namespace VioRentals.Web.DTOs
{
	public class RegisterDto
	{
		//[EmailAddress]
		public string Email { get; set; }
		public string Password { get; set; }

		//[RegularExpression("^[A-Za-z]{2,50}")]
		public string Forename { get; set; }

		//[RegularExpression("^[A-Za-z]{2,50}$")]
		public string Lastname { get; set; }
	}
}
