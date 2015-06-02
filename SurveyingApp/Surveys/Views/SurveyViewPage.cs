/// <author>Piotr Milczarek, Kim Rostgaard Christensen</author>
using System;
using Xamarin.Forms;
using System.Dynamic;
using System.Collections.Generic;
using System.Threading;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Surveys
{
	/// <summary>
	/// Page containig Survey page (one question per page)
	/// </summary>
	public class SurveyViewPage : ContentPage
	{
		bool AnsweredCorrectly { get; set;}

		StackLayout surveyContent;

		StackLayout navigationContent;
		Button goToPreviousButton;
		Button goToNextButton;

		private ToFillListPage _tflp = null;

		ViewGenerator vg = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.SurveyViewPage"/> class.
		/// </summary>
		/// <param name="vg">View generator.</param>
		/// <param name="tflp">Page which called this one (for callbacks)</param>
		public SurveyViewPage(ViewGenerator vg, ToFillListPage tflp)
		{
			this.vg = vg;
			this._tflp = tflp;

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
				DisplayAlert ("Done!", "Thank you for filling the survey", "Submit", "Change").ContinueWith(t =>
					{
						if (t.Result == true)
						{
							IOController.WriteSurveyResult (vg.SurveyScheme, vg.CurrentViews);
							_tflp.Reschedule ();
							this.navigationContent.Navigation.PopAsync ();
							this.navigationContent.Navigation.PopAsync ();
						}
					}, TaskScheduler.FromCurrentSynchronizationContext());  

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
				// TODO go to the main menu? But HW button available
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
