using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Surveys
{
	public abstract class QuestionView : StackLayout
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public bool IsMandatory { get; set;}
		public QuestionReference question { get; set; }
		public HashSet<AnswerOption> answers;
		private bool _isAnswered = false;
		public bool IsAnswered {
			get {
				return _isAnswered;
			}
			set {
				if (_isAnswered != value) {
					_isAnswered = value;
					RaisePropertyChanged();
					}
			}
		}

		public QuestionView()
		{
			
		}

		public QuestionView (QuestionReference qr, bool isMandatory)
		{
			this.VerticalOptions = LayoutOptions.FillAndExpand;
			this.IsMandatory = isMandatory;
			question = qr;
			answers = new HashSet<AnswerOption> ();
		}

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{

			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}

}