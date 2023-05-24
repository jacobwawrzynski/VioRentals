using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Models;

namespace VioRentals.Infrastructure.Data.Entities
{
	public class MovieEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Genre? Genre { get; set; }
		public byte? GenreId { get; set; }
		public DateTime DateAdded { get; set; }
		public DateTime? ReleaseDate { get; set; }
		public byte? NumberInStock { get; set; }
		public byte NumberAvailable { get; set; }
	}
}
