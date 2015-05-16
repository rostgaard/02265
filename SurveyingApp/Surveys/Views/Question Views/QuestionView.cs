using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Surveys
{
	public abstract class QuestionView : StackLayout
	{
		public bool IsMandatory { get; set;}
		public QuestionReference question { get; set; }
		public HashSet<AnswerOption> answers;
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
			
		}

		public QuestionView (QuestionReference qr, bool isMandatory)
		{
			this.VerticalOptions = LayoutOptions.FillAndExpand;
			isMandatory = isMandatory;
			question = qr;
			answers = new HashSet<AnswerOption> ();
		}
	}

}