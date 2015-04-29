using System;

namespace Surveys
{
	public class AnswerOption
	{

		public String Content { get; set; }

		public AnswerOption ()
		{
		}

		public AnswerOption (String text)
		{
			Content = text;
		}
	}
}

