using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cerulean.Storage;

namespace Cerulean.Storage.Tests
{
	[TestClass]
	public class NullableTTableStorageFieldSerializerTests
	{

		#region NullableByteSerializer

		[TestMethod]
		public void NullableByteSerializer_Serialize_SerializesNonNullByte()
		{
			Assert.AreEqual("1", TableEntityFieldSerializers.NullableByteSerializer.Serialize((byte?)1));
		}

		[TestMethod]
		public void NullableByteSerializer_Serialize_SerializesNullByteAsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableByteSerializer.Serialize((byte?)null));
		}

		[TestMethod]
		public void NullableByteSerializer_Deserialize_DeserializesNullByte()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableByteSerializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableByteSerializer_Deserialize_DeserializesNonNullByte()
		{
			Assert.AreEqual((byte)1, TableEntityFieldSerializers.NullableByteSerializer.Deserialize("1"));
		}

		#endregion

		#region NullableInt16Serializer

		[TestMethod]
		public void NullableInt16Serializer_Serialize_SerializesNonNullInt16()
		{
			Assert.AreEqual("1", TableEntityFieldSerializers.NullableInt16Serializer.Serialize((Int16?)1));
		}

		[TestMethod]
		public void NullableInt16Serializer_Serialize_SerializesNullInt16AsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableInt16Serializer.Serialize((Int16?)null));
		}

		[TestMethod]
		public void NullableInt16Serializer_Deserialize_DeserializesNullInt16()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableInt16Serializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableInt16Serializer_Deserialize_DeserializesNonNullInt16()
		{
			Assert.AreEqual((Int16)1, TableEntityFieldSerializers.NullableInt16Serializer.Deserialize("1"));
		}

		#endregion

		#region NullableInt32Serializer

		[TestMethod]
		public void NullableInt32Serializer_Serialize_SerializesNonNullInt32()
		{
			Assert.AreEqual("1", TableEntityFieldSerializers.NullableInt32Serializer.Serialize((Int32?)1));
		}

		[TestMethod]
		public void NullableInt32Serializer_Serialize_SerializesNullInt32AsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableInt32Serializer.Serialize((Int32?)null));
		}

		[TestMethod]
		public void NullableInt32Serializer_Deserialize_DeserializesNullInt32()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableInt32Serializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableInt32Serializer_Deserialize_DeserializesNonNullInt32()
		{
			Assert.AreEqual((Int32)1, TableEntityFieldSerializers.NullableInt32Serializer.Deserialize("1"));
		}

		#endregion

		#region NullableInt64Serializer

		[TestMethod]
		public void NullableInt64Serializer_Serialize_SerializesNonNullInt64()
		{
			Assert.AreEqual("1", TableEntityFieldSerializers.NullableInt64Serializer.Serialize((Int64?)1));
		}

		[TestMethod]
		public void NullableInt64Serializer_Serialize_SerializesNullInt64AsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableInt64Serializer.Serialize((Int64?)null));
		}

		[TestMethod]
		public void NullableInt64Serializer_Deserialize_DeserializesNullInt64()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableInt64Serializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableInt64Serializer_Deserialize_DeserializesNonNullInt64()
		{
			Assert.AreEqual((Int64)1, TableEntityFieldSerializers.NullableInt64Serializer.Deserialize("1"));
		}

		#endregion

		#region NullableDecimalSerializer

		[TestMethod]
		public void NullableDecimalSerializer_Serialize_SerializesNonNullDecimal()
		{
			Assert.AreEqual("1", TableEntityFieldSerializers.NullableDecimalSerializer.Serialize((Decimal?)1));
		}

		[TestMethod]
		public void NullableDecimalSerializer_Serialize_SerializesNullDecimalAsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableDecimalSerializer.Serialize((Decimal?)null));
		}

		[TestMethod]
		public void NullableDecimalSerializer_Deserialize_DeserializesNullDecimal()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableDecimalSerializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableDecimalSerializer_Deserialize_DeserializesNonNullDecimal()
		{
			Assert.AreEqual((Decimal)1, TableEntityFieldSerializers.NullableDecimalSerializer.Deserialize("1"));
		}

		#endregion

		#region NullableFloatSerializer

		[TestMethod]
		public void NullableFloatSerializer_Serialize_SerializesNonNullFloat()
		{
			Assert.AreEqual("1", TableEntityFieldSerializers.NullableFloatSerializer.Serialize((float?)1));
		}

		[TestMethod]
		public void NullableFloatSerializer_Serialize_SerializesNullFloatAsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableFloatSerializer.Serialize((float?)null));
		}

		[TestMethod]
		public void NullableFloatSerializer_Deserialize_DeserializesNullFloat()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableFloatSerializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableFloatSerializer_Deserialize_DeserializesNonNullFloat()
		{
			Assert.AreEqual((float)1, TableEntityFieldSerializers.NullableFloatSerializer.Deserialize("1"));
		}

		#endregion

		#region NullableDoubleSerializer

		[TestMethod]
		public void NullableDoubleSerializer_Serialize_SerializesNonNullDouble()
		{
			Assert.AreEqual("1", TableEntityFieldSerializers.NullableDoubleSerializer.Serialize((Double?)1));
		}

		[TestMethod]
		public void NullableDoubleSerializer_Serialize_SerializesNullDoubleAsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableDoubleSerializer.Serialize((Double?)null));
		}

		[TestMethod]
		public void NullableDoubleSerializer_Deserialize_DeserializesNullDouble()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableDoubleSerializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableDoubleSerializer_Deserialize_DeserializesNonNullDouble()
		{
			Assert.AreEqual((Double)1, TableEntityFieldSerializers.NullableDoubleSerializer.Deserialize("1"));
		}

		#endregion

		#region NullableBoolSerializer

		[TestMethod]
		public void NullableBoolSerializer_Serialize_SerializesNonNullBool()
		{
			Assert.AreEqual("True", TableEntityFieldSerializers.NullableBoolSerializer.Serialize((bool?)true));
		}

		[TestMethod]
		public void NullableBoolSerializer_Serialize_SerializesNullBoolAsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableBoolSerializer.Serialize((bool?)null));
		}

		[TestMethod]
		public void NullableBoolSerializer_Deserialize_DeserializesNullBool()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableBoolSerializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableBoolSerializer_Deserialize_DeserializesNonNullBool()
		{
			Assert.AreEqual(true, TableEntityFieldSerializers.NullableBoolSerializer.Deserialize("True"));
		}

		#endregion

		#region NullableDateTimeSerializer

		[TestMethod]
		public void NullableDateTimeSerializer_Serialize_SerializesNonNullDateTime()
		{
			var now = DateTime.Now;
			var serializedValue = TableEntityFieldSerializers.NullableDateTimeSerializer.Serialize((DateTime?)now);
			Assert.AreEqual(now.ToString("O"), serializedValue);
		}

		[TestMethod]
		public void NullableDateTimeSerializer_Serialize_SerializesNullDateTimeAsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableDateTimeSerializer.Serialize((DateTime?)null));
		}

		[TestMethod]
		public void NullableDateTimeSerializer_Deserialize_DeserializesNullDateTime()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableDateTimeSerializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableDateTimeSerializer_Deserialize_DeserializesNonNullDateTime()
		{
			var now = DateTime.Now;
			var serializedValue = TableEntityFieldSerializers.NullableDateTimeSerializer.Serialize(now);
			Assert.AreEqual((DateTime)now, (DateTime)TableEntityFieldSerializers.NullableDateTimeSerializer.Deserialize(serializedValue));
		}

		#endregion

		#region NullableDateTimeOffsetSerializer

		[TestMethod]
		public void NullableDateTimeOffsetSerializer_Serialize_SerializesNonNullDateTimeOffset()
		{
			var now = DateTimeOffset.Now;
			var serializedValue = TableEntityFieldSerializers.NullableDateTimeOffsetSerializer.Serialize((DateTimeOffset?)now);
			Assert.AreEqual(now.ToString("O"), serializedValue);
		}

		[TestMethod]
		public void NullableDateTimeOffsetSerializer_Serialize_SerializesNullDateTimeOffsetAsEmptyString()
		{
			Assert.AreEqual(String.Empty, TableEntityFieldSerializers.NullableDateTimeOffsetSerializer.Serialize((DateTimeOffset?)null));
		}

		[TestMethod]
		public void NullableDateTimeOffsetSerializer_Deserialize_DeserializesNullDateTimeOffset()
		{
			Assert.AreEqual(null, TableEntityFieldSerializers.NullableDateTimeOffsetSerializer.Deserialize(String.Empty));
		}

		[TestMethod]
		public void NullableDateTimeOffsetSerializer_Deserialize_DeserializesNonNullDateTimeOffset()
		{
			var now = DateTimeOffset.Now;
			var serializedValue = TableEntityFieldSerializers.NullableDateTimeOffsetSerializer.Serialize(now);
			var deserializedValue = (DateTimeOffset)TableEntityFieldSerializers.NullableDateTimeOffsetSerializer.Deserialize(serializedValue);
			Assert.AreEqual((DateTimeOffset)now, deserializedValue);
		}

		#endregion

	}
}