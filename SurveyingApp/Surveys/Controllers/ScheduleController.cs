using System;

namespace Surveys
{
	public class ScheduleController
	{
		private ScheduleController ()
		{
		}

		public static SurveyAnswer reschedule(SurveyAnswer survey) 
		{
			survey.Survey.Scheduler = generateNewDate (survey.Survey.Scheduler);
			survey.Survey.isActive = false;
			foreach (SurveyPart sp in survey.Survey.SurveyParts) {
				sp.Scheduler = generateNewDate (sp.Scheduler);
				sp.isActive = false;
			}
			setActive (survey);
			survey.Answers = new System.Collections.Generic.List<Answer> ();
			return survey;
		}

		private static Scheduler generateNewDate(Scheduler sched) 
		{
			if (sched != null) {
				switch (sched.Frequency) 
				{
				case Schedule.ONCE:
					sched.Available = DateTime.MinValue;
					break;
				case Schedule.DAILY:
					sched.Available = DateTime.Today.AddDays (1);
					break;
				case Schedule.WEEKLY:
					sched.Available = DateTime.Today.AddDays (7);
					break;
				case Schedule.MONTHLY:
					sched.Available = DateTime.Today.AddMonths (1);
					break;

				}
			}
			return sched;
		}

		public static bool setActive(SurveyAnswer survey) 
		{
			foreach (SurveyPart sp in survey.Survey.SurveyParts) {
				if (sp.Scheduler.Available.CompareTo (DateTime.Today) <= 0) {
					sp.isActive = true;
					survey.Survey.isActive = true;
				}
			}
			if (survey.Survey.isActive)
				return true;
			return false;
		}
	}
}

