using System;

namespace Surveys
{
	public class ScheduleController
	{
		private ScheduleController ()
		{
		}

		public static void reschedule(Survey survey) 
		{
			survey.Scheduler = generateNewDate (survey.Scheduler);
			survey.isActive = false;
			foreach (SurveyPart sp in survey.SurveyParts) {
				sp.Scheduler = generateNewDate (sp.Scheduler);
				sp.isActive = false;
			}
			setActive (survey);
		
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
					sched.Available = sched.Available.AddDays (1);
					break;
				case Schedule.WEEKLY:
					sched.Available = sched.Available.AddDays (7);
					break;
				case Schedule.MONTHLY:
					sched.Available = sched.Available.AddMonths (1);
					break;

				}
			}
			return sched;
		}

		public static Survey setActive(Survey survey) 
		{
			foreach (SurveyPart sp in survey.SurveyParts) {
				if (sp.Scheduler.Available.CompareTo (DateTime.Today) <= 0) {
					sp.isActive = true;
					survey.isActive = true;
				}
			}
			return survey;
		}
	}
}

