using System;
using System.Collections.Generic;

namespace Surveys
{
	public static class StartupController
	{
		public static List<Survey> checkForNewSchemas(List<Survey> schemas, List<SurveyAnswer> instances) 
		{
			List<SurveyAnswer> duplicate = new List<SurveyAnswer>(instances);
			List<Survey> news = new List<Survey>();

			foreach (Survey schema in schemas) {
				Boolean founded = false;
				foreach (SurveyAnswer instance in duplicate) {
					if (schema.SurveyId.Equals (instance.Survey.SurveyId)) {
						duplicate.Remove (instance);
						founded = true;
						break;
					}
				}
				if (!founded) {
					news.Add (schema);
				}
			}

			return news;
		}

		public static List<SurveyAnswer> generateInstances(List<Survey> schemas, UserData userData) 
		{
			List<SurveyAnswer> instances = new List<SurveyAnswer> ();
			foreach (Survey schema in schemas) 
			{
				SurveyAnswer instance = new SurveyAnswer (schema, userData);
				instances.Add (instance);
			}
			return instances;
		}
	}
}

