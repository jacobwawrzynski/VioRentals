using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Models;

namespace VioRentals.Infrastructure.Data.Entities
{
	public class RentalEntity
	{
		public int Id { get; set; }
		public Customer Customer { get; set; }
		public Movie Movie { get; set; }
		public int MovieId { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }
		public bool Returned { get; set; }
	}
}
