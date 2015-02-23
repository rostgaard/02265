using System;
using Xamarin.Forms;
using System.ServiceModel;

namespace Surveys
{
	public class SliderQuestionView : QuestionView
	{
		Label QuestionLabel { get; set;}

		Slider AnswerSlider {get; set;}
		Label AnswerLabel { get; set;}

		public SliderQuestionView ( string questionText, double maximumValue) : this ()
		{
			QuestionLabel.Text = questionText;
			AnswerSlider.Maximum = maximumValue;
		}

		public SliderQuestionView () : base ()
		{
			QuestionLabel = new Label{
				Text = "Default select sliding value"
			};

			AnswerLabel = new Label{
				Text = ""
			};

					

			AnswerSlider = new Slider {
				Maximum = 100

			};
			this.Children.Add (QuestionLabel);
			this.Children.Add (AnswerSlider);
			this.Children.Add (AnswerLabel);

			AnswerSlider.ValueChanged += (sender, e) => {
					this.IsAnswered = true;
			};
		}
	}}
