using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Infrastructure.Data.Entities
{
	public class GenreEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }

		// Relationships
		public IEnumerable<MovieEntity> _Movies { get; set; }
	}
}
