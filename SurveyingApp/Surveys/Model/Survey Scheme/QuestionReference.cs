using System;
using System.Collections.Generic;

namespace Surveys
{
	public class QuestionReference
	{

		public Guid QuestionId { get; set; }

		public Question Question { get; set; }

		public List<Prerequisite> Prerequisites { get; set; }

		public QuestionReference ()
		{
			Prerequisites = new List<Prerequisite> ();
			QuestionId = Guid.NewGuid ();
		}

		public QuestionReference (Question q) : this ()
		{
			Prerequisites = new List<Prerequisite> ();
			Question = q;
		}

		public override int GetHashCode()
		{
			return QuestionId.GetHashCode ();
		}

		public override bool Equals(object o)
		{
			if (o is QuestionReference) {
				return this.QuestionId.Equals (((QuestionReference)o).QuestionId);
			}
			return false;
		}
	}
}

