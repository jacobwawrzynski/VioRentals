﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Infrastructure.Data.Entities
{
	public class RentalEntity
	{
		public int Id { get; set; }
		//public Customer Customer { get; set; }
		//public Movie Movie { get; set; }
		//public int MovieId { get; set; }
		public DateTime DateRented { get; set; }
		public DateTime? DateReturned { get; set; }
		public bool Returned { get; set; }

		// Relationshpis
		public int CustomerFK { get; set; }
		public CustomerEntity _Customer { get; set; }

		public int MovieFK { get; set; }
		public MovieEntity _Movie { get; set; }

	}
}
