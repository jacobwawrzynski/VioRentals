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
		public int MovieFK { get; set; }
		public int CustomerFK { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }
		public bool Returned { get; set; }
	}
}
