using System;

using Xamarin.Forms;

namespace Surveys
{
	public class FreeValueView : QuestionView
	{
		Label QuestionLabel { get; set;}

		Editor AnswerEditor {get; set;}

		public FreeValueView ( string questionText) : this ()
		{
			QuestionLabel.Text = questionText;
		}

		public FreeValueView () : base ()
		{
			QuestionLabel = new Label{
				Text = "",
				FontSize = 32
			};

			// TODO Differentiate the keyboard type depending on the content
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


