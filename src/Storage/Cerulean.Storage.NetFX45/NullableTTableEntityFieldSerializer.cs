using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	public class NullableTTableStorageFieldSerializer<T> : TableEntityFieldSerializer where T : struct, IFormattable
	{
		public NullableTTableStorageFieldSerializer() : base(typeof(Nullable<T>), SerializeNullable, DeserializeNullable)
		{
		}

		private static object DeserializeNullable(string serializedValue)
		{
			if (String.IsNullOrEmpty(serializedValue)) return null;

			return System.Convert.ChangeType(serializedValue, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
		}

		private static string SerializeNullable(object value)
		{
			var nullableValue = ((Nullable<T>)value);
			if (nullableValue == null) return String.Empty;

			return ((IFormattable)nullableValue.Value).ToString(null, System.Globalization.CultureInfo.InvariantCulture);
		}
	}
}