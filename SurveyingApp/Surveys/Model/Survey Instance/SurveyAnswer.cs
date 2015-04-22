using System;
using System.Collections.Generic;

namespace Surveys
{
	public class SurveyAnswer
	{
		public List<Answer> Answers { get; set; }

		public UserData UserData { get; set; }

		public Survey Survey { get; set; }

		public SurveyAnswer ()
		{
		}
	}
}

