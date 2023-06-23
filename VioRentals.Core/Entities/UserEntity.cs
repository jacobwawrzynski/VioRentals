using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Core.Entities
{
	public class UserEntity : BaseEntity
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public byte[] PasswordHash { get; set; }
		public byte[] PasswordSalt { get; set; }
		public string Forename { get; set; }
		public string Lastname { get; set; }
	}
}
