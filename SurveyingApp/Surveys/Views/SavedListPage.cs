using System;

using Xamarin.Forms;
using System.Collections.ObjectModel;
using PCLStorage;
using System.Collections.Generic;
using System.Linq;

namespace Surveys
{
	public class SavedListPage : ContentPage
	{
		public IList<string> FilledSurveys {
			get;
			private set;
		}

		public SavedListPage ()
		{
			this.Title = "List of saved survey results";
			// Create and initialize ListView.
			ListView listView = new ListView ();
			this.FilledSurveys = IOController.ReadFilledFileNames ();
		
			listView.ItemsSource = FilledSurveys;
			listView.ItemSelected += (sender, args) => {
				if (args.SelectedItem != null) {
					// Deselect the item.
					listView.SelectedItem = null;
					string selectedFileName = (string)args.SelectedItem;
					string selectedFileContent = IOController.ReadFilledSurveyByName (selectedFileName);
					this.Navigation.PushAsync (new SavedInstancePage (selectedFileContent, selectedFileName));
				}
			};

			this.Content = new ScrollView {
				Content = listView
			};
		}
	}
}