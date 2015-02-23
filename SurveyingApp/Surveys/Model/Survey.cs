using System;
using System.Collections.Generic;

namespace Surveys
{
	public class Survey
	{
		public Guid SurveyId { get; set; }

		private IList<Question> questionList = new List<Question>();

		public IList<Question> QuestionList
		{
			get 
			{
				return questionList;
			} 
			private set
			{
				questionList = value;
			}
		}

		public Survey ()
		{
		}

		public void addQuestion (Question q)
		{
			if (q != null)
				questionList.Add (q);
			else
				throw new ArgumentNullException();
		}
	}
}

