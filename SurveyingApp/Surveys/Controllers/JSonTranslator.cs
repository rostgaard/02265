using System;
using Newtonsoft.Json;

namespace Surveys
{
    static class JSonTranslator
    {
        public static String serialize(Object obj) 
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static Object deserialize(String json, System.Type clazz)
        {
            return JsonConvert.DeserializeObject(json, clazz);
        }
    }
}
