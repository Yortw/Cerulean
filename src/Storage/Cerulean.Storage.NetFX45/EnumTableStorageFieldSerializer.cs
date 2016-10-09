using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class EnumTableStorageFieldSerializer<T> : TableEntityFieldSerializer 
	{
		public EnumTableStorageFieldSerializer() : 
			base
			(
				typeof(T),
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