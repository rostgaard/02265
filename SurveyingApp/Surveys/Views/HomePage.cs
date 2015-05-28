using System;

using Xamarin.Forms;

namespace Surveys
{
	public class HomePage : ContentPage
	{
		public HomePage ()
		{
			this.Title = "Surveyor";

			Button toFillButton = new Button {
				Text = "Surveys to fill",
				HorizontalOptions = LayoutOptions.Center,
			};

			Button filledButton = new Button {
				Text = "Filled surveys",
				HorizontalOptions = LayoutOptions.Center,
			};

			toFillButton.Clicked += (sender, args) => {
				this.Navigation.PushAsync (new ToFillListPage ());
			};

			filledButton.Clicked += (sender, args) => {
				this.Navigation.PushAsync (new SavedListPage ());
			};

			Content = new StackLayout {
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				Children = {
					toFillButton,
					filledButton
				}
			};
		}
	}
}