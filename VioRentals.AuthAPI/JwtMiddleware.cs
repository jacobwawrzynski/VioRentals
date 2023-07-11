using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.AuthAPI
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            //var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(jwtUtils.GetTokenDto());
            if (userId != null)
            {
                context.Items["User"] = userService.FindByIdAsync(userId.Value);
            }

            await _next(context);
        }
    }
}
