using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.Models.ModelsValidation
{
	public class ReleaseDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var movie = (Movie)validationContext.ObjectInstance;

			if (movie.ReleaseDate.HasValue && movie.ReleaseDate.Value > DateTime.Today)
				return new ValidationResult("Release Date cannot be in the future.");

			return ValidationResult.Success;
		}
	}
}
