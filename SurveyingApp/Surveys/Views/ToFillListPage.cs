using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using PCLStorage;
using System.Collections.Generic;
using System.Linq;

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
			// Create and initialize ListView.
			ListView listView = new ListView ();
			this.ToFillSurveyNames = new List<string> {
				"PLACEHOLDER"
			};
		
			listView.ItemsSource = ToFillSurveyNames;
			listView.ItemSelected += (sender, args) => {
				if (args.SelectedItem != null) {
					// Deselect the item.
					listView.SelectedItem = null;
//					string selectedFileName = (string)args.SelectedItem;
//					string selectedFileContent = IOController.ReadFilledSurveyByName (selectedFileName);
//					this.Navigation.PushAsync (new SavedInstancePage (selectedFileContent, selectedFileName));
					Survey s = SurveyGenerator.generateSurvey1 ();
					ViewGenerator vg = new ViewGenerator (s);
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