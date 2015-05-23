using System;
using System.Collections.Generic;

namespace Surveys
{
	public class QuestionReference
	{
		public Question Question { get; set; }

		public List<Prerequisite> Prerequisites { get; set; }

		public QuestionReference ()
		{
			Prerequisites = new List<Prerequisite> ();
		}

		public QuestionReference (Question q)
		{
			Prerequisites = new List<Prerequisite> ();
			Question = q;
		}
	}
}

