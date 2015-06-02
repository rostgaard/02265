/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;
using System.Collections.Generic;

namespace Surveys
{
	public class Prerequisite
	{
		public List<AnswerOption> Answers  { get; set; }
		public QuestionReference Question { get; set; }
		public PrOperator Op {get; set; }

		public Prerequisite (List<AnswerOption> answers, QuestionReference qr, PrOperator oper)
		{
			Answers = answers;
			Question = qr;
			Op = oper;
		}

	public enum PrOperator {AND, OR}
	}
}
