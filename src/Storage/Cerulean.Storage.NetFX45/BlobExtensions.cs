using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	/// <summary>
	/// Provides extension methods for <see cref="Microsoft.WindowsAzure.Storage.Blob.CloudBlob"/>.
	/// </summary>
	public static class BlobExtensions
	{

		/// <summary>
		/// Gets or sets the retry policy used by extension methods provided by this class.
		/// </summary>
		/// <remarks><para>If not explicitly set, a default <see cref="Microsoft.WindowsAzure.Storage.RetryPolicies.ExponentialRetry"/> instance is used.</para></remarks>
		/// <value>The default retry policy.</value>
		public static Microsoft.WindowsAzure.Storage.RetryPolicies.IRetryPolicy DefaultRetryPolicy
		{
			get; set;
		} = new Microsoft.WindowsAzure.Storage.RetryPolicies.ExponentialRetry(TimeSpan.FromMilliseconds(100), 10);

		#region UploadBlockBlobFromStreamAsync

		/// <summary>
		/// Uploads a stream to a block blob asynchronously.
		/// </summary>
		/// <param name="blobClient">A blob client instance.</param>
		/// <param name="containerName">The name of the blob container for the blob.</param>
		/// <param name="blobName">The name of the blob within the container.</param>
		/// <param name="stream">The stream to upload data from.</param>
		/// <returns>A Task&lt;CloudBlob&gt; instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="blobClient"/>, <paramref name="containerName"/>, <paramref name="blobName"/> or <paramref name="stream"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="containerName"/> or <paramref name="blobName"/> is an empty string.</exception>
		public static Task<CloudBlob> UploadBlockBlobFromStreamAsync(this Microsoft.WindowsAzure.Storage.Blob.CloudBlobClient blobClient, string containerName, string blobName, System.IO.Stream stream)
		{
			if (blobClient == null) throw new ArgumentNullException(nameof(blobClient));
			if (containerName == null) throw new ArgumentNullException(nameof(containerName));
			if (String.IsNullOrEmpty(containerName)) throw new ArgumentException("Empty string is not allowed.", nameof(containerName));

			var container = blobClient.GetContainerReference(containerName);
			return UploadBlockBlobFromStreamAsync(container, blobName, stream);
		}

		/// <summary>
		/// Uploads a stream to a block blob asynchronously.
		/// </summary>
		/// <param name="container">The <see cref="Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer"/> to contain the blob.</param>
		/// <param name="blobName">The name of the blob within the container.</param>
		/// <param name="stream">The stream to upload data from.</param>
		/// <returns>A Task&lt;CloudBlob&gt; instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="container"/>, <paramref name="blobName"/> or <paramref name="stream"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="blobName"/> is an empty string.</exception>
		public static Task<CloudBlob> UploadBlockBlobFromStreamAsync(this Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer container, string blobName, System.IO.Stream stream)
		{
			return UploadBlockBlobFromStreamAsync(container, blobName, stream, null, null);
		}

		/// <summary>
		/// Uploads a stream to a block blob asynchronously.
		/// </summary>
		/// <param name="container">The <see cref="Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer"/> to contain the blob.</param>
		/// <param name="blobName">The name of the blob within the container.</param>
		/// <param name="stream">The stream to upload data from.</param>
		/// <param name="contentEncoding">A string to be applied as the blob content encoding property. Can be null.</param>
		/// <param name="contentType">A string to be applied as the blob content type property. Can be null.</param>
		/// <returns>A Task&lt;CloudBlob&gt; instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="container"/>, <paramref name="blobName"/> or <paramref name="stream"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="blobName"/> is an empty string.</exception>
		public static Task<CloudBlob> UploadBlockBlobFromStreamAsync(this Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer container, string blobName, System.IO.Stream stream, string contentType, string contentEncoding)
		{
			return UploadBlockBlobFromStreamAsync(container, blobName, stream, contentType, contentEncoding, 
				new Microsoft.WindowsAzure.Storage.Blob.BlobRequestOptions()
				{
					SingleBlobUploadThresholdInBytes = 1048576,
					ParallelOperationThreadCount = 4,
					RetryPolicy = DefaultRetryPolicy?.CreateInstance()
				}
			);
		}

		/// <summary>
		/// Uploads a stream to a block blob asynchronously.
		/// </summary>
		/// <param name="container">The <see cref="Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer"/> to contain the blob.</param>
		/// <param name="blobName">The name of the blob within the container.</param>
		/// <param name="stream">The stream to upload data from.</param>
		/// <param name="contentEncoding">A string to be applied as the blob content encoding property. Can be null.</param>
		/// <param name="contentType">A string to be applied as the blob content type property. Can be null.</param>
		/// <param name="options">A <see cref="Microsoft.WindowsAzure.Storage.Blob.BlobRequestOptions"/> instance containing additional options for how the blob should be uploaded.</param>
		/// <returns>A Task&lt;CloudBlob&gt; instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="container"/>, <paramref name="blobName"/> or <paramref name="stream"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="blobName"/> is an empty string.</exception>
		public static async Task<CloudBlob> UploadBlockBlobFromStreamAsync(this Microsoft.WindowsAzure.Storage.Blob.CloudBlobContainer container, string blobName, System.IO.Stream stream, string contentType, string contentEncoding, BlobRequestOptions options)
		{
			if (blobName == null) throw new ArgumentNullException(nameof(blobName));
			if (String.IsNullOrEmpty(blobName)) throw new ArgumentException("Empty string is not allowed.", nameof(blobName));
			if (stream == null) throw new ArgumentNullException(nameof(stream));

			int retries = 0;
			while (retries < 3)
			{
				try
				{
					var blob = container.GetBlockBlobReference(blobName);

					if (!String.IsNullOrEmpty(contentType))
						blob.Properties.ContentType = contentType;

					if (!string.IsNullOrEmpty(contentEncoding))
						blob.Properties.ContentEncoding = contentEncoding;

					await blob.UploadFromStreamAsync(stream, null, options, null).ConfigureAwait(false);

					return blob;
				}
				catch (StorageException se)
				{
					var errorCode = se?.RequestInformation?.ExtendedErrorInformation?.ErrorCode;
					if ((errorCode ?? StorageErrorCodeStrings.ContainerNotFound) == StorageErrorCodeStrings.ContainerNotFound || se.RequestInformation.HttpStatusCode == (int)HttpStatusCode.NotFound)
					{
						await container.CreateIfNotExistsAsync().ConfigureAwait(false);
					}

					retries++;
				}
			}

			return null;
		}

		#endregion

		#region DeleteBlobAsync

		/// <summary>
		/// Deletes a blob asynchronously.
		/// </summary>
		/// <param name="blobClient">A <see cref="CloudBlobClient"/> instance.</param>
		/// <param name="containerName">The name of the container storing the blob.</param>
		/// <param name="blobName">The name of the blob to delete.</param>
		/// <returns>A Task instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="blobClient"/>, <paramref name="blobName"/> or <paramref name="containerName"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="containerName"/> or <paramref name="blobName"/> is an empty string.</exception>
		public static Task DeleteBlobAsync(this CloudBlobClient blobClient, string containerName, string blobName)
		{
			if (blobClient == null) throw new ArgumentNullException(nameof(blobClient));
			if (containerName == null) throw new ArgumentNullException(nameof(containerName));
			if (containerName.Length == 0 ) throw new ArgumentException("Empty string is not allowed.", nameof(containerName));
			if (blobName == null) throw new ArgumentNullException(nameof(blobName));
			if (blobName.Length == 0) throw new ArgumentException("Empty string is not allowed.", nameof(blobName));

			var container = blobClient.GetContainerReference(containerName);
			return DeleteBlobAsync(container, blobName);
		}

		/// <summary>
		/// Deletes a blob asynchronously.
		/// </summary>
		/// <param name="container">A <see cref="CloudBlobContainer"/> instance storing the blob.</param>
		/// <param name="blobName">The name of the blob to delete.</param>
		/// <returns>A Task instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="container"/> or <paramref name="blobName"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="blobName"/> is an empty string.</exception>
		public static async Task DeleteBlobAsync(CloudBlobContainer container, string blobName)
		{
			if (container == null) throw new ArgumentNullException(nameof(container));
			if (blobName == null) throw new ArgumentNullException(nameof(blobName));
			if (blobName.Length == 0) throw new ArgumentException("Empty string is not allowed.", nameof(blobName));

			var blob = container.GetBlobReference(blobName);
			await blob.DeleteAsync().ConfigureAwait(false);
		}

		#endregion

		#region DeleteBlobIfExistsAsync

		/// <summary>
		/// Deletes the blob asynchronously if exists.
		/// </summary>
		/// <param name="blobClient">A <see cref="CloudBlobClient"/> instance.</param>
		/// <param name="containerName">The name of the container storing the blob.</param>
		/// <param name="blobName">The name of the blob to delete.</param>
		/// <returns>A Task instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="blobClient"/>, <paramref name="containerName"/> or <paramref name="blobName"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="containerName"/> or <paramref name="blobName"/> is an empty string.</exception>
		public static Task DeleteBlobIfExistsAsync(this CloudBlobClient blobClient, string containerName, string blobName)
		{
			if (blobClient == null) throw new ArgumentNullException(nameof(blobClient));
			if (containerName == null) throw new ArgumentNullException(nameof(containerName));
			if (containerName.Length == 0) throw new ArgumentException("Empty string is not allowed.", nameof(containerName));
			if (blobName == null) throw new ArgumentNullException(nameof(blobName));
			if (blobName.Length == 0) throw new ArgumentException("Empty string is not allowed.", nameof(blobName));

			var container = blobClient.GetContainerReference(containerName);
			return DeleteBlobIfExistsAsync(container, blobName);
		}

		/// <summary>
		/// Deletes the blob asynchronously if exists.
		/// </summary>
		/// <param name="container">A <see cref="CloudBlobContainer"/> instance storing the blob.</param>
		/// <param name="blobName">The name of the blob to delete.</param>
		/// <returns>A Task instance.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="container"/> or <paramref name="blobName"/> is null.</exception>
		/// <exception cref="System.ArgumentException">Thrown if <paramref name="blobName"/> is an empty string.</exception>
		public static async Task DeleteBlobIfExistsAsync(this CloudBlobContainer container, string blobName)
		{
			if (container == null) throw new ArgumentNullException(nameof(container));
			if (blobName == null) throw new ArgumentNullException(nameof(blobName));
			if (blobName.Length == 0) throw new ArgumentException("Empty string is not allowed.", nameof(blobName));

			var blob = container.GetBlobReference(blobName);
			await blob.DeleteIfExistsAsync().ConfigureAwait(false);
		}

		#endregion

	}
}