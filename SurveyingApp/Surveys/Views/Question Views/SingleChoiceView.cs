using Xamarin.Forms;
using System.Collections.Generic;
using System.ServiceModel;


namespace Surveys
{
	public class SingleChoiceView : QuestionView
	{
		Label QuestionLabel { get; set; }

		Picker AnswerPicker { get; set; }

		public SingleChoiceView (QuestionReference qr, string questionText, List<string> options) : this (qr)
		{
			QuestionLabel.Text = questionText;
			foreach (string answer in options) {
				AnswerPicker.Items.Add (answer);
			}
		}

		public SingleChoiceView (QuestionReference qr) : base (qr)
		{
			QuestionLabel = new Label {
				Text = "",
				FontSize = 32
					
			};

			AnswerPicker = new Picker ();

			AnswerPicker.SelectedIndexChanged += (sender, e) => {
				if (AnswerPicker.SelectedIndex != -1) {
					this.IsAnswered = true;
					this.answers = new HashSet<AnswerOption> {
						new AnswerOption (AnswerPicker.Items [AnswerPicker.SelectedIndex])
					};
				};
			};

			this.Children.Add (QuestionLabel);

			this.Children.Add (AnswerPicker);
		}


	}
}