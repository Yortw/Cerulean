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
	/// A custom base class for table storage entities that can serialise decimals, nullable decimals and enum values.
	/// </summary>
	public abstract class CustomSerializingTableEntity : TableEntity
	{
		private Dictionary<Type, TableEntityFieldSerializer> _Serializers;

		protected CustomSerializingTableEntity(IEnumerable<TableEntityFieldSerializer> serializers)
		{
			_Serializers = new Dictionary<Type, TableEntityFieldSerializer>();
			if (serializers == null) return;

			foreach (var serializer in serializers)
			{
				_Serializers.Add(serializer.SupportedType, serializer);
			}
		}

		/// <summary>
		/// Reads custom serialised fields.
		/// </summary>
		/// <param name="properties"></param>
		/// <param name="operationContext"></param>
		public override void ReadEntity(IDictionary<string, EntityProperty> properties, OperationContext operationContext)
		{
			base.ReadEntity(properties, operationContext);

			var thisType = this.GetType();
			var typeProperties = thisType.GetProperties().Where((tp) => _Serializers.ContainsKey(tp.PropertyType));

			foreach (var typeProperty in typeProperties)
			{
				if (!properties.ContainsKey(typeProperty.Name)) continue;

				var serialisedProperty = properties[typeProperty.Name];
				var serializer = _Serializers[typeProperty.PropertyType];

				typeProperty.SetValue(this, serializer.Deserialize(serialisedProperty.StringValue));
			}
		}

		/// <summary>
		/// Writes custom serialised fields.
		/// </summary>
		/// <param name="operationContext"></param>
		/// <returns></returns>
		public override IDictionary<string, EntityProperty> WriteEntity(OperationContext operationContext)
		{
			var entityProperties = base.WriteEntity(operationContext);
			var objectProperties = GetType().GetProperties().Where((tp) => _Serializers.ContainsKey(tp.PropertyType)); 

			foreach (var item in objectProperties)
			{
				if (entityProperties.ContainsKey(item.Name)) continue;

				var serializer = _Serializers[item.PropertyType];
				var value = item.GetValue(this, null);
				if (value != null)
				{
					entityProperties.Add(item.Name, new EntityProperty(serializer.Serialize(value)));
				}
			}

			return entityProperties;
		}

		private static bool IsDecimalOrNullableDecimal(Type propertyType)
		{
			return propertyType == typeof(decimal)
				|| propertyType == typeof(decimal?);
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
	}
}
