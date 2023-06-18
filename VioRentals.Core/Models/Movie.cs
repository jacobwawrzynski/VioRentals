using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VioRentals.Core.Models
{
	public class Movie
	{
		public int Id { get; set; }
		
		[Required]
		[StringLength(255)]
		public string Name { get; set; }
		public int? GenreId { get; set; }
		public DateTime DateAdded { get; set; } = DateTime.Now;
		public DateTime? ReleaseDate { get; set; }

		[Range(1, 20)]
		public byte? NumberInStock { get; set; }
		public byte NumberAvailable { get; set; }
	}
}
