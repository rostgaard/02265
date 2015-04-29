using System;
using Xamarin.Forms;

namespace Surveys
{
	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = new GeolocatorPage ();
			SurveyGenerator sg = new SurveyGenerator ();
			Survey survey = sg.generateSurvey1 ();
			String json = JSonTranslator.serialize (survey);
			Survey ns = (Survey) JSonTranslator.deserialize (json, typeof(Survey));



		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

