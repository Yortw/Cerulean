using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class DecimalTableStorageFieldSerializer : TableEntityFieldSerializer
	{
		public DecimalTableStorageFieldSerializer() : 
			base
			(
				typeof(decimal),
				(value) => ((decimal)value).ToString(System.Globalization.CultureInfo.InvariantCulture),
				(value) => Decimal.Parse(value, System.Globalization.CultureInfo.InvariantCulture)
			)
		{
		}
	}
}