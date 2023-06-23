using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Infrastructure.Data.Entities
{
	public class CustomerEntity
	{
		public int Id { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public bool IsSubscribingToNewsletter { get; set; }

		// Relationships
		public IEnumerable<RentalEntity> _Rentals { get; set; }

		public int MembershipDetailsFK { get; set; }
		public MembershipDetailsEntity _MembershipDetails { get; set; }
	}
}
