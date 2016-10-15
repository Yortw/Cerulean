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
	public class TableExtensionTests
	{

		[TestMethod]
		public void TableExtensionTests_InsertEntity_InsertsOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertEntity(entity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual(entity.TestNullableByte, storedEntity.TestNullableByte);
			Assert.AreEqual(entity.TestEnum, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public async Task TableExtensionTests_InsertEntityAsync_InsertsOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertEntityAsync(entity);

			var storedEntity = await table.RetrieveEntityAsync<TestTableEntity>(entity);
			Assert.AreEqual(entity.TestNullableByte, storedEntity.TestNullableByte);
			Assert.AreEqual(entity.TestEnum, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public void TableExtensionTests_InsertOrReplaceEntity_InsertsOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertOrReplaceEntity(entity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual(entity.TestNullableByte, storedEntity.TestNullableByte);
			Assert.AreEqual(entity.TestEnum, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public async Task TableExtensionTests_InsertOrReplaceEntityAsync_InsertsOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertOrReplaceEntityAsync(entity);

			var storedEntity = await table.RetrieveEntityAsync<TestTableEntity>(entity);
			Assert.AreEqual(entity.TestNullableByte, storedEntity.TestNullableByte);
			Assert.AreEqual(entity.TestEnum, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public void TableExtensionTests_InsertOrReplaceEntity_ReplacesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertOrReplaceEntity(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.InsertOrReplaceEntity(updateEntity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual(null, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(null, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public async Task TableExtensionTests_InsertOrReplaceEntityAsync_ReplacesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertOrReplaceEntityAsync(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.InsertOrReplaceEntityAsync(updateEntity);

			var storedEntity = await table.RetrieveEntityAsync<TestTableEntity>(entity);
			Assert.AreEqual(null, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(null, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public void TableExtensionTests_InsertOrMergeEntity_InsertsOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertOrMergeEntity(entity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual(entity.TestNullableByte, storedEntity.TestNullableByte);
			Assert.AreEqual(entity.TestEnum, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public async Task TableExtensionTests_InsertOrMergeEntityAsync_InsertsOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertOrMergeEntityAsync(entity);

			var storedEntity = await table.RetrieveEntityAsync<TestTableEntity>(entity);
			Assert.AreEqual(entity.TestNullableByte, storedEntity.TestNullableByte);
			Assert.AreEqual(entity.TestEnum, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public void TableExtensionTests_InsertOrMergeEntity_MergesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertOrMergeEntity(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.InsertOrMergeEntity(updateEntity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual((byte)1, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public async Task TableExtensionTests_InsertOrMergeEntityAsync_MergesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertOrMergeEntityAsync(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.InsertOrMergeEntityAsync(updateEntity);

			var storedEntity = await table.RetrieveEntityAsync<TestTableEntity>(entity);
			Assert.AreEqual((byte)1, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(entity.NullableTestEnum, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public void TableExtensionTests_DeleteEntity_DeletesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertEntity(entity);

			table.DeleteEntity<TestTableEntity>(entity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.IsNull(storedEntity);
		}

		[TestMethod]
		public async Task TableExtensionTests_DeleteEntityAsync_DeletesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertEntityAsync(entity);
			await table.DeleteEntityAsync<TestTableEntity>(entity);

			var storedEntity = await table.RetrieveEntityAsync<TestTableEntity>(entity);
			Assert.IsNull(storedEntity);
		}

		[TestMethod]
		public void TableExtensionTests_MergeEntity_MergeOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertEntity(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.MergeEntity(updateEntity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual((byte)1, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(TestEnum.TestEnumValue2, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public async Task TableExtensionTests_MergeEntityAsync_MergesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertEntityAsync(entity);
			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.MergeEntityAsync(updateEntity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual((byte)1, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(TestEnum.TestEnumValue2, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public void TableExtensionTests_ReplaceEntity_ReplacesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertEntity(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.ReplaceEntity(updateEntity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual(null, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(null, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public async Task TableExtensionTests_ReplaceEntityAsync_ReplacesOk()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertEntityAsync(entity);
			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.ReplaceEntityAsync(updateEntity);

			var storedEntity = table.RetrieveEntity<TestTableEntity>(entity);
			Assert.AreEqual(null, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(null, storedEntity.NullableTestEnum);
		}

		[TestMethod]
		public void TableExtensionTests_RetrieveEntity_RetrievesFromExistingEntity()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();	

			table.InsertEntity(entity);

			var retrievedEntity = table.RetrieveEntity(entity);
			Assert.AreEqual(entity.RowKey, entity.RowKey);
			Assert.AreEqual(entity.PartitionKey, entity.PartitionKey);
			Assert.AreEqual(entity.NullableTestEnum, entity.NullableTestEnum);
			Assert.AreEqual(entity.TestDecimal, entity.TestDecimal);
			Assert.AreEqual(entity.TestEnum, entity.TestEnum);
			Assert.AreEqual(entity.TestInt16, entity.TestInt16);
			Assert.AreEqual(entity.TestNullableBool, entity.TestNullableBool);
			Assert.AreEqual(entity.TestNullableByte, entity.TestNullableByte);
			Assert.AreEqual(entity.TestNullableDateTime, entity.TestNullableDateTime);
			Assert.AreEqual(entity.TestNullableDateTimeOffset, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(entity.TestNullableDecimal, entity.TestNullableDecimal);
			Assert.AreEqual(entity.TestNullableDouble, entity.TestNullableDouble);
			Assert.AreEqual(entity.TestNullableInt16, entity.TestNullableInt16);
			Assert.AreEqual(entity.TestNullableInt32, entity.TestNullableInt32);
			Assert.AreEqual(entity.TestNullableInt64, entity.TestNullableInt64);
			Assert.AreEqual(entity.TestNullableSingle, entity.TestNullableSingle);
		}

		[TestMethod]
		public void TableExtensionTests_RetrieveEntity_RetrievesFromPartitionAndRowKeys()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			table.InsertEntity(entity);

			var retrievedEntity = table.RetrieveEntity<TestTableEntity>(entity.PartitionKey, entity.RowKey);
			Assert.AreEqual(entity.RowKey, entity.RowKey);
			Assert.AreEqual(entity.PartitionKey, entity.PartitionKey);
			Assert.AreEqual(entity.NullableTestEnum, entity.NullableTestEnum);
			Assert.AreEqual(entity.TestDecimal, entity.TestDecimal);
			Assert.AreEqual(entity.TestEnum, entity.TestEnum);
			Assert.AreEqual(entity.TestInt16, entity.TestInt16);
			Assert.AreEqual(entity.TestNullableBool, entity.TestNullableBool);
			Assert.AreEqual(entity.TestNullableByte, entity.TestNullableByte);
			Assert.AreEqual(entity.TestNullableDateTime, entity.TestNullableDateTime);
			Assert.AreEqual(entity.TestNullableDateTimeOffset, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(entity.TestNullableDecimal, entity.TestNullableDecimal);
			Assert.AreEqual(entity.TestNullableDouble, entity.TestNullableDouble);
			Assert.AreEqual(entity.TestNullableInt16, entity.TestNullableInt16);
			Assert.AreEqual(entity.TestNullableInt32, entity.TestNullableInt32);
			Assert.AreEqual(entity.TestNullableInt64, entity.TestNullableInt64);
			Assert.AreEqual(entity.TestNullableSingle, entity.TestNullableSingle);
		}

		[TestMethod]
		public async Task TableExtensionTests_RetrieveEntityAsync_RetrievesFromEntity()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertEntityAsync(entity);

			var retrievedEntity = await table.RetrieveEntityAsync(entity);
			Assert.AreEqual(entity.RowKey, entity.RowKey);
			Assert.AreEqual(entity.PartitionKey, entity.PartitionKey);
			Assert.AreEqual(entity.NullableTestEnum, entity.NullableTestEnum);
			Assert.AreEqual(entity.TestDecimal, entity.TestDecimal);
			Assert.AreEqual(entity.TestEnum, entity.TestEnum);
			Assert.AreEqual(entity.TestInt16, entity.TestInt16);
			Assert.AreEqual(entity.TestNullableBool, entity.TestNullableBool);
			Assert.AreEqual(entity.TestNullableByte, entity.TestNullableByte);
			Assert.AreEqual(entity.TestNullableDateTime, entity.TestNullableDateTime);
			Assert.AreEqual(entity.TestNullableDateTimeOffset, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(entity.TestNullableDecimal, entity.TestNullableDecimal);
			Assert.AreEqual(entity.TestNullableDouble, entity.TestNullableDouble);
			Assert.AreEqual(entity.TestNullableInt16, entity.TestNullableInt16);
			Assert.AreEqual(entity.TestNullableInt32, entity.TestNullableInt32);
			Assert.AreEqual(entity.TestNullableInt64, entity.TestNullableInt64);
			Assert.AreEqual(entity.TestNullableSingle, entity.TestNullableSingle);
		}

		[TestMethod]
		public async Task TableExtensionTests_RetrieveEntityAsync_RetrievesFromPartitionAndRowKeys()
		{
			var entity = new TestTableEntity();
			entity.PartitionKey = System.Guid.NewGuid().ToString();
			entity.RowKey = System.Guid.NewGuid().ToString();
			entity.TestNullableByte = 1;
			entity.TestEnum = TestEnum.TestEnumValue;
			entity.NullableTestEnum = TestEnum.TestEnumValue2;

			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var tableClient = account.CreateCloudTableClient();
			var table = tableClient.GetTableReference("TestEntities");
			table.DeleteIfExists();
			table.Create();

			await table.InsertEntityAsync(entity);

			var retrievedEntity = await table.RetrieveEntityAsync<TestTableEntity>(entity.PartitionKey, entity.RowKey);
			Assert.AreEqual(entity.RowKey, entity.RowKey);
			Assert.AreEqual(entity.PartitionKey, entity.PartitionKey);
			Assert.AreEqual(entity.NullableTestEnum, entity.NullableTestEnum);
			Assert.AreEqual(entity.TestDecimal, entity.TestDecimal);
			Assert.AreEqual(entity.TestEnum, entity.TestEnum);
			Assert.AreEqual(entity.TestInt16, entity.TestInt16);
			Assert.AreEqual(entity.TestNullableBool, entity.TestNullableBool);
			Assert.AreEqual(entity.TestNullableByte, entity.TestNullableByte);
			Assert.AreEqual(entity.TestNullableDateTime, entity.TestNullableDateTime);
			Assert.AreEqual(entity.TestNullableDateTimeOffset, entity.TestNullableDateTimeOffset);
			Assert.AreEqual(entity.TestNullableDecimal, entity.TestNullableDecimal);
			Assert.AreEqual(entity.TestNullableDouble, entity.TestNullableDouble);
			Assert.AreEqual(entity.TestNullableInt16, entity.TestNullableInt16);
			Assert.AreEqual(entity.TestNullableInt32, entity.TestNullableInt32);
			Assert.AreEqual(entity.TestNullableInt64, entity.TestNullableInt64);
			Assert.AreEqual(entity.TestNullableSingle, entity.TestNullableSingle);
		}

	}
}