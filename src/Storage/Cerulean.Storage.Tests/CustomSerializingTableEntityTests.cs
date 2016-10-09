using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage.Tests
{
	[TestClass]
	public class CustomSerializingTableEntityTests
	{

		[TestMethod]
		public void CustomSerializingTableEntity_SerializesAllTypes()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestNullableDecimal = entity.TestDecimal = 1.56M;
			entity.TestEnum = TestEnum.TestEnumValue;
			
			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			var op = TableOperation.Insert(entity);
			table.Execute(op);
		}
	}

	public class TestTableEntity : CustomSerializingTableEntity
	{
		public TestTableEntity() : base(new TableEntityFieldSerializer[]
		{
			TableEntityFieldSerializers.NullableBoolSerializer,
			TableEntityFieldSerializers.NullableByteSerializer,
			TableEntityFieldSerializers.NullableDateTimeOffsetSerializer,
			TableEntityFieldSerializers.NullableDateTimeSerializer,
			TableEntityFieldSerializers.NullableDecimalSerializer,
			TableEntityFieldSerializers.NullableDoubleSerializer,
			TableEntityFieldSerializers.NullableFloatSerializer,
			TableEntityFieldSerializers.NullableInt16Serializer,
			TableEntityFieldSerializers.NullableInt32Serializer,
			TableEntityFieldSerializers.NullableInt64Serializer,
			TableEntityFieldSerializers.DecimalSerializer,
			new EnumTableStorageFieldSerializer<TestEnum>(),
		})
		{

		}

		public byte? TestNullableByte { get; set; }
		public decimal? TestNullableDecimal { get; set; }
		public decimal TestDecimal { get; set; }
		public TestEnum TestEnum { get; set; }
	}

	public enum TestEnum
	{
		None = 0,
		TestEnumValue = 1,
		TestEnumValue2 = 2
	}
}