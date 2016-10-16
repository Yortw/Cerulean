using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cerulean.Storage;
using Microsoft.WindowsAzure.Storage;

namespace Cerulean.Storage.Tests
{
	// NOTE: These tests require the Azure storage emulator to be started before being run.

	[TestClass]
	public class BlobExtensionsTest
	{

		[TestMethod]
		public async Task BlobExtensions_UploadBlockBlobFromStreamAsync_UploadsBlob()
		{
			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var blobClient = account.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference("testcontainer");
			var blob = container.GetBlobReference("testblob1");
			await blob.DeleteIfExistsAsync();

			using (var stream = System.IO.File.OpenRead(@"TestBlobContent.gif"))
			{
				blob = await blobClient.UploadBlockBlobFromStreamAsync("testcontainer", "testblob1", stream);
				Assert.IsNotNull(blob);
				Assert.IsTrue(await blob.ExistsAsync());
			}
		}

		[TestMethod]
		public async Task BlobExtensions_DeleteBlobAsync_DeletesBlob()
		{
			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var blobClient = account.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference("testcontainer");
			var blob = container.GetBlobReference("testblob1");
			await blob.DeleteIfExistsAsync();

			using (var stream = System.IO.File.OpenRead(@"TestBlobContent.gif"))
			{
				blob = await blobClient.UploadBlockBlobFromStreamAsync("testcontainer", "testblob1", stream);
			}

			await blobClient.DeleteBlobAsync("testcontainer", "testblob1");
			Assert.IsFalse(blob.Exists());
		}

		[ExpectedException(typeof(StorageException))]
		[TestMethod]
		public async Task BlobExtensions_DeleteBlobAsync_ThrowsOnBlobNotFound()
		{
			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var blobClient = account.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference("testcontainer");
			var blob = container.GetBlobReference("testblob1");
			await blob.DeleteIfExistsAsync();

			using (var stream = System.IO.File.OpenRead(@"TestBlobContent.gif"))
			{
				blob = await blobClient.UploadBlockBlobFromStreamAsync("testcontainer", "testblob1", stream);
			}

			await blobClient.DeleteBlobAsync("testcontainer", "testblob1");
			await blobClient.DeleteBlobAsync("testcontainer", "testblob1");
			Assert.IsFalse(blob.Exists());
		}

		[TestMethod]
		public async Task BlobExtensions_DeleteBlobIfExistsAsync_DeletesBlob()
		{
			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var blobClient = account.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference("testcontainer");
			var blob = container.GetBlobReference("testblob1");
			await blob.DeleteIfExistsAsync();

			using (var stream = System.IO.File.OpenRead(@"TestBlobContent.gif"))
			{
				blob = await blobClient.UploadBlockBlobFromStreamAsync("testcontainer", "testblob1", stream);
			}

			await blobClient.DeleteBlobIfExistsAsync("testcontainer", "testblob1");
			Assert.IsFalse(blob.Exists());
		}

		[TestMethod]
		public async Task BlobExtensions_DeleteBlobIfExistsAsync_DoesNotThrowOnBlobNotFound()
		{
			var account = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
			var blobClient = account.CreateCloudBlobClient();
			var container = blobClient.GetContainerReference("testcontainer");
			var blob = container.GetBlobReference("testblob1");
			await blob.DeleteIfExistsAsync();

			using (var stream = System.IO.File.OpenRead(@"TestBlobContent.gif"))
			{
				blob = await blobClient.UploadBlockBlobFromStreamAsync("testcontainer", "testblob1", stream);
			}

			await blobClient.DeleteBlobIfExistsAsync("testcontainer", "testblob1");
			await blobClient.DeleteBlobIfExistsAsync("testcontainer", "testblob1");
			Assert.IsFalse(blob.Exists());
		}

	}
}