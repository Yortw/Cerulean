using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	/// <summary>
	/// Provides common implementations of <see cref="TableEntityFieldSerializer"/>.
	/// </summary>
	public static class TableEntityFieldSerializers
	{

		private static TableEntityFieldSerializer _NullableByteSerializer;
		private static TableEntityFieldSerializer _NullableInt16Serializer;
		private static TableEntityFieldSerializer _NullableInt32Serializer;
		private static TableEntityFieldSerializer _NullableInt64Serializer;
		private static TableEntityFieldSerializer _NullableDecimalSerializer;
		private static TableEntityFieldSerializer _NullableFloatSerializer;
		private static TableEntityFieldSerializer _NullableDoubleSerializer;
		private static TableEntityFieldSerializer _NullableBoolSerializer;
		private static TableEntityFieldSerializer _NullableDateTimeSerializer;
		private static TableEntityFieldSerializer _NullableDateTimeOffsetSerializer;
		private static TableEntityFieldSerializer _DecimalSerializer;
		private static TableEntityFieldSerializer _DateTimeOffsetSerializer;

		public static TableEntityFieldSerializer NullableByteSerializer
		{
			get
			{
				return _NullableByteSerializer ??
				(
					_NullableByteSerializer = new NullableTTableStorageFieldSerializer<byte>()
				);
			}
		}

		public static TableEntityFieldSerializer NullableInt16Serializer
		{
			get
			{
				return _NullableInt16Serializer ??
				(
					_NullableInt16Serializer = new NullableTTableStorageFieldSerializer<Int16>()
				);
			}
		}

		public static TableEntityFieldSerializer NullableInt32Serializer
		{
			get
			{
				return _NullableInt32Serializer ??
				(
					_NullableInt32Serializer = new NullableTTableStorageFieldSerializer<int>()
				);
			}
		}

		public static TableEntityFieldSerializer NullableInt64Serializer
		{
			get
			{
				return _NullableInt64Serializer ??
				(
					_NullableInt64Serializer = new NullableTTableStorageFieldSerializer<Int64>()
				);
			}
		}

		public static TableEntityFieldSerializer NullableDecimalSerializer
		{
			get
			{
				return _NullableDecimalSerializer ?? 
				(
					_NullableDecimalSerializer = new NullableTTableStorageFieldSerializer<decimal>()
				);
			}
		}

		public static TableEntityFieldSerializer NullableFloatSerializer
		{
			get
			{
				return _NullableFloatSerializer ??
				(
					_NullableFloatSerializer = new NullableTTableStorageFieldSerializer<float>()
				);
			}
		}

		public static TableEntityFieldSerializer NullableDoubleSerializer
		{
			get
			{
				return _NullableDoubleSerializer ??
				(
					_NullableDoubleSerializer = new NullableTTableStorageFieldSerializer<double>()
				);
			}
		}

		public static TableEntityFieldSerializer NullableBoolSerializer
		{
			get
			{
				return _NullableBoolSerializer ??
				(
					_NullableBoolSerializer = new NullableBoolTableStorageFieldSerializer()
				);
			}
		}

		public static TableEntityFieldSerializer NullableDateTimeSerializer
		{
			get
			{
				return _NullableDateTimeSerializer ??
				(
					_NullableDateTimeSerializer = new NullableDateTimeTableStorageFieldSerializer()
				);
			}
		}

		public static TableEntityFieldSerializer NullableDateTimeOffsetSerializer
		{
			get
			{
				return _NullableDateTimeOffsetSerializer ??
				(
					_NullableDateTimeOffsetSerializer = new NullableDateTimeOffsetTableStorageFieldSerializer()
				);
			}
		}

		public static TableEntityFieldSerializer DecimalSerializer
		{
			get
			{
				return _DecimalSerializer ??
				(
					_DecimalSerializer = new DecimalTableStorageFieldSerializer()
				);
			}
		}

	}
}