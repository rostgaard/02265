using System;

using Xamarin.Forms;

namespace Surveys
{
	public class SavedInstancePage : ContentPage
	{
		public SavedInstancePage (string fileContent)
		{
			Content = new ScrollView
			{
				Content = new Label {
					Text = fileContent
				}
			};
		}
	}
}


