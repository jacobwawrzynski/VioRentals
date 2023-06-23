using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;

namespace VioRentals.Core.Entities
{
    public class MembershipDetailsEntity : BaseEntity
	{
		public int Id { get; set; }
		public short SignUpFee { get; set; }
		public byte DurationInMonths { get; set; }
		public byte DiscountRate { get; set; }

		// Relationships
		public IEnumerable<CustomerEntity> _Customers { get; set; }
	}
}
