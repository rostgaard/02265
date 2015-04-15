using System;
using System.Collections;

namespace Surveys
{
	public class SurveyAnswer
	{
		public IList<Answer> Answers { get; set; }

		public UserData UserData { get; set; }

		public Survey Survey { get; set; }

		public SurveyAnswer ()
		{
		}
	}
}

