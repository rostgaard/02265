using System;

using Xamarin.Forms;

namespace Surveys
{
	public class MultipleChocieView : QuestionView
	{
		public MultipleChocieView (string text)
		{
			Children.Add (new Label { Text = text });
		}
	}
}


