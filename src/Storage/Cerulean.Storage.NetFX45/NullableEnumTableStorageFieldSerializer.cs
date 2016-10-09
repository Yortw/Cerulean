using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class NullableEnumTableStorageFieldSerializer<T> : TableEntityFieldSerializer where T : struct
	{
		public NullableEnumTableStorageFieldSerializer() : 
			base
			(
				typeof(Nullable<T>),
				(value) => value == null ? String.Empty : value.ToString(),
				(value) =>
				{
					if (String.IsNullOrEmpty(value)) return null;
					return (T)Enum.Parse(typeof(T), value);	
				}
			)
		{
		}
	}
}