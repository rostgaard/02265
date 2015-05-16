﻿using Xamarin.Forms;
using System.Collections.Generic;
using System.ServiceModel;
using XLabs.Forms.Controls;


namespace Surveys
{
	public class MultipleChocieView : QuestionView
	{
		Label QuestionLabel { get; set;}


		StackLayout answerOptions;

		public MultipleChocieView ( string questionText, List<string> options) : this ()
		{
			QuestionLabel.Text = questionText;
			foreach (string answer in options) {

				CheckBox answerCheckbox = new CheckBox ();
				answerCheckbox.CheckedChanged += (sender, e) => {
					{
						if (answerCheckbox.Checked == true)
							this.answers.Add(new AnswerOption (answer));
						else
							this.answers.Remove (new AnswerOption (answer));
					};
				};
				answerOptions.Children.Add (new StackLayout {
					Orientation = StackOrientation.Horizontal,
					HeightRequest = 40,
					Children = {
						new CheckBox() {},
						new Label {
							Text = answer
						}
					}
				});
			}
		}

		public MultipleChocieView () : base()
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