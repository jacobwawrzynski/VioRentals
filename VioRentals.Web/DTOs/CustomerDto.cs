using VioRentals.Core.Entities;

namespace VioRentals.Web.DTOs
{
    public class CustomerDto
    {
        public string Forename { get; set; }
        public string Surename { get; set; }
        public MembershipTypeEnum MembershipType { get; set; }
        public DateTime DateOfBirdth { get; set; }
        public bool IsSubscibingToNewsletter { get; set; }
        public int MembershipDetailsFK { get => (int)MembershipType; }
    }
}
