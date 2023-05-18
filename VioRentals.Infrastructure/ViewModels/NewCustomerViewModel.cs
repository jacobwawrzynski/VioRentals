using VioRentals.Models;

namespace VioRentals.ViewModels;

public class NewCustomerViewModel
{
    public IEnumerable<MembershipType> MembershipTypes { get; set; }
    public Customer Customer { get; set; }
    public bool HasErrors { get; set; }
}