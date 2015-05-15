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
			bool value;
			foreach (Prerequisite p in question.Prerequisites) {
			}
			return value;
		}
	}
}

