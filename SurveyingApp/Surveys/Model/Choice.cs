using System;

namespace Surveys
{
	public class Choice : QuestionType
	{
		public int maxNumOfAnswers { get; set; }

		public int minNumOfAnswers { get; set; }

		public Choice ()
		{
		}
	}
}

