using VioRentals.Core.Entities;

namespace VioRentals.AuthAPI
{
    public interface IJwtUtils
    {
        public string GenerateToken(UserEntity user);
        public int? ValidateToken(string token);
    }
}
