/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;
using System.Collections.Generic;

namespace Surveys
{
	public class SurveyAnswer
	{
		public List<Answer> Answers { get; set; }

		public UserData UserData { get; set; }

		public Guid SurveyId { get; set; }

		public Survey Survey { get; set;}

		public SurveyAnswer ()
		{
		}

		public SurveyAnswer(Survey surv, UserData data) 
		{
			Survey = surv;
			UserData = data;
			this.Survey.SurveyAnswers.AddLast (this);
		}
	}
}