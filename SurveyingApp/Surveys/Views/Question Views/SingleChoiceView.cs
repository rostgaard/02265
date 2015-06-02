/// <author>Piotr Milczarek</author>
using Xamarin.Forms;
using System.Collections.Generic;
using System.ServiceModel;


namespace Surveys
{
	/// <summary>
	/// Single choice view.
	/// </summary>
	public class SingleChoiceView : QuestionView
	{
		Label QuestionLabel { get; set; }

		Picker AnswerPicker { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.SingleChoiceView"/> class.
		/// </summary>
		/// <param name="qr">Qr.</param>
		/// <param name="questionText">Question text.</param>
		/// <param name="options">Options.</param>
		/// <param name="isMandatory">If set to <c>true</c> is mandatory.</param>
		public SingleChoiceView (QuestionReference qr, string questionText, List<string> options, bool isMandatory) : this (qr, isMandatory)
		{
			QuestionLabel.Text = questionText;
			foreach (string answer in options) {
				AnswerPicker.Items.Add (answer);
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.SingleChoiceView"/> class.
		/// </summary>
		/// <param name="qr">Qr.</param>
		/// <param name="isMandatory">If set to <c>true</c> is mandatory.</param>
		public SingleChoiceView (QuestionReference qr, bool isMandatory) : base (qr, isMandatory)
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
				}
				else {
					this.IsAnswered = false;
				};
			};

			this.Children.Add (QuestionLabel);

			this.Children.Add (AnswerPicker);
		}


	}
}