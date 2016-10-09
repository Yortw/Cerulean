using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class NullableDateTimeTableStorageFieldSerializer : TableEntityFieldSerializer
	{
		public NullableDateTimeTableStorageFieldSerializer() : 
			base
			(
				typeof(DateTime?),
				(value) => value == null ? String.Empty : ((DateTime)value).ToString("O"),
				(value) => String.IsNullOrEmpty(value) ? null : (DateTime?)DateTime.Parse(value)
			)
		{
		}
	}
}