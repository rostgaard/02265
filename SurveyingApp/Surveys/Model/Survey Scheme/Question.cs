/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
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

		public IList<AnswerOption> PossibleAnswers { get; set; }

		public QuestionType QuestionType { get; set; }

		public DataType AnswerType { get; set; }

		private IList<Guid> dependencyList = new List<Guid> ();

		public Question ()
		{
			QuestionId = Guid.NewGuid ();
		}

		public Question (Guid guid, Boolean isMandatory, String questionText)
		{
			QuestionId = guid;
			IsMandatory = isMandatory;
			QuestionText = questionText;
		}

	}
}

