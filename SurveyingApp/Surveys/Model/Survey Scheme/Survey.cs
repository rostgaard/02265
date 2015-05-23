using System;
using System.Collections.Generic;
using System.Collections;

namespace Surveys
{
	public class Survey
	{
		public Guid SurveyId { get; set; }

		public Boolean isActive { get; set; }

		public Scheduler Scheduler { get; set; }

		private LinkedList<SurveyPart> surveyParts = new LinkedList<SurveyPart> ();

		public LinkedList<SurveyPart> SurveyParts {
			get {
				return surveyParts;
			} 
			private set {
				surveyParts = value;
			}
		}

		public LinkedList<SurveyAnswer> SurveyAnswers { get; set; }

		public Survey ()
		{
			isActive = false;
		}

		public void addPart (SurveyPart q)
		{
			if (q != null)
				surveyParts.AddLast (q);
			else
				throw new ArgumentNullException ();
		}
	}
}

