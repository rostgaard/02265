using System;
using System.Collections.Generic;

namespace Surveys
{
	public class Prerequisite
	{
		List<AnswerOption> Prereq  { get; set; }
		QuestionReference question { get; set; }
		PrOperator op {get; set; }

		public Prerequisite (List<AnswerOption> answers, QuestionReference qr, PrOperator oper)
		{
			Prereq = answers;
			question = QuestionReference;
			op = oper;
		}
			
	}

	public enum PrOperator {AND, OR}
}

