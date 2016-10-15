using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	/// <summary>
	/// Provides extension methods for <see cref="Microsoft.WindowsAzure.Storage.Table.CloudTable"/>.
	/// </summary>
	public static class TableExtensions
	{

		#region Insert

		/// <summary>
		/// Inserts the specified entity into the table.
		/// </summary>
		/// <typeparam name="T">The type of entity to insert.</typeparam>
		/// <param name="table">The table to insert into.</param>
		/// <param name="entity">The entity to insert.</param>
		/// <returns>A <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		public static TableResult InsertEntity<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));

			var op = TableOperation.Insert(entity, false);
			return table.Execute(op);
		}

		/// <summary>
		/// Inserts the specified entity into the table as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T">The type of entity to insert.</typeparam>
		/// <param name="table">The table to insert into.</param>
		/// <param name="entity">The entity to insert.</param>
		/// <returns>A <see cref="Task{TResult}"/> whose result is a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		public static async Task<TableResult> InsertEntityAsync<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));

			var op = TableOperation.Insert(entity, false);
			return await table.ExecuteAsync(op).ConfigureAwait(false);
		}

		#region InsertOrMerge

		/// <summary>
		/// Inserts the specified entity into the table if it doesn't exist, otherwise performs a merge.
		/// </summary>
		/// <typeparam name="T">The type of entity to insert or merge.</typeparam>
		/// <param name="table">The table to insert or merge into.</param>
		/// <param name="entity">The entity to insert or merge.</param>
		/// <returns>A <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> or <paramref name="entity"/> is null.</exception>
		/// <seealso cref="MergeEntity{T}(CloudTable, T)"/>
		public static TableResult InsertOrMergeEntity<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);
			var op = TableOperation.InsertOrMerge(entity);
			return table.Execute(op);
		}

		/// <summary>
		/// Inserts the specified entity into the table if it doesn't exist, otherwise performs a merge as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T">The type of entity to insert or merge.</typeparam>
		/// <param name="table">The table to insert or merge into.</param>
		/// <param name="entity">The entity to insert or merge.</param>
		/// <returns>A <see cref="Task{TResult}"/> whose result is a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		/// <seealso cref="MergeEntityAsync{T}(CloudTable, T)"/>
		public static async Task<TableResult> InsertOrMergeEntityAsync<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);

			var op = TableOperation.InsertOrMerge(entity);
			return await table.ExecuteAsync(op).ConfigureAwait(false);
		}

		#endregion

		#region InsertOrReplace

		/// <summary>
		/// Inserts an entity into the specified table, or if it already exists performs a replace.
		/// </summary>
		/// <typeparam name="T">The type of entity to insert or replace.</typeparam>
		/// <param name="table">The table to insert or replace into.</param>
		/// <param name="entity">The entity to insert or replace.</param>
		/// <returns>A <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> or <paramref name="entity"/> is null.</exception>
		/// <seealso cref="ReplaceEntity{T}(CloudTable, T)"/>
		public static TableResult InsertOrReplaceEntity<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);
			var op = TableOperation.InsertOrReplace(entity);
			return table.Execute(op);
		}

		/// <summary>
		/// Inserts an entity into the specified table, or if it already exists performs a replace as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T">The type of entity to insert or replace.</typeparam>
		/// <param name="table">The table to insert or replace into.</param>
		/// <param name="entity">The entity to insert or replace.</param>
		/// <returns>A <see cref="Task{TResult}"/> whose result is a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		public static async Task<TableResult> InsertOrReplaceEntityAsync<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);

			var op = TableOperation.InsertOrReplace(entity);
			return await table.ExecuteAsync(op).ConfigureAwait(false);
		}

		#endregion
		
		#endregion

		#region Merge

		/// <summary>
		/// Merges the specified entity into the table.
		/// </summary>
		/// <remarks>
		/// <para>A merge operation is an 'update' where only properties that are not null are updated in the table. Properties that are null are not modified in storage.</para>
		/// </remarks>
		/// <typeparam name="T">The type of entity to merge.</typeparam>
		/// <param name="table">The table to merge into.</param>
		/// <param name="entity">The entity to merge.</param>
		/// <returns>A <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> or <paramref name="entity"/> is null.</exception>
		public static TableResult MergeEntity<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);
			var op = TableOperation.Merge(entity);
			return table.Execute(op);
		}

		/// <summary>
		/// Merges the specified entity into the table as an asynchronous operation.
		/// </summary>
		/// <remarks>
		/// <para>A merge operation is an 'update' where only properties that are not null are updated in the table. Properties that are null are not modified in storage.</para>
		/// </remarks>
		/// <typeparam name="T">The type of entity to merge.</typeparam>
		/// <param name="table">The table to merge into.</param>
		/// <param name="entity">The entity to merge.</param>
		/// <returns>A <see cref="Task{TResult}"/> whose result is a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		public static async Task<TableResult> MergeEntityAsync<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);

			var op = TableOperation.Merge(entity);
			return await table.ExecuteAsync(op).ConfigureAwait(false);
		}

		#endregion

		#region Replace

		/// <summary>
		/// Replaces the specified entity in the table.
		/// </summary>
		/// <remarks>
		/// <para>A replace operation is an 'update' where all properties in the table are overwritten with the values from the new entity.</para>
		/// </remarks>
		/// <typeparam name="T">The type of entity to replace.</typeparam>
		/// <param name="table">The table to replace into.</param>
		/// <param name="entity">The entity to replace.</param>
		/// <returns>A <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> or <paramref name="entity"/> is null.</exception>
		public static TableResult ReplaceEntity<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);
			var op = TableOperation.Replace(entity);
			return table.Execute(op);
		}

		/// <summary>
		/// Replaces the specified entity in the table as an asynchronous operation.
		/// </summary>
		/// <remarks>
		/// <para>A replace operation is an 'update' where all properties in the table are overwritten with the values from the new entity.</para>
		/// </remarks>
		/// <typeparam name="T">The type of entity to replace.</typeparam>
		/// <param name="table">The table to replace into.</param>
		/// <param name="entity">The entity to replace.</param>
		/// <returns>A <see cref="Task{TResult}"/> whose result is a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		/// <exception cref="StorageException">If the operation is not successful then a <see cref="StorageException"/> is thrown, but only the <see cref="RequestResult.HttpStatusCode"/> will be set.</exception>
		public static async Task<TableResult> ReplaceEntityAsync<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (entity == null) throw new ArgumentNullException(nameof(entity));

			SetMissingEtag(entity);

			var op = TableOperation.Replace(entity);
			return await table.ExecuteAsync(op).ConfigureAwait(false);
		}

		#endregion

		#region Retrieve

		/// <summary>
		/// Retrieves an entity from table storage.
		/// </summary>
		/// <typeparam name="T">The type of entity to retrieve.</typeparam>
		/// <param name="table">The table to retrieve from.</param>
		/// <param name="entity">An existing entity whose <see cref="ITableEntity.PartitionKey"/> and <see cref="ITableEntity.RowKey"/> will be used to retrieve a fresh instance of the entity from storage.</param>
		/// <returns>Null if the entity was not found, otherwise a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> or <paramref name="entity"/> is null.</exception>
		/// <exception cref="StorageException">If the operation is not successful then a <see cref="StorageException"/> is thrown, but only the <see cref="RequestResult.HttpStatusCode"/> will be set.</exception>
		public static T RetrieveEntity<T>(this CloudTable table, T entity) where T : class, ITableEntity
		{
			return RetrieveEntity<T>(table, entity.PartitionKey, entity.RowKey);
		}

		/// <summary>
		/// Retrieves the specified entity from the table.
		/// </summary>
		/// <typeparam name="T">The type of entity to retrieve.</typeparam>
		/// <param name="table">The table to retrieve from.</param>
		/// <param name="partitionKey">The partition key of the entity to retrieve.</param>
		/// <param name="rowKey">The row key of the entity to retrieve.</param>
		/// <returns>Null if the entity was not found, otherwise a <see cref="Task{TResult}"/> whose result is the retrieved entity.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/>, <paramref name="partitionKey"/> or <paramref name="rowKey"/> is null.</exception>
		/// <exception cref="StorageException">If the operation is not successful then a <see cref="StorageException"/> is thrown, but only the <see cref="RequestResult.HttpStatusCode"/> will be set.</exception>
		public static T RetrieveEntity<T>(this CloudTable table, string partitionKey, string rowKey) where T : class, ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (partitionKey == null) throw new ArgumentNullException(nameof(partitionKey));
			if (rowKey == null) throw new ArgumentNullException(nameof(rowKey));

			var op = TableOperation.Retrieve<T>(partitionKey, rowKey);
			var result = table.Execute(op);
			if (result.HttpStatusCode == 404) return null;
			EnsureSuccess(result.HttpStatusCode);

			return (T)result.Result;
		}

		/// <summary>
		/// Retrieves the specified entity from the table as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T">The type of entity to retrieve.</typeparam>
		/// <param name="table">The table to retrieve from.</param>
		/// <param name="entity">An existing entity whose <see cref="ITableEntity.PartitionKey"/> and <see cref="ITableEntity.RowKey"/> will be used to retrieve a fresh instance of the entity from storage.</param>
		/// <returns>Null if the entity was not found, otherwise a <see cref="Task{TResult}"/> whose result is a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> or <paramref name="entity"/> is null.</exception>
		/// <exception cref="StorageException">If the operation is not successful then a <see cref="StorageException"/> is thrown, but only the <see cref="RequestResult.HttpStatusCode"/> will be set.</exception>
		public static async Task<T> RetrieveEntityAsync<T>(this CloudTable table, T entity) where T : class, ITableEntity
		{
			return await RetrieveEntityAsync<T>(table, entity.PartitionKey, entity.RowKey).ConfigureAwait(false);
		}

		/// <summary>
		/// Retrieves the specified entity from the table as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T">The type of entity to retrieve.</typeparam>
		/// <param name="table">The table to retrieve from.</param>
		/// <param name="partitionKey">The partition key of the entity to retrieve.</param>
		/// <param name="rowKey">The row key of the entity to retrieve.</param>
		/// <returns>Null if the entity was not found, otherwise a <see cref="Task{TResult}"/> whose result is the retrieved entity.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/>, <paramref name="partitionKey"/> or <paramref name="rowKey"/> is null.</exception>
		/// <exception cref="StorageException">If the operation is not successful then a <see cref="StorageException"/> is thrown, but only the <see cref="RequestResult.HttpStatusCode"/> will be set.</exception>
		public static async Task<T> RetrieveEntityAsync<T>(this CloudTable table, string partitionKey, string rowKey) where T : class, ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));
			if (partitionKey == null) throw new ArgumentNullException(nameof(partitionKey));
			if (rowKey == null) throw new ArgumentNullException(nameof(rowKey));

			var op = TableOperation.Retrieve<T>(partitionKey, rowKey);
			var result = await table.ExecuteAsync(op);
			if (result.HttpStatusCode == 404) return null;
			EnsureSuccess(result.HttpStatusCode);

			return (T)result.Result;
		}

		#endregion

		#region Delete

		/// <summary>
		/// Deletes the specified entity in the table.
		/// </summary>
		/// <typeparam name="T">The type of entity to delete.</typeparam>
		/// <param name="table">The table to delete into.</param>
		/// <param name="entity">The entity to delete.</param>
		/// <returns>A <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		public static TableResult DeleteEntity<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));

			var op = TableOperation.Delete(entity);
			return table.Execute(op);
		}

		/// <summary>
		/// Deletes the specified entity from the table as an asynchronous operation.
		/// </summary>
		/// <typeparam name="T">The type of entity to delete.</typeparam>
		/// <param name="table">The table to delete into.</param>
		/// <param name="entity">The entity to delete.</param>
		/// <returns>A <see cref="Task{TResult}"/> whose result is a <see cref="TableResult"/> with the result of the operation.</returns>
		/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="table"/> is null.</exception>
		public static async Task<TableResult> DeleteEntityAsync<T>(this CloudTable table, T entity) where T : ITableEntity
		{
			if (table == null) throw new ArgumentNullException(nameof(table));

			var op = TableOperation.Delete(entity);
			return await table.ExecuteAsync(op).ConfigureAwait(false);
		}

		#endregion

		private static void EnsureSuccess(int httpStatusCode)
		{
			if (httpStatusCode < 200 || httpStatusCode > 299)
				throw new StorageException(new RequestResult() { HttpStatusCode = httpStatusCode }, "Storage operation returned: " + httpStatusCode.ToString(System.Globalization.CultureInfo.InvariantCulture), null);
		}

		private static void SetMissingEtag(ITableEntity entity)
		{
			if (String.IsNullOrEmpty(entity.ETag)) entity.ETag = "*";
		}

	}
}