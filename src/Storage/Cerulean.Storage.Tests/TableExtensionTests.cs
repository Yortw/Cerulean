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

			table.Insert(entity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertAsync(entity);

			var storedEntity = await table.RetrieveAsync<TestTableEntity>(entity);
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

			table.InsertOrReplace(entity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertOrReplaceAsync(entity);

			var storedEntity = await table.RetrieveAsync<TestTableEntity>(entity);
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

			table.InsertOrReplace(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.InsertOrReplace(updateEntity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertOrReplaceAsync(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.InsertOrReplaceAsync(updateEntity);

			var storedEntity = await table.RetrieveAsync<TestTableEntity>(entity);
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

			table.InsertOrMerge(entity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertOrMergeAsync(entity);

			var storedEntity = await table.RetrieveAsync<TestTableEntity>(entity);
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

			table.InsertOrMerge(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.InsertOrMerge(updateEntity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertOrMergeAsync(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.InsertOrMergeAsync(updateEntity);

			var storedEntity = await table.RetrieveAsync<TestTableEntity>(entity);
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

			table.Insert(entity);

			table.Delete<TestTableEntity>(entity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertAsync(entity);
			await table.DeleteAsync<TestTableEntity>(entity);

			var storedEntity = await table.RetrieveAsync<TestTableEntity>(entity);
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

			table.Insert(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.Merge(updateEntity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertAsync(entity);
			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.MergeAsync(updateEntity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			table.Insert(entity);

			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			table.Replace(updateEntity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
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

			await table.InsertAsync(entity);
			var updateEntity = new TestTableEntity()
			{
				PartitionKey = entity.PartitionKey,
				RowKey = entity.RowKey,
				TestEnum = TestEnum.None
			};
			await table.ReplaceAsync(updateEntity);

			var storedEntity = table.Retrieve<TestTableEntity>(entity);
			Assert.AreEqual(null, storedEntity.TestNullableByte);
			Assert.AreEqual(TestEnum.None, storedEntity.TestEnum);
			Assert.AreEqual(null, storedEntity.NullableTestEnum);
		}

	}
}