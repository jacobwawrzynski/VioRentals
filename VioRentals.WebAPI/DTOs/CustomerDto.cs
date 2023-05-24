using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.DTOs
{
	public class CustomerDto
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public bool IsSubscribedToNewsletter { get; set; }

		public byte MembershipTypeId { get; set; }
	}
}
