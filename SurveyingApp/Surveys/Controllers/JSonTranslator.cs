using System;
using Newtonsoft.Json;

namespace Surveys
{
    static class JSonTranslator 
    {
        public static String Serialize(Survey obj) 
        {
			return JsonConvert.SerializeObject (obj, Formatting.Indented, new JsonSerializerSettings {
				TypeNameHandling = TypeNameHandling.Objects,
				TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple
			});
        }

        public static Survey Deserialize(String json)
        {
			return JsonConvert.DeserializeObject<Survey> (json, new JsonSerializerSettings {
				TypeNameHandling = TypeNameHandling.Objects
			});
        }
    }
}
