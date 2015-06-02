/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;

namespace Surveys
{
	public class DummyContentProvider
	{
		private Survey s;
		public DummyContentProvider ()

		{
			s = new Survey ();
		}

		public Survey CreateDummySurvey() {
			Question q1 = new Question();

			return s;
		}


	}
}

