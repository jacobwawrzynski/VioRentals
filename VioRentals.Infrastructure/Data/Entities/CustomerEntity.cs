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
		public string ForeName { get; set; }
		public string Surname { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public bool IsSubscribingToNewsletter { get; set; }
		public byte MembershipTypeId { get; set; }
	}
}
