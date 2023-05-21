using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.Models
{
	public class Rental
	{
		public int Id { get; set; }

		[Required]
		public Customer Customer { get; set; }

		public Movie Movie { get; set; }

		[Required] 
		public int MovieId { get; set; }

		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }
		public bool Returned { get; set; }
	}
}
