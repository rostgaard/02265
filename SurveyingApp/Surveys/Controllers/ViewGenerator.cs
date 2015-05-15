﻿using System;
using System.Collections.Generic;
using XLabs.Forms.Validation;
using System.Reflection;

namespace Surveys
{
	public class ViewGenerator
	{
		private Survey surveyScheme = null;

		private LinkedListNode<SurveyPart> currentSurveyPart = null;
		private LinkedListNode<QuestionReference> currentQuestion = null;

		private QuestionView currentQuestionView = null;

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
			if (!currentViews.Contains (generatedViews[currentQuestion.Value]))
				currentViews.AddLast (currentQuestionView);
			while (true) {
				if (currentQuestion.Next != null) {
					LinkedList<QuestionReference> preqList = new LinkedList<QuestionReference> ();
					foreach (Prerequisite p in currentQuestion.Next.Value.Prerequisites) {
						preqList.AddLast (p.Question);
					}

					bool isValid = PrerequisiteController.calculatePrerequisite (currentQuestion.Next.Value, preqList);

					if (isValid) {
						currentQuestion = currentQuestion.Next;
						currentQuestionView = GetQuestionView (currentQuestion.Value);
						generatedViews.Add (currentQuestion.Value, currentQuestionView);
						return;
					} else {
						currentQuestion = currentQuestion.Next;
						currentQuestionView = GetQuestionView (currentQuestion.Value);
						generatedViews.Add (currentQuestion.Value, currentQuestionView);
						return currentQuestionView;
					}
				}
				else {
					if (currentSurveyPart.Next == null)
						return null;
					currentSurveyPart = currentSurveyPart.Next;
					currentQuestion = currentSurveyPart.Value.Questions.First;
					currentQuestionView = GetQuestionView (currentQuestion.Value);
					generatedViews.Add (currentQuestion.Value, currentQuestionView);
					return currentQuestionView;
				}
			}

				

			// if we have finished the current part and need to move to the next else 

		}

		public QuestionView PreviousQuestion ()
		{
			currentQuestion = currentQuestion.Previous;
			return currentQuestionView;
		}

		public QuestionView InitialQuestion ()
		{
			// empty initialization
			currentViews = new LinkedList<QuestionView> ();
			generatedViews = new Dictionary<QuestionReference, QuestionView> ();

			currentSurveyPart = surveyScheme.SurveyParts.First;

			currentQuestion = currentSurveyPart.Value.Questions.First;
			currentQuestionView = GetQuestionView (currentQuestion.Value);

			generatedViews.Add (currentQuestion.Value, currentQuestionView);

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

