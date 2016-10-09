using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class Int16TableStorageFieldSerializer : TableEntityFieldSerializer
	{
		public Int16TableStorageFieldSerializer() : 
			base
			(
				typeof(Int16),
				(value) => ((Int16)value).ToString(System.Globalization.CultureInfo.InvariantCulture),
				(value) => Int16.Parse(value, System.Globalization.CultureInfo.InvariantCulture)
			)
		{
		}
	}
}