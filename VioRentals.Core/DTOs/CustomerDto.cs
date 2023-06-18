using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.DTOs
{
	internal class CustomerDto
	{
		public int Id { get; set; }
		public string Forename { get; set; }
		public string Surname { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public bool IsSubscribedToNewsletter { get; set; }
		public byte MembershipTypeId { get; set; }
	}
}
