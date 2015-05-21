using System;
using System.Collections.Generic;

namespace Surveys
{
	public class SurveyAnswer
	{
		public List<Answer> Answers { get; set; }

		public UserData UserData { get; set; }

		public Survey Survey { get; set; }

		public SurveyAnswer ()
		{
		}
	}
}

// start of app
// check if any new schemas (schema that has no instance generated yet)
// if new schema, generate new survey instance
// foreach survey instance in unfilled instances folder do
//		set Active flags (ScheduleController.setActive(surveyInstance))
//		if surveyInstance.survey.IsActive then add survey Instance to display active list

// app is displaying active instance
// customer chooses one of them
// he fills it in and saves
// save actions:
// serialize survey instance (with all the answer) to "done surveys" folder
// call ScheduleController.reschedule(surveyInstance) <-- generated new dates (and may set active flags)
// remove old surveyInstance_serialized from "unfilled instances" folder
// add new surveyInstance_rescheduled to "unfilled instances" folder


