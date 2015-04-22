using System;
using System.Collections.Generic;
using System.Collections;

namespace Surveys
{
	public class Survey
	{
		public Guid SurveyId { get; set; }

		public Scheduler Scheduler { get; set; }

		private IList<SurveyPart> surveyParts = new List<SurveyPart> ();

		public IList<SurveyPart> SurveyParts {
			get {
				return surveyParts;
			} 
			private set {
				surveyParts = value;
			}
		}

		public IList<SurveyAnswer> SurveyAnswers { get; set; }

		public Survey ()
		{
		}

		public void addPart (SurveyPart q)
		{
			if (q != null)
				surveyParts.Add (q);
			else
				throw new ArgumentNullException ();
		}
	}
}

