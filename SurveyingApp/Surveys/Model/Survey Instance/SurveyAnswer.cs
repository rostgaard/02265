using System;
using System.Collections.Generic;

namespace Surveys
{
	public class SurveyAnswer
	{
		public List<Answer> Answers { get; set; }

		public UserData UserData { get; set; }

		public Guid SurveyId { get; set; }

		public SurveyAnswer ()
		{
		}

		public SurveyAnswer(Survey surv, UserData data) 
		{
			Survey = surv;
			UserData = data;
		}
	}
}

// 1. start of app
// 2. check if any new schemas (schema that has no instance generated yet)
// /\ StartupController.chechForNewSchemas(List<Survey> allSchemas, List<SurveyAnswer> AllUnfilledInstances)
// this one returns list of all new schemas
// 3. if new schema, generate new survey instance
// /\ StartupController.generateInstances(List<Survey> schemas, UserData userData) 
// this one return list of all the new instances
// 4. foreach survey instance in unfilled instances folder do
//		set Active flags (ScheduleController.setActive(surveyInstance))
//		if surveyInstance.survey.IsActive then add survey Instance to display active list

// 5. app is displaying active instances
// 6. customer chooses one of them
// 7. he fills it in and saves
// save actions:
// 8. serialize survey instance (with all the answer) to "done surveys" folder
// 9. call ScheduleController.reschedule(surveyInstance) <-- generated new dates (and may set active flags)
// 10. remove old surveyInstance_serialized from "unfilled instances" folder
// 11. add new surveyInstance_rescheduled to "unfilled instances" folder


