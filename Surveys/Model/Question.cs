using System;
using System.Collections.Generic;

namespace Surveys
{
	public class Question
	{
		public Guid QuestionId { get; set; }
		public Boolean IsAnswered { get; set; }
		public Boolean IsMandatory { get; set; }
		public String QuestionText { get; set; }

		private IList<Guid> dependencyList = new List<Guid>();

		public Question (Guid guid, Boolean isMandatory, String questionText)
		{
			QuestionId = guid
		}

		public void addDependantQuestion(Question q)
		{
			throw new NotImplementedException ();
		}

		public void addDependantQuestion(Question q)
		{
			throw new NotImplementedException ();
		}

		public void addDependantQuestion(Guid g)
		{
			throw new NotImplementedException ();
		}
	}
}

