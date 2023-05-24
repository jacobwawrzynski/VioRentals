namespace VioRentals.Web.DTOs
{
	public class UserDto
	{
		public string? Email { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public string Forename { get; set; }
		public string Lastname { get; set; }
	}
}
