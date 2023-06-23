using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities.Validation;

namespace VioRentals.Core.Entities
{
	public class CustomerEntity
	{
		public int Id { get; set; }

		[StringLength(100)]
		public string Forename { get; set; }

		[StringLength(100)]
		public string Surname { get; set; }
        public MembershipTypeEnum MembershipType { get; set; }

        [AgeValidation]
        public DateTime DateOfBirth { get; set; }
		public bool IsSubscribingToNewsletter { get; set; }

		// Relationships
		public IEnumerable<RentalEntity> _Rentals { get; set; }

		public int MembershipDetailsFK { get; set; }
		public MembershipDetailsEntity _MembershipDetails { get; set; }
	}
}
