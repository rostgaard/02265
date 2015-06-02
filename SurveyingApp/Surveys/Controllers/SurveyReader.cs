using System;
using System.Reflection;
using System.IO;

namespace Surveys
{
	/// <summary>
	/// Survey reader.
	/// </summary>
	static class SurveyReader
	{
		/// <summary>
		/// Reads the survey from resource.
		/// </summary>
		/// <returns>The survey from resource.</returns>
		/// <param name="resourceName">Resource name.</param>
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

			Survey ns = (Survey) JSonTranslator.Deserialize<Survey> (json);
			return ns;
		}
	}
}

