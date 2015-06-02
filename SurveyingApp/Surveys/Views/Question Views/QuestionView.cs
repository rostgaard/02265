using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace Surveys
{
	/// <summary>
	/// Question view.
	/// </summary>
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

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.QuestionView"/> class.
		/// </summary>
		/// <param name="qr">Qr.</param>
		/// <param name="isMandatory">If set to <c>true</c> is mandatory.</param>
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