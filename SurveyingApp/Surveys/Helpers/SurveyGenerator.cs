using System;
using System.Collections.Generic;

namespace Surveys
{
	public class SurveyGenerator
	{
		public SurveyGenerator ()
		{
		}

		public Survey generateSurvey1()
		{
			Survey survey = new Survey ();
			survey.addPart (generatePart1());
			survey.addPart (generatePart2());
			survey.addPart (generatePart3());
			return survey;

		}

		private SurveyPart generatePart1() {
			SurveyPart sp1 = new SurveyPart ();
			LinkedList<QuestionReference> list = new LinkedList<QuestionReference> ();
			list.AddLast (generateQuestion11());
			list.AddLast (generateQuestionFreeM("What year were you born in?", DataType.DATE, true));
			list.AddLast (generateQuestionFreeM("What country do you come from", DataType.STRING, true));
			sp1.Questions = list;
			return sp1;
		}

		private SurveyPart generatePart2() {
			SurveyPart sp1 = new SurveyPart ();
			LinkedList<QuestionReference> list = new LinkedList<QuestionReference> ();
			list.AddLast (generateQuestion21());
			list.AddLast (generateQuestionFreeM("What is the reason of your discomfort", DataType.STRING, true));
			list.AddLast (generateQuestion23());
			list.AddLast (generateQuestionFreeM("What is the ambient temperature?", DataType.FLOAT, false));
			sp1.Questions = list;
			return sp1;
		}


		private SurveyPart generatePart3() {
			SurveyPart sp1 = new SurveyPart ();
			LinkedList<QuestionReference> list = new LinkedList<QuestionReference> ();
			list.AddLast (generateQuestion31());
			list.AddLast (generateQuestion32());
			list.AddLast (generateQuestionFreeM("Please enter the other food(s) which caused an allergic reaction for you", DataType.STRING, false));
			sp1.Questions = list;
			return sp1;
		}


		private QuestionReference generateQuestion11() 
		{
			QuestionReference qr1 = new QuestionReference ();
			Question q1 = new Question ();
			q1.IsMandatory = true;
			q1.QuestionText = "What is your gender?";
			q1.AnswerType = DataType.STRING; 
			q1.QuestionType = new Choice (1, 1);
			List<AnswerOption> list = new List<AnswerOption> ();
			AnswerOption a11 = new AnswerOption ("Male");
			AnswerOption a12 = new AnswerOption ("Female");
			AnswerOption a13 = new AnswerOption ("Other");
			list.Add (a11);
			list.Add (a12);
			list.Add (a13);
			q1.PossibleAnswers = list;
			qr1.Question = q1;
			return qr1;			
		}

		private QuestionReference generateQuestionFreeM(String question, DataType answerType, Boolean mandatory) 
		{
			QuestionReference qr1 = new QuestionReference ();
			Question q1 = new Question ();
			q1.IsMandatory = mandatory;
			q1.QuestionText = question;
			q1.AnswerType = answerType; 
			q1.QuestionType = new FreeValue ();
			qr1.Question = q1;
			return qr1;
		}

		private QuestionReference generateQuestion21() 
		{
			QuestionReference qr1 = new QuestionReference ();
			Question q1 = new Question ();
			q1.IsMandatory = true;
			q1.QuestionText = "How are you feeling today?";
			q1.AnswerType = DataType.INTEGER; 
			q1.QuestionType = new Choice (1, 1);
			List<AnswerOption> list = new List<AnswerOption> ();
			for (int i = 1; i < 11; i++) {
				AnswerOption a = new AnswerOption (i.ToString());
				list.Add (a);
			}
			q1.PossibleAnswers = list;
			qr1.Question = q1;
			return qr1;			
		}

		private QuestionReference generateQuestion23() 
		{
			QuestionReference qr1 = new QuestionReference ();
			Question q1 = new Question ();
			q1.IsMandatory = true;
			q1.QuestionText = "How would you describe your heat comfort?";
			q1.AnswerType = DataType.STRING; 
			q1.QuestionType = new Choice (1, 1);
			List<AnswerOption> list = new List<AnswerOption> ();
			AnswerOption a11 = new AnswerOption ("Freezing");
			AnswerOption a12 = new AnswerOption ("Cold");
			AnswerOption a13 = new AnswerOption ("Acceptable");
			AnswerOption a14 = new AnswerOption ("Warm");
			AnswerOption a15 = new AnswerOption ("Hot");
			list.Add (a11);
			list.Add (a12);
			list.Add (a13);
			list.Add (a14);
			list.Add (a15);
			q1.PossibleAnswers = list;
			qr1.Question = q1;
			return qr1;			
		}

		private QuestionReference generateQuestion31() 
		{
			QuestionReference qr1 = new QuestionReference ();
			Question q1 = new Question ();
			q1.IsMandatory = true;
			q1.QuestionText = "Did you experience any allergic reactions recently?";
			q1.AnswerType = DataType.BOOLEAN; 
			q1.QuestionType = new Choice (1, 1);
			List<AnswerOption> list = new List<AnswerOption> ();
			AnswerOption a11 = new AnswerOption ("Yes");
			AnswerOption a12 = new AnswerOption ("No");
			list.Add (a11);
			list.Add (a12);
			q1.PossibleAnswers = list;
			qr1.Question = q1;
			return qr1;			
		}

		private QuestionReference generateQuestion32() 
		{
			QuestionReference qr1 = new QuestionReference ();
			Question q1 = new Question ();
			q1.IsMandatory = true;
			q1.QuestionText = "Which of the following did cause an allergic reaction for you?";
			q1.AnswerType = DataType.BOOLEAN; 
			q1.QuestionType = new Choice (1, 7);
			List<AnswerOption> list = new List<AnswerOption> ();
			AnswerOption a11 = new AnswerOption ("Nuts");
			AnswerOption a12 = new AnswerOption ("Milk");
			AnswerOption a13 = new AnswerOption ("Egg");
			AnswerOption a14 = new AnswerOption ("Egg");
			AnswerOption a15 = new AnswerOption ("Soy");
			AnswerOption a16 = new AnswerOption ("Fish");
			AnswerOption a17 = new AnswerOption ("Other");
			list.Add (a11);
			list.Add (a12);
			list.Add (a13);
			list.Add (a14);
			list.Add (a15);
			list.Add (a16);
			list.Add (a17);
			q1.PossibleAnswers = list;
			qr1.Question = q1;
			return qr1;			
		}
	}
}

