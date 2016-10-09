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
	public class PrimitiveSerializingTableEntityTests
	{

		[TestMethod]
		public void PrimitiveSerializingTableEntity_SerializesAllTypesWhenNotNull()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestNullableDecimal = entity.TestDecimal = 1.56M;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue;
			entity.TestNullableSingle = 2.3456F;
			entity.TestNullableInt16 = 4;
			entity.TestInt16 = 7;

			// Handled natively, but tested in case that changes
			entity.TestNullableBool = true;
			var dateNow = DateTime.Now;
			entity.TestNullableDateTime = dateNow;
			var offsetNow = DateTimeOffset.Now;
			entity.TestNullableDateTimeOffset = offsetNow;
			entity.TestNullableDouble = 3.14159;
			entity.TestNullableInt32 = 5;
			entity.TestNullableInt64 = 6;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			var op = TableOperation.Insert(entity);
			table.Execute(op);


			op = TableOperation.Retrieve<TestTableEntity>(entity.PartitionKey, entity.RowKey);
			var result = table.Execute(op);
			var storedEntity = result.Result as TestTableEntity;

			Assert.AreEqual((byte)1, storedEntity.TestNullableByte);
			Assert.AreEqual(1.56M, storedEntity.TestNullableDecimal);
			Assert.AreEqual(1.56M, storedEntity.TestDecimal);
			Assert.AreEqual(TestEnum.TestEnumValue, storedEntity.TestEnum);
			Assert.AreEqual(TestEnum.TestEnumValue, storedEntity.NullableTestEnum);
			Assert.AreEqual((short)7, storedEntity.TestInt16);
			Assert.AreEqual((short)4, storedEntity.TestNullableInt16);
			Assert.AreEqual(5, storedEntity.TestNullableInt32);
			Assert.AreEqual((long)6, storedEntity.TestNullableInt64);

			Assert.AreEqual(true, entity.TestNullableBool);
			Assert.AreEqual(dateNow, entity.TestNullableDateTime);
			Assert.AreEqual(offsetNow, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(3.14159, entity.TestNullableDouble);

			// Handled natively, but tested in case that changes
			Assert.AreEqual(true, entity.TestNullableBool);
			Assert.AreEqual(dateNow, entity.TestNullableDateTime);
			Assert.AreEqual(offsetNow, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(3.14159, entity.TestNullableDouble);
		}

		[TestMethod]
		public void PrimitiveSerializingTableEntity_SerializesAllNullableTypesWhenNull()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = null;
			entity.TestNullableDecimal = null;
			entity.TestNullableSingle = null;
			entity.TestNullableInt16 = null;
			entity.NullableTestEnum = null;

			// Handled natively, but tested in case that changes
			entity.TestNullableBool = null;
			entity.TestNullableDateTime = null;
			entity.TestNullableDateTimeOffset = null;
			entity.TestNullableDouble = null;
			entity.TestNullableInt32 = null;
			entity.TestNullableInt64 = null;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			var op = TableOperation.Insert(entity);
			table.Execute(op);

			op = TableOperation.Retrieve<TestTableEntity>(entity.PartitionKey, entity.RowKey);
			var result = table.Execute(op);
			var storedEntity = result.Result as TestTableEntity;

			Assert.AreEqual(null, storedEntity.TestNullableByte);
			Assert.AreEqual(null, storedEntity.TestNullableDecimal);
			Assert.AreEqual(null, storedEntity.TestNullableInt16);
			Assert.AreEqual(null, storedEntity.TestNullableInt32);
			Assert.AreEqual(null, storedEntity.TestNullableInt64);

			Assert.AreEqual(null, entity.TestNullableBool);
			Assert.AreEqual(null, entity.TestNullableDateTime);
			Assert.AreEqual(null, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(null, entity.TestNullableDouble);
			Assert.AreEqual(null, entity.NullableTestEnum);

			// Handled natively, but tested in case that changes
			Assert.AreEqual(null, entity.TestNullableBool);
			Assert.AreEqual(null, entity.TestNullableDateTime);
			Assert.AreEqual(null, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(null, entity.TestNullableDouble);
		}

		[TestMethod]
		public void PrimitiveSerializingTableEntity_ReplaceNullSetsNull()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			var op = TableOperation.Insert(entity);
			table.Execute(op);


			op = TableOperation.Retrieve<TestTableEntity>(entity.PartitionKey, entity.RowKey);
			var result = table.Execute(op);
			var storedEntity = result.Result as TestTableEntity;

			storedEntity.TestNullableByte = null;
			op = TableOperation.Replace(storedEntity);
			table.Execute(op);

			op = TableOperation.Retrieve<TestTableEntity>(entity.PartitionKey, entity.RowKey);
			result = table.Execute(op);
			storedEntity = result.Result as TestTableEntity;

			Assert.IsNull(storedEntity.TestNullableByte);
		}

		[TestMethod]
		public void PrimitiveSerializingTableEntity_MergeNullDoesNotSetNull()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			var op = TableOperation.Insert(entity);
			table.Execute(op);

			var entityToMerge = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestNullableBool = true,
				ETag = "*"
			};

			op = TableOperation.Merge(entityToMerge);
			table.Execute(op);

			op = TableOperation.Retrieve<TestTableEntity>(entity.PartitionKey, entity.RowKey);
			var result = table.Execute(op);
			var storedEntity = result.Result as TestTableEntity;

			Assert.AreEqual((byte)1, storedEntity.TestNullableByte);
			Assert.AreEqual(true, storedEntity.TestNullableBool);
		}

	}

	public class TestTableEntity : PrimitiveSerializingTableEntity
	{

		public bool? TestNullableBool { get; set; }

		public DateTimeOffset? TestNullableDateTimeOffset { get; set; }

		public DateTime? TestNullableDateTime { get; set; }

		public byte? TestNullableByte { get; set; }

		public decimal? TestNullableDecimal { get; set; }

		public decimal TestDecimal { get; set; }

		public double? TestNullableDouble { get; set; }

		public float? TestNullableSingle { get; set; }

		public short TestInt16 {get;set;}

		public short? TestNullableInt16 { get; set; }

		public int? TestNullableInt32 { get; set; }

		public long? TestNullableInt64 { get; set; }

		public TestEnum TestEnum { get; set; }

		public TestEnum? NullableTestEnum { get; set; }

	}

	public enum TestEnum
	{
		None = 0,
		TestEnumValue = 1,
		TestEnumValue2 = 2
	}
}