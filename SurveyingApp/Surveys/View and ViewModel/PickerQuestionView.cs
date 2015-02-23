using Xamarin.Forms;
using System.Collections.Generic;
using System.ServiceModel;


namespace Surveys
 {
	public class PickerQuestionView : QuestionView
	{
		Label QuestionLabel { get; set;}


		Picker AnswerPicker { get; set;}



		public PickerQuestionView ( string questionText, List<string> options) : this ()
		{
			QuestionLabel.Text = questionText;
			foreach (string answer in options) {
				AnswerPicker.Items.Add (answer);
			}
		}

		public PickerQuestionView () : base()
		{
			QuestionLabel = new Label {
				Text = "Pick the item you like!"
			};

			AnswerPicker = new Picker ();

			AnswerPicker.Items.Add ("First option");
			AnswerPicker.Items.Add ("Second option");
			AnswerPicker.Items.Add ("Third option");
			AnswerPicker.Items.Add ("Fourth option");

			AnswerPicker.SelectedIndexChanged += (sender, e) => {
				this.IsAnswered = true;
			};

			this.Children.Add (QuestionLabel);

			this.Children.Add (AnswerPicker);

		}



	}
}