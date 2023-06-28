using VioRentals.Core.Entities;

namespace VioRentals.Web.DTOs
{
    public class CustomerDto
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public MembershipTypeEnum MembershipType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSubscibingToNewsletter { get; set; }
        public int MembershipDetailsFK { get => (int)MembershipType; }

    }
}
