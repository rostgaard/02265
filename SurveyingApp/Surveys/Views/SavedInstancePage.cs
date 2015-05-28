using System;

using Xamarin.Forms;

namespace Surveys
{
	public class SavedInstancePage : ContentPage
	{
		public SavedInstancePage (string fileContent, string fileName = "")
		{
			Title = fileName;
			Content = new ScrollView
			{
				Content = new Label {
					Text = fileContent
				}
			};
		}
	}
}


