namespace VioRentals.Web.DTOs
{
	public class LoginDto
	{
		//TODO VALIDATION
		public string Email { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
	}
}
