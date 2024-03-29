﻿/// <author>Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;
using System.Collections.Generic;
using XLabs.Forms.Validation;
using System.Reflection;
using Xamarin.Forms;
using PCLStorage;

namespace Surveys
{
	/// <summary>
	/// View generator.
	/// </summary>
	public class ViewGenerator
	{
		bool isFinished = false;
		bool isBeginning = true;

		public Survey SurveyScheme { private set; get; }

		private LinkedListNode<SurveyPart> currentSurveyPart = null;
		private LinkedListNode<QuestionReference> currentQuestion = null;

		public  LinkedList<QuestionView> CurrentViews { private set; get; }

		private Dictionary<QuestionReference, QuestionView> generatedViews = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.ViewGenerator"/> class.
		/// </summary>
		/// <param name="s">S.</param>
		public ViewGenerator (Survey s)
		{
			SurveyScheme = s;
		}

		// when reading from a saved survey TODO
		//		public ViewGenerator (List<QuestionView> aq, Survey s) : this(s)
		//		{
		//			generatedViews = aq;
		//		}

		/// <summary>
		/// Moves to the next question
		/// </summary>
		/// <returns>The next question.</returns>
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
					QuestionView q;
					if (generatedViews.TryGetValue (p.Question, out q)) {
						if (CurrentViews.Find (generatedViews [q.question]) != null)
							preqViewList.AddLast (generatedViews [p.Question]);
					}
				}
				if (PrerequisiteController.calculatePrerequisite (currentQuestion.Value, preqViewList)) {
					if (CurrentViews.Find (newView) == null) {
						CurrentViews.AddAfter (CurrentViews.Find (oldView), newView);
					}
					return newView;
							
				} else {
					if (CurrentViews.Find (newView) != null)
						CurrentViews.Remove (CurrentViews.Find (generatedViews [currentQuestion.Value]));
				}
			}
			return null;
		}

		/// <summary>
		/// Moves to the previous question.
		/// </summary>
		/// <returns>The previous question.</returns>
		public QuestionView PreviousQuestion ()
		{
			QuestionView currentView = generatedViews [currentQuestion.Value];
			LinkedListNode<QuestionView> currentViewNode = CurrentViews.Find (currentView);
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

		/// <summary>
		/// Gets initial question
		/// </summary>
		/// <returns>The initial question.</returns>
		public QuestionView InitialQuestion ()
		{
			// empty initialization
			CurrentViews = new LinkedList<QuestionView> ();
			generatedViews = new Dictionary<QuestionReference, QuestionView> ();

			currentSurveyPart = SurveyScheme.SurveyParts.First;

			currentQuestion = currentSurveyPart.Value.Questions.First;
			QuestionView currentQuestionView = GetQuestionView (currentQuestion.Value);

			generatedViews.Add (currentQuestion.Value, currentQuestionView);
			CurrentViews.AddLast (currentQuestionView);

			return currentQuestionView;
		}

		/// <summary>
		/// Gets the question view specific to the reference.
		/// </summary>
		/// <returns>The question view.</returns>
		/// <param name="qref">Question reference.</param>
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
			if (isFinished) {
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

