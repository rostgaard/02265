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

		public QuestionView currentQuestionView = null;
		public QuestionReference currentQuestionReference = null;

		private LinkedList<QuestionView> currentViews = null;
		private Dictionary<QuestionReference, QuestionView> generatedViews = null;

		public ViewGenerator (Survey s)
		{
			surveyScheme = s;
		}

		// when reading from a saved survey TODO
		//		public ViewGenerator (List<QuestionView> aq, Survey s) : this(s)
		//		{
		//			generatedViews = aq;
		//		}

		public QuestionView NextQuestion ()
		{

			currentViews.AddLast (currentQuestionView);

			// if we still have questions in the current part
			if (QREnumerator != null && QREnumerator.MoveNext ()) {
				currentQuestionReference = QREnumerator.Current;
				currentQuestionView = GetQuestionView (currentQuestionReference);
				generatedViews.Add (currentQuestionReference, currentQuestionView);
				return currentQuestionView;

				// if we have finished the current part and need to move to the next
			} else if (QREnumerator != null && !QREnumerator.MoveNext ()) {
				partEnumerator.MoveNext ();
				SurveyPart currentPart = partEnumerator.Current;

				QREnumerator = currentPart.Questions.GetEnumerator ();
				QREnumerator.Reset ();
				QREnumerator.MoveNext ();

				currentQuestionReference = QREnumerator.Current;
				currentQuestionView = GetQuestionView (currentQuestionReference);
				return currentQuestionView;
			}
			// if survey is finished, display something meaningful
			return null;
		}
		

		//		public QuestionView PreviousQuestion()
		//		{
		//			if (generatedViews[currentQuestionReference] == null)
		//				filledViews.AddLast (currentQuestionView);
		//			LinkedListNode<QuestionView> node = filledViews.Find (currentQuestionView);
		//			if (node.Previous.Value != null)
		//				currentQuestionView = node.Previous.Value;
		//			return currentQuestionView;
		//		}
			
		public QuestionView InitialQuestion ()
		{
			partEnumerator = surveyScheme.SurveyParts.GetEnumerator ();
			currentViews = new LinkedList<QuestionView> ();
			generatedViews = new Dictionary<QuestionReference, QuestionView> ();

			partEnumerator.Reset ();
			partEnumerator.MoveNext ();
			SurveyPart initialPart = partEnumerator.Current;

			QREnumerator = initialPart.Questions.GetEnumerator ();
			QREnumerator.Reset ();
			QREnumerator.MoveNext ();
			// questionCounter++;

			currentQuestionReference = QREnumerator.Current;
			currentQuestionView = GetQuestionView (currentQuestionReference);

			generatedViews.Add (currentQuestionReference, currentQuestionView);

			return currentQuestionView;
		}

		private QuestionView GetQuestionView (QuestionReference qref)
		{
			var qtype = qref.Question.QuestionType;

			if (qtype is FreeValue) {
				string questionText = qref.Question.QuestionText;
				return new FreeValueView (questionText);
			} else if (qtype is Choice) {
				string questionText = qref.Question.QuestionText;
				if (((Choice)qtype).minNumOfAnswers == 1 && ((Choice)qtype).maxNumOfAnswers == 1) {

					List<string> answerStrings = new List<string> ();
					foreach (AnswerOption ao in qref.Question.PossibleAnswers) {
						answerStrings.Add (ao.Content);
					}
						
					return new SingleChoiceView (questionText, answerStrings);
				} else {
					List<string> answerStrings = new List<string> ();
					foreach (AnswerOption ao in qref.Question.PossibleAnswers) {
						answerStrings.Add (ao.Content);
					}

					return new MultipleChocieView (questionText, answerStrings);
				}
			} else
				throw new ArgumentException ("Question Type not supported");

		}



	}
}

