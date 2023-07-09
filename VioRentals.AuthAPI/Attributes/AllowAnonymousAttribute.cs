namespace VioRentals.AuthAPI.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute : Attribute
    {
        // Class to avoid ambiguous reference errors between namespaces
        // Leave empty
    }
}
