using System;
using System.Collections.Generic;

namespace Surveys
{
	public class Prerequisite
	{
		List<AnswerOption> Prereq  { get; set; }
		public QuestionReference Question { get; set; }
		PrOperator Op {get; set; }

		public Prerequisite (List<AnswerOption> answers, QuestionReference qr, PrOperator oper)
		{
			Prereq = answers;
			Question = qr;
			Op = oper;
		}

	public enum PrOperator {AND, OR}
	}
}
