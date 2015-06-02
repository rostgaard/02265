/// <author>Anna Walach </author>
using System;

namespace Surveys
{
	/// <summary>
	/// Schedule controller.
	/// </summary>
	public class ScheduleController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.ScheduleController"/> class.
		/// </summary>
		private ScheduleController ()
		{
		}

		/// <summary>
		/// Reschedule the specified survey.
		/// </summary>
		/// <param name="survey">Survey.</param>
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

		/// <summary>
		/// Generates the new date.
		/// </summary>
		/// <returns>The new date.</returns>
		/// <param name="sched">Sched.</param>
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

		/// <summary>
		/// Sets the active parameter in the survey.
		/// </summary>
		/// <returns><c>true</c>, if active was set in any of the parts, <c>false</c> otherwise.</returns>
		/// <param name="survey">Survey.</param>
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

