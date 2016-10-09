using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerulean.Storage
{
	/// <summary>
	/// Represents a mechanism for serializing data of a specific type to a string for storage in an Azure table.
	/// </summary>
	public class TableEntityFieldSerializer
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="TableEntityFieldSerializer"/> class.
		/// </summary>
		/// <param name="supportedType">The <see cref="Type"/> that can be (de)serialized by this class.</param>
		/// <param name="serialize">The serialize delegate.</param>
		/// <param name="deserialize">The deserialize delegate.</param>
		/// <exception cref="ArgumentNullException">Thrown if any of the arguments are null.</exception>
		public TableEntityFieldSerializer(Type supportedType, Func<object, string> serialize, Func<string, object> deserialize)
		{
			if (supportedType == null) throw new ArgumentNullException(nameof(supportedType));
			if (serialize == null) throw new ArgumentNullException(nameof(Serialize));
			if (deserialize == null) throw new ArgumentNullException(nameof(deserialize));

			SupportedType = supportedType;
			Serialize = serialize;
			Deserialize = deserialize;
		}

		/// <summary>
		/// Gets or sets a <see cref="Type"/> reference indicating the .Net type handled by this serializer. 
		/// </summary>
		public Type SupportedType { get; set; }
		/// <summary>
		/// Gets or sets a delegate used to convert a value into a string for storage.
		/// </summary>
		/// <value>The serialize.</value>
		public Func<object, string> Serialize { get; protected set; }
		/// <summary>
		/// Gets or sets a delegate used to turn a string previously created by <see cref="Serialize"/> into a value of the type handled by this serializer.
		/// </summary>
		/// <value>The deserialize.</value>
		public Func<string, object> Deserialize { get; protected set; }
	}
}