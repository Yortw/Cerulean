using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class NullableBoolTableStorageFieldSerializer : TableEntityFieldSerializer
	{
		public NullableBoolTableStorageFieldSerializer() : 
			base
			(
				typeof(bool?),
				(value) => value == null ? String.Empty : value.ToString(),
				(value) => String.IsNullOrEmpty(value) ? null : (bool?)bool.Parse(value)
			)
		{
		}
	}
}