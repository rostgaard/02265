using System;

namespace Surveys
{
	public class QuestionReference
	{
		public Question Question { get; set; }

		public Prerequisite Prerequisite { get; set; }

		public QuestionReference ()
		{
		}
	}
}

