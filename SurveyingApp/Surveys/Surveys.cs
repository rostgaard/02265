using System;

using Xamarin.Forms;
using XLabs.Forms.Controls;
using System.Reflection;
using System.IO;

namespace Surveys
{
	public class App : Application
	{
		public App ()
		{
			// Survey s = SurveyReader.ReadSurveyFromResource("Surveys.SerializedSurveys.MedicalSurvey.json");
			Survey s = SurveyGenerator.generateSurvey1 ();
			ViewGenerator vg = new ViewGenerator (s);
			MainPage = new SurveyViewPage (vg);
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

