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

		List<QuestionView> questionQueue; 
		int questionCounter = 0;

		public SurveyViewPage()
		{

			questionQueue = InitializeDummyQuestions ();
			int questionCounter = 0;

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


			surveyContent = new StackLayout ();

			InitializeDummyQuestions ();
			LoadFirstQuestion();



			this.Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 0);

			this.Content= surveyContent;

			updateQuestionNavigation ();

		}

		public void OnNextClicked (object sender, EventArgs args) {

			goToPreviousButton.IsEnabled = true;
		
			surveyContent.Children.Clear();

			surveyContent.Children.Add (questionQueue[++questionCounter]);
			surveyContent.Children.Add (navigationContent);

			updateQuestionNavigation ();

			if (questionQueue[questionCounter].IsAnswered == false) {
			goToNextButton.IsEnabled = false;
			}
		}



		public void OnPreviousClicked(object sender, EventArgs args) {

			if (--questionCounter == 0)
				goToPreviousButton.IsEnabled = false;

			surveyContent.Children.Clear();

			surveyContent.Children.Add (questionQueue[questionCounter]);
			surveyContent.Children.Add (navigationContent);

			updateQuestionNavigation ();

			if (questionQueue[questionCounter].IsAnswered == false) {
				goToNextButton.IsEnabled = false;
			}

		}

		public void LoadFirstQuestion () {
		

			surveyContent.Children.Add (questionQueue[questionCounter]);
			surveyContent.Children.Add (navigationContent);

			updateQuestionNavigation ();

			if (questionQueue[questionCounter].IsAnswered == false) {
				goToNextButton.IsEnabled = false;
		}
		}

		public void updateQuestionNavigation() {
			questionQueue[questionCounter].PropertyChanged+= (sender, e) => 
			{
				if (questionQueue[questionCounter].IsAnswered && (questionCounter < questionQueue.Count - 1))
					goToNextButton.IsEnabled = true;
				else goToNextButton.IsEnabled = false;
			};

		}

		public List<QuestionView> InitializeDummyQuestions() {

			List<QuestionView> dummyList = new List<QuestionView>();

			dummyList.Add (new TrueFalseQuestionView());

			dummyList.Add (new SliderQuestionView());

			dummyList.Add (new PickerQuestionView());

			dummyList.Add (new EssayQuestionView());




			return dummyList;

		}
			
}

}
