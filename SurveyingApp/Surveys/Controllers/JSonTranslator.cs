using System;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Surveys
{
    static class JSonTranslator 
    {
        public static String Serialize(Object obj) 
        {
			return JsonConvert.SerializeObject (obj, Formatting.Indented, new JsonSerializerSettings {
				TypeNameHandling = TypeNameHandling.Objects,
				TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore
			});
        }
			

        public static T Deserialize<T>(String json)
        {
			return JsonConvert.DeserializeObject<T> (json, new JsonSerializerSettings {
				TypeNameHandling = TypeNameHandling.Objects
			});
        }

		public static T DeepClone<T>(T obj)
		{
			var temp = JSonTranslator.Serialize (obj);
			return JSonTranslator.Deserialize<T> (temp);
		}
    }
}
