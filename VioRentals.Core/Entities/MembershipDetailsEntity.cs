using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Core.Entities
{
    public class MembershipDetailsEntity : BaseEntity
	{
		[Key]
		public new int Id { get; set; }
		public string Name { get; set; }
		public short SignUpFee { get; set; }
		public byte DurationInMonths { get; set; }
		public byte DiscountRate { get; set; }

		// Relationships
		public ICollection<CustomerEntity> _Customers { get; set; }
	}
}
