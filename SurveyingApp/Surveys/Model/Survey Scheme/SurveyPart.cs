/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;
using System.Collections.Generic;

namespace Surveys
{
	public class SurveyPart
	{
		public Scheduler Scheduler {get; set;}
		public LinkedList<QuestionReference> Questions { get; set; }

		public Boolean isActive { get; set; }

		public SurveyPart ()
		{
		}
	}
}

