using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using PCLStorage;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Surveys
{
	public class ToFillListPage : ContentPage
	{
		public IList<string> ToFillSurveyNames {
			get;
			private set;
		}

		public ToFillListPage ()
		{
			this.Title = "To be answered";

			// Read schemas, instances and check for new
			List<Survey> allSchemas = IOController.ReadAllSchemas ();
			List<SurveyAnswer> filledAnswers = new List<SurveyAnswer> ();

			List<string> filledAnswersSerialized = IOController.ReadFiles (Constants.filledFolder);

			foreach (string s in filledAnswersSerialized) {
				filledAnswers.Add (JSonTranslator.Deserialize<SurveyAnswer> (s));
			}


			List<Survey> newSchemas = StartupController.checkForNewSchemas (allSchemas, filledAnswers);

			// If new, generate unfilled instances
			List <SurveyAnswer> surveyUnfilledInstances = new List<SurveyAnswer> ();
			if (newSchemas.Count != 0) {

				UserData user = new UserData ();
				user.Username = "John";
				user.ID = "test_ID";
				surveyUnfilledInstances = StartupController.generateInstances (newSchemas, user);
			}

			// Read previously stored unfilled instances 
			List<string> previouslyUnfilledSerialized = IOController.ReadFiles (Constants.toFillFolder);
			foreach (string s in previouslyUnfilledSerialized) {
				SurveyAnswer sa = JSonTranslator.Deserialize<SurveyAnswer> (s);
				surveyUnfilledInstances.Add (sa);
			}
				

			// set active parts and preapre a display dictionary
			Dictionary<string, SurveyAnswer> toAnswerNamed = new Dictionary<string, SurveyAnswer> ();
			foreach (SurveyAnswer sa in surveyUnfilledInstances) {
				if (ScheduleController.setActive (sa)) {
					toAnswerNamed.Add (sa.Survey.SurveyName + "_" + sa.SurveyId.ToString ().Substring (0, 4) + "_" + DateTime.Now.DayOfYear,
						sa);
				}
			}

			// 1. start of app //
			// 2. check if any new schemas (schema that has no instance generated yet) - potencjalnie z neta
			//	StartupController.checkForNewSchemas ()
			// this one returns list of all new schemas
			// 3. if new schema, generate new survey instance
			//StartupController.generateInstances ()
			// this one return list of all the new instances
			// 4. foreach survey instance in unfilled instances folder do // dorzucam do tej w pamieci 
			// zwraca zamiast ustalac flage	////		set Active flags (ScheduleController.setActive(surveyInstance))
			//		if surveyInstance.survey.IsActive then add survey Instance to display active list

			// 5. app is displaying active instances
			// 6. customer chooses one of them
			// 7. he fills it in and saves
			// save actions:
			// 8. serialize survey instance (with all the answer) to "done surveys" folder
			// 9. call ScheduleController.reschedule(surveyInstance) <-- generated new dates (and may set active flags) przeprasuj setactive i ewentualnie dodac do pamieci
			// 10. remove old surveyInstance_serialized from "unfilled instances" folder lista w pamieci
			// 11. add new surveyInstance_rescheduled to "unfilled instances" folder




			// Create and initialize ListView.
		
			ListView listView = new ListView ();

			listView.ItemsSource = toAnswerNamed.Keys;

			listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem != null) {
					listView.SelectedItem = null;
					ViewGenerator vg = new ViewGenerator (toAnswerNamed [(string)e.SelectedItem].Survey);
					ContentPage surveyPage = new SurveyViewPage (vg);
					this.Navigation.PushAsync (surveyPage);
				}
			};

			this.Content = new ScrollView {
				Content = listView
			};
		}
	}
}