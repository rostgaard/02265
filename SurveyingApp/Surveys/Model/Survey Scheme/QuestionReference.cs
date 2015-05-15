using System;

namespace Surveys
{
	public class QuestionReference
	{
		public Question Question { get; set; }

		public List<Prerequisite> Prerequisites { get; set; }

		public QuestionReference ()
		{
		}

		public QuestionReference (Question q)
		{
			Question = q;
		}
	}
}

