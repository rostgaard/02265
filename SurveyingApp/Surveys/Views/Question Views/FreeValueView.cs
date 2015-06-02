using System;

using Xamarin.Forms;
using System.Collections.Generic;

namespace Surveys
{
	/// <summary>
	/// Free value view.
	/// </summary>
	public class FreeValueView : QuestionView
	{
		Label QuestionLabel { get; set;}

		Editor AnswerEditor {get; set;}

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.FreeValueView"/> class.
		/// </summary>
		/// <param name="qr">Qr.</param>
		/// <param name="questionText">Question text.</param>
		/// <param name="isMandatory">If set to <c>true</c> is mandatory.</param>
		public FreeValueView (QuestionReference qr, string questionText, bool isMandatory) : this (qr, isMandatory)
		{
			QuestionLabel.Text = questionText;
			this.IsMandatory = isMandatory;
			this.IsAnswered = false;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.FreeValueView"/> class.
		/// </summary>
		/// <param name="qr">Qr.</param>
		/// <param name="isMandatory">If set to <c>true</c> is mandatory.</param>
		public FreeValueView (QuestionReference qr, bool isMandatory) : base (qr, isMandatory)
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
				{
					this.IsAnswered = true;
					this.answers = new HashSet<AnswerOption> {
						new AnswerOption(AnswerEditor.Text)
					};
				}
				else this.IsAnswered = false;
			};
		}





	}


}


