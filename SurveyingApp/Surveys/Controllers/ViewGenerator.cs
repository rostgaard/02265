using System;
using System.Collections.Generic;
using XLabs.Forms.Validation;
using System.Reflection;
using Xamarin.Forms;

namespace Surveys
{
	public class ViewGenerator
	{

		bool isFinished = false;
		bool isBeginning = true;

		private Survey surveyScheme = null;

		private LinkedListNode<SurveyPart> currentSurveyPart = null;
		private LinkedListNode<QuestionReference> currentQuestion = null;

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
			QuestionView oldView = generatedViews [currentQuestion.Value];

			// loop as long as we have a next question in the survey definition
			while (ProgressQuestion ()) {
				QuestionView newView = null;
				if (generatedViews.ContainsKey (currentQuestion.Value))
					newView = generatedViews [currentQuestion.Value];
				else {
					newView = GetQuestionView (currentQuestion.Value);
					generatedViews.Add (currentQuestion.Value, newView);
				}

				LinkedList<QuestionView> preqViewList = new LinkedList<QuestionView> ();
				foreach (Prerequisite p in currentQuestion.Value.Prerequisites) {
					if (currentViews.Find (generatedViews [p.Question])!= null)
						preqViewList.AddLast (generatedViews [p.Question]);
				}
				if (PrerequisiteController.calculatePrerequisite (currentQuestion.Value, preqViewList)) {
					if (currentViews.Find (newView) == null) {
						currentViews.AddAfter (currentViews.Find (oldView), newView);
					}
					return newView;
							
				} else {
					if (currentViews.Find (newView) != null)
						currentViews.Remove (currentViews.Find (generatedViews [currentQuestion.Value]));
				}
			}
			return null;
		}

		public QuestionView PreviousQuestion ()
		{
			QuestionView currentView = generatedViews [currentQuestion.Value];
			LinkedListNode<QuestionView> currentViewNode = currentViews.Find (currentView);
			if (currentViewNode.Previous == null)
				return null;
			QuestionView previousView = currentViewNode.Previous.Value;
			QuestionReference previousReference = previousView.question;

			while (currentQuestion.Value != previousReference) {
				if (!RegressQuestion ())
					throw new ArgumentOutOfRangeException ("Could not find the predecessor for the question");
			}
			return previousView;
		}

		public QuestionView InitialQuestion ()
		{
			// empty initialization
			currentViews = new LinkedList<QuestionView> ();
			generatedViews = new Dictionary<QuestionReference, QuestionView> ();

			currentSurveyPart = surveyScheme.SurveyParts.First;

			currentQuestion = currentSurveyPart.Value.Questions.First;
			QuestionView currentQuestionView = GetQuestionView (currentQuestion.Value);

			generatedViews.Add (currentQuestion.Value, currentQuestionView);
			currentViews.AddLast (currentQuestionView);

			return currentQuestionView;
		}

		private QuestionView GetQuestionView (QuestionReference qref)
		{
			var qtype = qref.Question.QuestionType;

			if (qtype is FreeValue) {
				string questionText = qref.Question.QuestionText;
				return new FreeValueView (qref, questionText, qref.Question.IsMandatory);
			} else if (qtype is Choice) {
				string questionText = qref.Question.QuestionText;
				if (((Choice)qtype).minNumOfAnswers == 1 && ((Choice)qtype).maxNumOfAnswers == 1) {

					List<string> answerStrings = new List<string> ();
					foreach (AnswerOption ao in qref.Question.PossibleAnswers) {
						answerStrings.Add (ao.Content);
					}
						
					return new SingleChoiceView (qref, questionText, answerStrings, qref.Question.IsMandatory);
				} else {
					List<string> answerStrings = new List<string> ();
					foreach (AnswerOption ao in qref.Question.PossibleAnswers) {
						answerStrings.Add (ao.Content);
					}

					return new MultipleChocieView (qref, questionText, answerStrings, qref.Question.IsMandatory);
				}
			} else
				throw new ArgumentException ("Question Type not supported");

		}

		/// <summary>
		/// Progresses the question.
		/// </summary>
		/// <returns><c>true</c>, if question was progressed, <c>false</c> otherwise.</returns>
		private bool ProgressQuestion ()
		{
			if (currentQuestion.Next != null) {
				currentQuestion = currentQuestion.Next;
				return true;
			} else if (currentSurveyPart.Next != null) {
				currentSurveyPart = currentSurveyPart.Next;
				currentQuestion = currentSurveyPart.Value.Questions.First;
				return true;
			}
			isFinished = true;
			return false;
		}

		/// <summary>
		/// Regresses the question.
		/// </summary>
		/// <returns><c>true</c>, if question was regressed, <c>false</c> otherwise.</returns>
		private bool RegressQuestion ()
		{
			if (isFinished)
			{
				isFinished = false;
				return true;
			}
			if (currentQuestion.Previous != null) {
				currentQuestion = currentQuestion.Previous;
				return true;
			} else if (currentSurveyPart.Previous != null) {
				currentSurveyPart = currentSurveyPart.Previous;
				currentQuestion = currentSurveyPart.Value.Questions.Last;
				return true;
			}
			return false;
		}



	}
}

