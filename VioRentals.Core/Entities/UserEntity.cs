using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace VioRentals.Core.Entities
{
	public class UserEntity : BaseEntity
	{
		public string Email { get; set; }
		
		[JsonIgnore]
		public byte[] PasswordHash { get; set; }

		[JsonIgnore]
		public byte[] PasswordSalt { get; set; }
		public string Forename { get; set; }
		public string Lastname { get; set; }
	}
}
