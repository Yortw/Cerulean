using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class NullableDateTimeOffsetTableStorageFieldSerializer : TableEntityFieldSerializer
	{
		public NullableDateTimeOffsetTableStorageFieldSerializer() : 
			base
			(
				typeof(DateTimeOffset?),
				(value) => value == null ? String.Empty : ((DateTimeOffset)value).ToString("O"),
				(value) => String.IsNullOrEmpty(value) ? null : (DateTimeOffset?)DateTimeOffset.Parse(value)
			)
		{
		}
	}
}