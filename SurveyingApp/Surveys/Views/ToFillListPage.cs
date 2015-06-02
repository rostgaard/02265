/// <author>Anna Walach, Kim Rostgaard Christensen, Piotr Milczarek</author>
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

	/// <summary>
	/// Page containing list of the surveys available to be filled.
	/// </summary>
	public class ToFillListPage : ContentPage
	{
		private SurveyAnswer chosenSurvey;
		public IList<string> ToFillSurveyNames {
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.ToFillListPage"/> class.
		/// </summary>
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
				
			// Create and initialize ListView.
		
			ListView listView = new ListView ();

			listView.ItemsSource = toAnswerNamed.Keys;

			listView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) => {
				if (e.SelectedItem != null) {
					listView.SelectedItem = null;
					chosenSurvey = toAnswerNamed [(string)e.SelectedItem];
					ViewGenerator vg = new ViewGenerator (chosenSurvey.Survey);
					ContentPage surveyPage = new SurveyViewPage (vg, this);
					this.Navigation.PushAsync (surveyPage);
				}
			};

			this.Content = new ScrollView {
				Content = listView
			};
		}

		/// <summary>
		/// Reschedule the instance of the chosen SurveyAnswer.
		/// </summary>
		public void Reschedule()
		{
			ScheduleController.reschedule (chosenSurvey);
			string rescheduled = JSonTranslator.Serialize (chosenSurvey);
			IOController.SaveFile (rescheduled,chosenSurvey.Survey.SurveyName + "_" + chosenSurvey.Survey.SurveyId.ToString ().Substring (0,4), Constants.toFillFolder);
		}
	}
}