using System;
using System.Collections.Generic;

namespace Surveys
{
	public class PrerequisiteController
	{
		public PrerequisiteController ()
		{
		}

		public static bool calculatePrerequisite(QuestionReference question, LinkedList<QuestionView> views) 
		{
			bool value = false;
			bool found = false;
			foreach (Prerequisite p in question.Prerequisites) 
			{
				foreach (QuestionView qv in views) 
				{
					if (p.question.Equals(qv.question)) {
						foreach (AnswerOption a in p.answers) 
						{
							if (qv.answers.Contains (a)) {
								value = true;
								if (p.op == PrOperator.OR)
									break;
							} else {
								if (p.op == PrOperator.AND) return false;
							}
						}
						found = true;
						break;
					} 

				}
				if (!found)
					return false;
				found = false;
			}
			return value;
		}
	}
}

