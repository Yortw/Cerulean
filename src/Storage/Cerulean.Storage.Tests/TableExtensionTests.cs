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
		public void TableExtensionTests_Insert_InsertsOk()
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
		public async Task TableExtensionTests_InsertAsync_InsertsOk()
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
		public void TableExtensionTests_InsertOrReplace_InsertsOk()
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
		public async Task TableExtensionTests_InsertOrReplaceAsync_InsertsOk()
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
		public void TableExtensionTests_InsertOrReplace_ReplacesOk()
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
		public async Task TableExtensionTests_InsertOrReplaceAsync_ReplacesOk()
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
		public void TableExtensionTests_InsertOrMerge_InsertsOk()
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
		public async Task TableExtensionTests_InsertOrMergeAsync_InsertsOk()
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
		public void TableExtensionTests_InsertOrMerge_MergesOk()
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
		public async Task TableExtensionTests_InsertOrMergeAsync_MergesOk()
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
		public void TableExtensionTests_Delete_DeletesOk()
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
		public async Task TableExtensionTests_DeleteAsync_DeletesOk()
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
		public void TableExtensionTests_Merge_MergeOk()
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
		public async Task TableExtensionTests_MergeAsync_MergesOk()
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
		public void TableExtensionTests_Replace_ReplacesOk()
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
		public async Task TableExtensionTests_ReplaceAsync_ReplacesOk()
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

	}
}