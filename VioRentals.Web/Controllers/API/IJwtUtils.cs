using VioRentals.Core.Entities;

namespace VioRentals.Web.Controllers.API
{
    public interface IJwtUtils
    {
        public string GenerateToken(UserEntity user);
        public int? ValidateToken(string token);
    }
}
