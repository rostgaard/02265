using System;
using System.Collections.Generic;
using XLabs.Forms.Validation;
using System.Reflection;

namespace Surveys
{
	public class ViewGenerator
	{
		private Survey surveyScheme = null;
		IEnumerator<SurveyPart> partEnumerator = null;
		IEnumerator<QuestionReference> QREnumerator = null;



		private Stack<QuestionView> filledViews = null;
		private List<QuestionView> answeredQuestions = null;


		private QuestionReference currentQuestionReference {
			set;
			get;
		}

		public ViewGenerator (Survey s)
		{
			surveyScheme = s;
			partEnumerator = surveyScheme.SurveyParts.GetEnumerator ();
		}

		public ViewGenerator (List<QuestionView> aq, Survey s) : this(s)
		{
			answeredQuestions = aq;
		}

		public QuestionView NextQuestion() {

			while (true) {
				if (QREnumerator != null && QREnumerator.MoveNext ()) {

					currentQuestionReference = QREnumerator.Current;

					QuestionView qv = GetQuestionView (currentQuestionReference);
					return qv;
				} else {
					partEnumerator.MoveNext ();
					SurveyPart currentPart = partEnumerator.Current;

					QREnumerator = currentPart.Questions.GetEnumerator ();
					QREnumerator.Reset ();
					QREnumerator.MoveNext ();

					currentQuestionReference = QREnumerator.Current;

					QuestionView qv = GetQuestionView (currentQuestionReference);
					return qv;
				}
			}
		}
		

		public QuestionView PreviousQuestion()
		{
			return new FreeValueView ();
		//	return filledViews.Pop();
		}


		public QuestionView InitialQuestion()
		{
			partEnumerator.Reset ();
			partEnumerator.MoveNext ();
			SurveyPart initialPart = partEnumerator.Current;

			QREnumerator = initialPart.Questions.GetEnumerator ();
			QREnumerator.Reset ();
			QREnumerator.MoveNext ();

			currentQuestionReference = QREnumerator.Current;

			QuestionView qv = GetQuestionView (currentQuestionReference);
			return qv;
		}

		private QuestionView GetQuestionView(QuestionReference qref) {
			var qtype = qref.Question.QuestionType;

			if (qtype is FreeValue) {
				string questionText = qref.Question.QuestionText;
				return new FreeValueView (questionText);
			} else if (qtype is Choice) {
				string questionText = qref.Question.QuestionText;
				if (((Choice)qtype).minNumOfAnswers == 1 && ((Choice)qtype).maxNumOfAnswers == 1)
					return new SingleChoiceView (questionText);
				else
					return new MultipleChocieView (questionText);
			} else
				throw new ArgumentException ("Question Type not supported");

		}



	}
}

