using System;
using Xamarin.Forms;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading;
using System.Linq.Expressions;

namespace Surveys
{
	public class SurveyViewPage : ContentPage
	{
		bool AnsweredCorrectly { get; set;}

		StackLayout surveyContent;

		StackLayout navigationContent;
		Button goToPreviousButton;
		Button goToNextButton;

		ViewGenerator vg = null;

		public SurveyViewPage(ViewGenerator vg)
		{
			this.vg = vg;

			#region creating navigationContent with two buttons
			goToPreviousButton = new Button {
				Text = "Previous",
				IsEnabled = true,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};
			goToPreviousButton.Clicked += OnPreviousClicked;


			goToNextButton = new Button {
				Text = "Next",
				IsEnabled = true,
				HorizontalOptions = LayoutOptions.EndAndExpand

			};
			goToNextButton.Clicked += OnNextClicked;

			navigationContent = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HeightRequest = 50
			};
			navigationContent.Children.Add (goToPreviousButton);
			navigationContent.Children.Add (goToNextButton);
			#endregion

			surveyContent = new StackLayout ();
			surveyContent.Children.Add (navigationContent);

			this.Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 0);

			this.Content = surveyContent;

			LoadSurvey ();
		}

		public void OnNextClicked (object sender, EventArgs args) {
			QuestionView v = vg.NextQuestion ();
			surveyContent.Children.RemoveAt (0);
			surveyContent.Children.Insert (0,v);
		}



		public void OnPreviousClicked(object sender, EventArgs args) {
			QuestionView v = vg.PreviousQuestion ();
			surveyContent.Children.RemoveAt (0);
			surveyContent.Children.Insert (0,v);
		}

		private void LoadSurvey()
		{
			QuestionView v = vg.InitialQuestion ();
			surveyContent.Children.Insert (0, v);
		}
}

}
