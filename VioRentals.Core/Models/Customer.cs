using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.Models
{
	internal class Customer
	{	
		// Validation in the models
		public int Id { get; set; }
		public string ForeName { get; set; }
		public string Surname { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public bool IsSubscribingToNewsletter { get; set; }
		public byte MembershipTypeId { get; set; }
	}
}
