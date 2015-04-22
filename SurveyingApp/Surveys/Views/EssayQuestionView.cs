using System;
using Xamarin.Forms;
using System.ComponentModel;

namespace Surveys
{
	public class EssayQuestionView : QuestionView
	{
		Label QuestionLabel { get; set;}

		Editor AnswerEditor {get; set;}

		public EssayQuestionView ( string questionText) : this ()
		{
			QuestionLabel.Text = questionText;
		}

		public EssayQuestionView () : base ()
		{
			QuestionLabel = new Label{
				Text = "Default descriptive question?"
			};
			AnswerEditor = new Editor {
				Keyboard = Keyboard.Create (KeyboardFlags.All),
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			this.Children.Add (QuestionLabel);
			this.Children.Add (AnswerEditor);

			AnswerEditor.TextChanged += (sender, e) => {
				if (AnswerEditor.Text != null && AnswerEditor.Text != "")
					this.IsAnswered = true;
				else this.IsAnswered = false;
			};
		}
			



	}
}

