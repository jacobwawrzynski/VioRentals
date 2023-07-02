﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.Entities
{
	public class GenreEntity : BaseEntity
	{
		//[Key]
		//public override int Id { get; set; }
		public string Name { get; set; }

		// Relationships
		public ICollection<MovieEntity> _Movies { get; set; }
	}
}
