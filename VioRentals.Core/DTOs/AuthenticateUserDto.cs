namespace VioRentals.Core.DTOs
{
    public class AuthenticateUserDto // Authenticate Response
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
