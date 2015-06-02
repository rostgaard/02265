/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;

namespace Surveys
{
	public class Answer
	{
		public AnswerOption AnsweredOption { get; set; }

		public QuestionReference QuestionRef { get; set; }

		public Answer ()
		{
		}
	}
}

