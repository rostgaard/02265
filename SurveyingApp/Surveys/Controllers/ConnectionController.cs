using System;

namespace Surveys
{
	public static class ConnectionController
	{
		public static void DownloadNewSurveys ()
		{
			Survey s = SurveyGenerator.generateSurvey1 ();
			string ss = JSonTranslator.Serialize (s);
			IOController.SaveFile (ss,s.SurveyName, Constants.schemasFolder);
		}
	}
}

