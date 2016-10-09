using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	/// <summary>
	/// A custom entity base class for table storage entities that can serialise various common primitive types not natively supported, including enums and decimals.
	/// </summary>
	/// <remarks>
	/// <para>To use this class, inherit from it and add your custom properties (just like inheriting from <see cref="TableEntity"/>. Properties of the supported types will automatically be handled.</para>
	/// <para>
	/// Supports the following list of types
	/// <list type="Bullet">
	/// <item>Decimal</item>
	/// <item>Int16</item>
	/// <item>Byte</item>
	/// <item>Floats</item>
	/// <item>Enums</item>
	/// <item>Nullable{T} where T is any type implementing <see cref="IFormattable"/> (including all of the aforementioned types)</item>
	/// </list>
	/// </para>
	/// </remarks>
	public abstract class PrimitiveSerializingTableEntity : TableEntity
	{

		private static System.Collections.Concurrent.ConcurrentDictionary<Type, IEnumerable<CachedPropertyDetails>> _CachedEntityDetails = new System.Collections.Concurrent.ConcurrentDictionary<Type, IEnumerable<CachedPropertyDetails>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="PrimitiveSerializingTableEntity"/> with the reflection cache enabled.
		/// </summary>
		protected PrimitiveSerializingTableEntity() : this(true)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PrimitiveSerializingTableEntity"/> class allowing you to specify if reflection caching is enabled.
		/// </summary>
		/// <param name="useReflectionCache">If set to <c>true</c> this instance uses the reflection cache.</param>
		/// <seealso cref="UseReflectionCache"/>
		protected PrimitiveSerializingTableEntity(bool useReflectionCache)
		{
			this.UseReflectionCache = useReflectionCache;
		}

		/// <summary>
		/// Gets or sets a value indicating whether to use the reflection cache.
		/// </summary>
		/// <remarks>
		/// <para>If true, details about properties on each derived table entity are cached internally. This improves performance and reduces 
		/// the number of allocations when more than one operation using an entity type is performed in a process. However, it consumes a little 
		/// more memory and could cause issues in some advanced scenarios where types are modified at runtime.</para>
		/// </remarks>
		/// <value><c>true</c> if the reflection cache will be used; otherwise, <c>false</c>.</value>
		protected bool UseReflectionCache { get; set; }

		/// <summary>
		/// Overrides &amp; calls the base method, as well as reading custom serialised fields.
		/// </summary>
		/// <param name="properties">The list of properties stored in the table row.</param>
		/// <param name="operationContext">The context of the table operation being performed.</param>
		public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
		{
			if (properties == null) return;

			base.ReadEntity(properties, operationContext);

			var thisType = this.GetType();
			var typeProperties = GetPropertiesRequiringCustomSerialization(thisType);

			foreach (var cachedTypeProperty in typeProperties)
			{
				//Skip properties that aren't in the serialized data
				if (!properties.ContainsKey(cachedTypeProperty.PropertyDetails.Name)) continue;

				//Skip properties already deserialized for us.
				var serialisedProperty = properties[cachedTypeProperty.PropertyDetails.Name];
				if (serialisedProperty.PropertyType != EdmType.String) continue;

				object value = null;
				if (!String.IsNullOrEmpty(serialisedProperty.StringValue))
				{
					if (IsEnumOrNullableEnum(cachedTypeProperty.PropertyDetails.PropertyType))
					{
						var enumType = cachedTypeProperty.PropertyDetails.PropertyType;
						if (!enumType.IsEnum)
							enumType = enumType.GenericTypeArguments[0];

						value = Enum.Parse(enumType, serialisedProperty.StringValue);
					}
					else if (cachedTypeProperty.NullableOfType != null)
					{
						value = System.Convert.ChangeType(serialisedProperty.StringValue, cachedTypeProperty.PropertyDetails.PropertyType.GenericTypeArguments[0], System.Globalization.CultureInfo.InvariantCulture);
					}
					else
					{
						value = System.Convert.ChangeType(serialisedProperty.StringValue, cachedTypeProperty.PropertyDetails.PropertyType, System.Globalization.CultureInfo.InvariantCulture);
					}
				}

				cachedTypeProperty.PropertyDetails.SetValue(this, value);
			}
		}

		/// <summary>
		/// Overrides &amp; calls the base method, as well as writing custom serialised fields.
		/// </summary>
		/// <param name="operationContext">The context of the table operation being performed.</param>
		/// <returns>A dictionary of <see cref="EntityProperty"/> instances keyed by property name, to be written to table storage.</returns>
		public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
		{
			var entityProperties = base.WriteEntity(operationContext);
			var objectProperties = GetPropertiesRequiringCustomSerialization(this.GetType());

			foreach (var item in objectProperties)
			{
				// Skip properties already serialized for us.
				if (entityProperties.ContainsKey(item.PropertyDetails.Name)) continue;

				var value = item.PropertyDetails.GetValue(this, null);
				if (value != null)
				{
					var formattable = value as IFormattable;
					if (formattable != null)
						entityProperties.Add(item.PropertyDetails.Name, new EntityProperty(formattable.ToString(null, System.Globalization.CultureInfo.InvariantCulture)));
					else
						entityProperties.Add(item.PropertyDetails.Name, new EntityProperty(value?.ToString()));
				}
			}

			return entityProperties;
		}

		private IEnumerable<CachedPropertyDetails> GetPropertiesRequiringCustomSerialization(Type thisEntityType)
		{
			IEnumerable<CachedPropertyDetails> propertyDetails = null;
			if (UseReflectionCache && _CachedEntityDetails.TryGetValue(thisEntityType, out propertyDetails))
				return propertyDetails;

			propertyDetails = 
			(
				from
				pi in
				thisEntityType.GetProperties()
					where IsEnumOrNullableEnum(pi.PropertyType)
						|| pi.PropertyType == typeof(decimal)
						|| pi.PropertyType == typeof(Int16)
						|| pi.PropertyType == typeof(byte)
						|| pi.PropertyType == typeof(float)
						|| (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
					select new CachedPropertyDetails()
					{
						NullableOfType = (pi.PropertyType.IsConstructedGenericType ? pi.PropertyType.GenericTypeArguments[0] : null),
						PropertyDetails = pi
				}
			);

			if (UseReflectionCache)
				_CachedEntityDetails.TryAdd(thisEntityType, propertyDetails);

			return propertyDetails;
		}

		private static bool IsEnumOrNullableEnum(Type propertyType)
		{
			return propertyType.IsEnum
				||
				(
					propertyType.IsConstructedGenericType
					&& propertyType.GenericTypeArguments[0].IsEnum
				);
		}

		private class CachedPropertyDetails
		{
			public PropertyInfo PropertyDetails { get; set; }
			public Type NullableOfType { get; set; }
		}
	}
}