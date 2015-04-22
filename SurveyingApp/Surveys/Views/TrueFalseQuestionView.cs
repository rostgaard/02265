using System;
using XLabs.Forms.Controls;
using Xamarin.Forms;
using System.ComponentModel;

namespace Surveys
{
	public class TrueFalseQuestionView : QuestionView
	{
		Label QuestionLabel { get; set;}


		CustomRadioButton YesButton { get; set;}
		CustomRadioButton NoButton { get; set;}



		public TrueFalseQuestionView ( string questionText) : this ()
		{
			QuestionLabel.Text = questionText;
		}

		public TrueFalseQuestionView () : base()
		{
			QuestionLabel = new Label{
				Text = "Default true/false question?"
			};

			YesButton = new CustomRadioButton {
				Text =  "True"
			};

			NoButton = new CustomRadioButton {
				Text = "False"
			};

			YesButton.CheckedChanged += (sender, e) => {
				if (YesButton.Checked) {
					NoButton.Checked = false;
					this.IsAnswered = true;
				}
			};

			NoButton.CheckedChanged += (sender, e) => 
			{
				if (NoButton.Checked) 
				{
					YesButton.Checked = false;
					this.IsAnswered = true;
				}
			};

			this.Children.Add (QuestionLabel);
					
			this.Children.Add (YesButton);

			this.Children.Add (NoButton); 
		}



	}
}

