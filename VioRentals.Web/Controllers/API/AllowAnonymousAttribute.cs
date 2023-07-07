namespace VioRentals.Web.Controllers.API
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
        // Class to avoid ambiguous reference errors with [AllowAnonymous]
        // Leave empty
    }
}
