using System;

using Xamarin.Forms;

namespace Surveys
{
	public class SingleChoiceView : QuestionView
	{
		public SingleChoiceView (string text)
		{
			Children.Add (new Label { Text = text });
		}
	}
}


