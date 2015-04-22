using System;

using Xamarin.Forms;

namespace Surveys
{
	public class SingleChoiceView : ContentPage
	{
		public SingleChoiceView ()
		{
			Content = new StackLayout { 
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}


