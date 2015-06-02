/// <author>Piotr Milczarek</author>

using Xamarin.Forms;
using System.Collections.Generic;
using System.ServiceModel;
using XLabs.Forms.Controls;


namespace Surveys
{
	/// <summary>
	/// Multiple chocie view.
	/// </summary>
	public class MultipleChocieView : QuestionView
	{
		Label QuestionLabel { get; set;}


		StackLayout answerOptions;

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.MultipleChocieView"/> class.
		/// </summary>
		/// <param name="qr">Qr.</param>
		/// <param name="questionText">Question text.</param>
		/// <param name="options">Options.</param>
		/// <param name="isMandatory">If set to <c>true</c> is mandatory.</param>
		public MultipleChocieView (QuestionReference qr, string questionText, List<string> options, bool isMandatory) : this (qr, isMandatory)
		{
			this.IsMandatory = isMandatory;
			QuestionLabel.Text = questionText;
			foreach (string answer in options) {

				CheckBox answerCheckbox = new CheckBox ();
				answerCheckbox.CheckedChanged += (sender, e) => {
					{
						if (answerCheckbox.Checked == true) {
							this.answers.Add(new AnswerOption (answer));
							this.IsAnswered = true;
						}
						else
						{
							this.answers.Remove (new AnswerOption (answer));
							bool foundChecked = false;
							foreach (StackLayout sl in this.answerOptions.Children)
							{
								CheckBox c = (CheckBox)sl.Children[0];
								if (c.Checked == true)
									foundChecked = true;
							}
							this.IsAnswered = foundChecked;
						}
					};
				};
				answerOptions.Children.Add (new StackLayout {
					Orientation = StackOrientation.Horizontal,
					HeightRequest = 40,
					Children = {
						answerCheckbox,
						new Label {
							Text = answer
						}
					}
				});
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.MultipleChocieView"/> class.
		/// </summary>
		/// <param name="qr">Qr.</param>
		/// <param name="isMandatory">If set to <c>true</c> is mandatory.</param>
		public MultipleChocieView (QuestionReference qr, bool isMandatory) : base(qr, isMandatory)
		{
			QuestionLabel = new Label {
				Text = "",
				FontSize = 32
			};

			answerOptions = new StackLayout {
			};
				

			this.Children.Add (QuestionLabel);
			this.Children.Add (answerOptions);

		}



	}
}