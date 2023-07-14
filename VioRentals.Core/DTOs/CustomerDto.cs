using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Core.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public MembershipTypeEnum MembershipType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsSubscribingToNewsletter { get; set; }
        public int MembershipDetailsFK { get => (int)MembershipType; }
    }
}
