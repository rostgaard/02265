using System;
using System.Collections.Generic;

namespace Surveys
{
	/// <summary>
	/// Survey generator for the mockup survey. Used only for debugging.
	/// </summary>
	public static class SurveyGenerator
	{
		/// <summary>
		/// Generates the exemplary Survey
		/// </summary>
		/// <returns>The fresh survey instance.</returns>
		public static Survey generateSurvey1()
		{
			Survey survey = new Survey ();
			survey.SurveyName = "Clinical";
			survey.SurveyId = Guid.NewGuid ();
			survey.addPart (generatePart1());
			survey.addPart (generatePart2());
			survey.addPart (generatePart3());
			survey.Scheduler = new Scheduler (Schedule.DAILY);
			return survey;
		}

		private static SurveyPart generatePart1() {
			SurveyPart sp1 = new SurveyPart ();
			LinkedList<QuestionReference> list = new LinkedList<QuestionReference> ();
			list.AddLast (generateQuestion11());
			list.AddLast (generateQuestionFreeM("What year were you born in?", DataType.DATE, true));
			list.AddLast (generateQuestionFreeM("What country do you come from", DataType.STRING, true));
			sp1.Scheduler = new Scheduler (Schedule.DAILY);
			sp1.Questions = list;
			return sp1;
		}

		private static SurveyPart generatePart2() {
			SurveyPart sp1 = new SurveyPart ();
			sp1.Scheduler = new Scheduler (Schedule.DAILY);
			LinkedList<QuestionReference> list = new LinkedList<QuestionReference> ();
			QuestionReference qr21 = generateQuestion21 (); 
			list.AddLast (qr21);
			QuestionReference qr22 = generateQuestionFreeM ("What is the reason of your discomfort", DataType.STRING, true);
			qr22.Prerequisites.Add(generatePrerequisite22(qr21));
			list.AddLast (qr22);
			list.AddLast (generateQuestion23());
			list.AddLast (generateQuestionFreeM("What is the ambient temperature?", DataType.FLOAT, false));
			sp1.Questions = list;
			return sp1;
		}


		private static SurveyPart generatePart3() {
			SurveyPart sp1 = new SurveyPart ();
			sp1.Scheduler = new Scheduler (Schedule.DAILY);
			LinkedList<QuestionReference> list = new LinkedList<QuestionReference> ();
			QuestionReference qr31 = generateQuestion31 ();
			list.AddLast (qr31);
			QuestionReference qr32 = generateQuestion32 ();
			qr32.Prerequisites.Add(generatePrerequisite32 (qr31));
			list.AddLast (qr32);
			QuestionReference qr33 = generateQuestionFreeM ("Please enter the other food(s) which caused an allergic reaction for you", DataType.STRING, false);
			qr33.Prerequisites.Add(generatePrerequisite33 (qr32));
			list.AddLast (qr33);
			sp1.Questions = list;
			return sp1;
		}


		private static QuestionReference generateQuestion11() 
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

		private static QuestionReference generateQuestionFreeM(String question, DataType answerType, Boolean mandatory) 
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

		private static Prerequisite generatePrerequisite22(QuestionReference qr) {
			List<AnswerOption> preq = new List<AnswerOption> ();
			for (int i = 1; i < 4; i++) {
				AnswerOption a = new AnswerOption (i.ToString());
				preq.Add (a);
			}
			return new Prerequisite (preq, qr, Prerequisite.PrOperator.OR);
		}

		private static Prerequisite generatePrerequisite32(QuestionReference qr) {
			List<AnswerOption> preq = new List<AnswerOption> ();
			AnswerOption a = new AnswerOption ("Yes");
			preq.Add (a);
			return new Prerequisite (preq, qr, Prerequisite.PrOperator.AND);
		}

		private static Prerequisite generatePrerequisite33(QuestionReference qr) {
			List<AnswerOption> preq = new List<AnswerOption> ();
			AnswerOption a = new AnswerOption ("Other");
			preq.Add (a);
			return new Prerequisite (preq, qr, Prerequisite.PrOperator.OR);
		}

		private static QuestionReference generateQuestion21() 
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

		private static QuestionReference generateQuestion23() 
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

		private static QuestionReference generateQuestion31() 
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

		private static QuestionReference generateQuestion32() 
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

