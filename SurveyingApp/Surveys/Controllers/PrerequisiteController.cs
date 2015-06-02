/// <author>Anna Walach</author>
using System;
using System.Collections.Generic;

namespace Surveys
{
	/// <summary>
	/// Prerequisite controller.
	/// </summary>
	public class PrerequisiteController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.PrerequisiteController"/> class.
		/// </summary>
		public PrerequisiteController ()
		{
		}

		/// <summary>
		/// Calculates the prerequisite.
		/// </summary>
		/// <returns><c>true</c>, if prerequisite was fullfilled, <c>false</c> otherwise.</returns>
		/// <param name="question">Question.</param>
		/// <param name="views">Views.</param>
		public static bool calculatePrerequisite(QuestionReference question, LinkedList<QuestionView> views) 
		{
			bool value = false;
			bool found = false;
			if (question.Prerequisites.Count == 0) return true;
			foreach (Prerequisite p in question.Prerequisites) 
			{
				foreach (QuestionView qv in views) 
				{
					if (p.Question.Equals(qv.question)) {
						foreach (AnswerOption a in p.Answers) 
						{
							if (qv.answers.Contains (a)) {
								value = true;
								if (p.Op == Prerequisite.PrOperator.OR)
									break;
							} else {
								if (p.Op == Prerequisite.PrOperator.AND) return false;
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