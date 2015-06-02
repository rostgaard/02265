/// <author>Piotr Milczarek, Anna Walach, Kim Rostgaard Christensen, Pawel Drozdowski </author>
using System;

using Xamarin.Forms;
using XLabs.Forms.Controls;
using System.Reflection;
using System.IO;
using PCLStorage;
using XLabs.Forms.Validation;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Surveys
{
	public class App : Application
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.App"/> class. Entry point to the application.
		/// </summary>
		public App ()
		{
		//	Survey s = SurveyGenerator.generateSurvey1 ();
		//	string s2 = JSonTranslator.Serialize (s);
			ConnectionController cc = new ConnectionController ();
			cc.DownloadNewSurveys ();
			MainPage = new NavigationPage(new HomePage ());
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

