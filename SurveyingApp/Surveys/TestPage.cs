using System;
using Xamarin.Forms;
using XLabs.Forms.Controls;


namespace Surveys
{
	public class TestPage : ContentPage
	{
		public TestPage ()
		{
			Content = new StackLayout {
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Label {
						XAlign = TextAlignment.Center,
						Text = "Welcome to Xamarin Forms!"
					},
					new CheckBox {

					}
				}
			};
		}
	}
}

