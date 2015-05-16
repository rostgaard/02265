using System;

namespace Surveys
{
	public class AnswerOption : IEquatable<AnswerOption>
	{
		public String Content { get; set; }

		public AnswerOption ()
		{
		}

		public AnswerOption (String text)
		{
			Content = text;
		}
			
		public bool Equals (AnswerOption obj)
		{
			return String.Equals(this.Content, ((AnswerOption)obj).Content);
		}

	}
}

