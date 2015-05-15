using System;
using System.Collections.Generic;

namespace Surveys
{
	public class Prerequisite
	{
		public List<AnswerOption> answers  { get; set; }
		public QuestionReference question { get; set; }
		public PrOperator op {get; set; }

		public Prerequisite (List<AnswerOption> answers, QuestionReference qr, PrOperator oper)
		{
			Prereq = answers;
			question = QuestionReference;
			op = oper;
		}
			
	}

	public enum PrOperator {AND, OR}
}

