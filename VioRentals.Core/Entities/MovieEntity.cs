using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities.Validation;

namespace VioRentals.Core.Entities
{
	public class MovieEntity
	{
		public int Id { get; set; }

		[StringLength(255)]
		public string Name { get; set; }
		public DateTime DateAdded { get; set; } = DateTime.Now;

		[ReleaseDate]
		public DateTime? ReleaseDate { get; set; }

		[Range(1, 20)]
		public byte NumberInStock { get; set; }
		public byte NumberAvailable { get; set; }

		// Relationships
		public IEnumerable<RentalEntity> _Rentals { get; set; }

		public int? GenreFK { get; set; }
		public GenreEntity _Genre { get; set; }
	}
}
