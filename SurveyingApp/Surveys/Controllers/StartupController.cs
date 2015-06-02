/// <author>Anna Walach</author>
using System;
using System.Collections.Generic;

namespace Surveys
{
	/// <summary>
	/// Startup controller.
	/// </summary>
	public static class StartupController
	{
		/// <summary>
		/// Checks for new schemas.
		/// </summary>
		/// <returns>The list of new schemas.</returns>
		/// <param name="schemas">Schemas.</param>
		/// <param name="instances">Instances.</param>
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

		/// <summary>
		/// Generates the instances of SurveyAnswers for a specific user.
		/// </summary>
		/// <returns>The instances.</returns>
		/// <param name="schemas">Schemas.</param>
		/// <param name="userData">User data.</param>
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

