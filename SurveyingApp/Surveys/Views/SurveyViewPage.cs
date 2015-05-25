using System;
using Xamarin.Forms;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading;
using System.Linq.Expressions;
using System.ComponentModel;

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
				IsEnabled = false,
				HorizontalOptions = LayoutOptions.StartAndExpand
			};
			goToPreviousButton.Clicked += OnPreviousClicked;


			goToNextButton = new Button {
				Text = "Next",
				IsEnabled = false,
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
			if (v == null) {
				var answer = this.DisplayAlert ("Done!", "Thank you for filling the survey", "Submit", "Change");
				if (answer.Result == true)
				{
					
				}
				// TODO actually handle the alert
			} else {
				InitializePropertyCallback (v);
				surveyContent.Children.RemoveAt (0);
				surveyContent.Children.Insert (0, v);
				this.goToNextButton.IsEnabled = v.IsAnswered;
				this.goToPreviousButton.IsEnabled = true;
			}
		}

		public void OnPreviousClicked(object sender, EventArgs args) {
			QuestionView v = vg.PreviousQuestion ();
			if (v != null) {
				InitializePropertyCallback (v);
				this.goToNextButton.IsEnabled = v.IsAnswered;
				this.goToPreviousButton.IsEnabled = true;
				surveyContent.Children.RemoveAt (0);
				surveyContent.Children.Insert (0, v);
			}
			else{
				this.goToPreviousButton.IsEnabled = false;
				// TODO go to the main menu
			}
		}

		private void LoadSurvey()
		{
			QuestionView v = vg.InitialQuestion ();
			InitializePropertyCallback (v);
			surveyContent.Children.Insert (0, v);
		}

		private void InitializePropertyCallback(QuestionView v)
		{

			v.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
				switch (e.PropertyName)
				{
				case "IsAnswered":
					if (((QuestionView) sender).IsAnswered == true)
						this.goToNextButton.IsEnabled = true;
					else
						this.goToNextButton.IsEnabled = false;
					break;
				}
			};
		}

}

}
