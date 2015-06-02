/// <author>Pawel Drozdowski </author>
using System;
using Xamarin.Forms;
using System.Reflection;
using System.IO;

namespace Surveys
{
	/// <summary>
	/// Connection controller.
	/// </summary>
	public class ConnectionController
	{
		/// <summary>
		/// Mockup class simulating actual connection for the new surveys.
		/// </summary>
		public void DownloadNewSurveys ()
		{
			// this part is actually faking the connection to the Internet
			Assembly assembly = GetType().GetTypeInfo().Assembly;
			string resource = "Surveys.SerializedSurveys.MedicalSurvey.json";
			string ss;
			using (Stream stream = assembly.GetManifestResourceStream (resource))
			{
				using (StreamReader reader = new StreamReader (stream))
				{
					var stringTask = reader.ReadToEndAsync ();
					ss = stringTask.Result;
				}
			}

			// process and save in the desired directory
			Survey s = JSonTranslator.Deserialize<Survey> (ss);
			IOController.SaveFile (ss,s.SurveyName, Constants.schemasFolder);
		}
	}
}

