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
			
		public override bool Equals (object obj)
		{
			return String.Equals(this.Content, ((AnswerOption)obj).Content);
		}

		public override int GetHashCode ()
		{
			return this.Content.GetHashCode ();
		}
	}
}

