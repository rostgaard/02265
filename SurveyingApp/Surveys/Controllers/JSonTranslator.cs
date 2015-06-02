using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Surveys
{
	/// <summary>
	/// Json translator for serializing and de-serializng.
	/// </summary>
    static class JSonTranslator 
    {
		/// <summary>
		/// Serialize the specified object.
		/// </summary>
		/// <param name="obj">Object to serialize.</param>
        public static String Serialize(Object obj) 
        {
			return JsonConvert.SerializeObject (obj, Formatting.Indented, new JsonSerializerSettings {
				TypeNameHandling = TypeNameHandling.Objects,
				TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
        }
			

		/// <summary>
		/// Deserialize the specified json.
		/// </summary>
		/// <param name="json">String containing JSON encoded object.</param>
		/// <typeparam name="T">The type of the object to serialize.</typeparam>
        public static T Deserialize<T>(String json)
        {
			return JsonConvert.DeserializeObject<T> (json, new JsonSerializerSettings {
				TypeNameHandling = TypeNameHandling.Objects
			});
        }

		/// <summary>
		/// Deeply clones object.
		/// </summary>
		/// <returns>The clone.</returns>
		/// <param name="obj">Object.</param>
		/// <typeparam name="T">The type of the object.</typeparam>
		public static T DeepClone<T>(T obj)
		{
			var temp = JSonTranslator.Serialize (obj);
			return JSonTranslator.Deserialize<T> (temp);
		}
    }
}
