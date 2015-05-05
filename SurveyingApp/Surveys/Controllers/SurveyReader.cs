using System;
using System.Reflection;
using System.IO;

namespace Surveys
{
	static class SurveyReader
	{
		public static Survey ReadSurveyFromResource(string resourceName)
		{
			Assembly assembly = typeof(SurveyReader).GetTypeInfo ().Assembly;
			string json = null;

			using (Stream stream = assembly.GetManifestResourceStream (resourceName))
			{
				stream.Position = 0;
				using (StreamReader reader = new StreamReader(stream))
				{
					json = reader.ReadToEnd ();
				}
			}

			Survey ns = (Survey) JSonTranslator.Deserialize (json);
			return ns;
		}
	}
}

