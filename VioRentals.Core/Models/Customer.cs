using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Models.ModelsValidation;

namespace VioRentals.Core.Models
{
	public class Customer
	{
		public int Id { get; set; }
		
		[Required]
		[StringLength(100)]
		public string ForeName { get; set; }

		[Required]
		[StringLength(100)]
		public string Surname { get; set; }

		[AgeValidation]
		public DateTime? DateOfBirth { get; set; }
		public bool IsSubscribedToNewsletter { get; set; }
		public int MembershipTypeFK { get; set; }
	}
}
