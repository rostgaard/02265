using System;
using Xamarin.Forms;

namespace Surveys
{
	public abstract class QuestionView : StackLayout
{

		public bool IsMandatory { get; set;}

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
}

}