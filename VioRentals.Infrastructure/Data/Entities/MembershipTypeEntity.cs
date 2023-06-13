using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VioRentals.Infrastructure.Data.Entities
{
	public class MembershipTypeEntity
	{
		//public static readonly byte Unknown = 0;
		//public static readonly byte PayAsYouGo = 1;
		public int Id { get; set; }
		public string Name { get; set; }
		public short SignUpFee { get; set; }
		public byte DurationInMonths { get; set; }
		public byte DiscountRate { get; set; }

		// Relationships
		public IEnumerable<CustomerEntity> _Customers { get; set; }
	}
}
