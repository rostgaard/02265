/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;
using System.Collections.Generic;
using System.Collections;

namespace Surveys
{
	public class Survey
	{
		public Guid SurveyId { get; set; }

		public String SurveyName { get; set; }

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

		private LinkedList<SurveyAnswer> _SurveyAnswers;

		public LinkedList<SurveyAnswer> SurveyAnswers {
			get {
				if (_SurveyAnswers == null)
					_SurveyAnswers = new LinkedList<SurveyAnswer> ();
				return _SurveyAnswers;
			}
			set {
				_SurveyAnswers = value;
			}
		}

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

