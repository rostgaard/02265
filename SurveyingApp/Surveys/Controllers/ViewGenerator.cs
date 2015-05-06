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

		private int viewCounter = 0;
		private int questionCounter = 0;

		QuestionView currentQuestionView = null;

		private LinkedList<QuestionView> filledViews = null;
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

				filledViews.AddLast (currentQuestionView);

				// if we still have questions in the current part
				if (QREnumerator != null && QREnumerator.MoveNext ()) {
					viewCounter++;
					questionCounter++;
					currentQuestionReference = QREnumerator.Current;
					currentQuestionView = GetQuestionView (currentQuestionReference);
					return currentQuestionView;

		        // if we have finished the current part
				} else {
					partEnumerator.MoveNext ();
					SurveyPart currentPart = partEnumerator.Current;

					QREnumerator = currentPart.Questions.GetEnumerator ();
					QREnumerator.Reset ();
					QREnumerator.MoveNext ();

					viewCounter++;
					questionCounter++;

					currentQuestionReference = QREnumerator.Current;

					currentQuestionView = GetQuestionView (currentQuestionReference);
					return currentQuestionView;
				}
			}
		}
		

		public QuestionView PreviousQuestion()
		{
			if (filledViews.Find (currentQuestionView) == null)
				filledViews.AddLast (currentQuestionView);
			LinkedListNode<QuestionView> node = filledViews.Find (currentQuestionView);
			currentQuestionView = node.Previous.Value;
			return currentQuestionView;
		}
			
		public QuestionView InitialQuestion()
		{
			filledViews = new LinkedList<QuestionView> ();

			partEnumerator.Reset ();
			partEnumerator.MoveNext ();
			SurveyPart initialPart = partEnumerator.Current;

			QREnumerator = initialPart.Questions.GetEnumerator ();
			QREnumerator.Reset ();
			QREnumerator.MoveNext ();
			questionCounter++;

			currentQuestionReference = QREnumerator.Current;

			currentQuestionView = GetQuestionView (currentQuestionReference);

			return currentQuestionView;
		}

		private QuestionView GetQuestionView(QuestionReference qref) {
			var qtype = qref.Question.QuestionType;

			if (qtype is FreeValue) {
				string questionText = qref.Question.QuestionText;
				return new FreeValueView (questionText);
			} else if (qtype is Choice) {
				string questionText = qref.Question.QuestionText;
				if (((Choice)qtype).minNumOfAnswers == 1 && ((Choice)qtype).maxNumOfAnswers == 1)
				{

					List<string> answerStrings = new List<string>();
					foreach (AnswerOption ao in qref.Question.PossibleAnswers)
				{
						answerStrings.Add (ao.Content);
				}
						
					return new SingleChoiceView (questionText, answerStrings);
				}
				else
				{
					List<string> answerStrings = new List<string>();
					foreach (AnswerOption ao in qref.Question.PossibleAnswers)
					{
						answerStrings.Add (ao.Content);
					}

					return new MultipleChocieView (questionText, answerStrings);
				}
			} else
				throw new ArgumentException ("Question Type not supported");

		}



	}
}

