using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Surveys
{
	public abstract class QuestionView : StackLayout
	{
		public bool IsMandatory { get; set;}
		public QuestionReference question { get; set; }
		public HashSet<AnswerOption> answers {get; set;}
		bool isAnswered = false;
		public bool IsAnswered {
			get {
				return isAnswered;
			}
			set {
				if (isAnswered != value) {
					isAnswered = value;
					OnPropertyChanged("IsAnswered");
					}

			}
		}

		public QuestionView()
		{
			this.VerticalOptions = LayoutOptions.FillAndExpand;
		}


		public QuestionView(QuestionReference qr)
		{
			this.VerticalOptions = LayoutOptions.FillAndExpand;
			question = qr;
		}
	}

}