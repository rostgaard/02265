using System;
using System.Collections.Generic;

namespace Surveys
{
	public class AnswerType
	{
		private IList<String> answerText = new List<String>();
		private ContentType ContentType { get; set;}


		public AnswerType (ContentType contentType)
		{
			this.ContentType = contentType;
		}

		public void addAnswerText(String answerText)
		{
			answerText.Add (answerText);
		}
	}
}

