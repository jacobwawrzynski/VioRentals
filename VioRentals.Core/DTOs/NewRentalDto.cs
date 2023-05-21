using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.DTOs
{
	public class NewRentalDto
	{
		public int CustomerId { get; set; }
		public List<int> MovieIds { get; set; }
	}
}
