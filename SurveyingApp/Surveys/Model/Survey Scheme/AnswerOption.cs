using System;

namespace Surveys
{
	public class AnswerOption : IComparable
	{
		public String Content { get; set; }

		public AnswerOption ()
		{
		}

		public AnswerOption (String text)
		{
			Content = text;
		}

		int IComparable.CompareTo(object obj)
		{
			AnswerOption ao=(AnswerOption)obj;
			return String.Compare(this.Content, ao.Content);
		}
	}
}

