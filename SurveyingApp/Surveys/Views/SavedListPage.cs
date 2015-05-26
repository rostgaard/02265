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
		public List<string> FilledSurveys {
			get;
			private set;
		}

		public SavedListPage ()
		{
			this.Title = "List of saved survey results";
			// Create and initialize ListView.
			ListView listView = new ListView ();
			this.FilledSurveys = new List<string> ();
			LoadFiles ();
		
			listView.ItemsSource = FilledSurveys;
			listView.ItemSelected += (sender, args) => {
				if (args.SelectedItem != null) {
					// Deselect the item.
					listView.SelectedItem = null;
					// Navigate to NotePage.
					string selectedFileName = (string)args.SelectedItem;

					string selectedFileContent = ReadFilledSurveyByName (selectedFileName);

					this.Navigation.PushAsync (new SavedInstancePage (selectedFileContent));
				}
			};

			this.Content = new ScrollView {
				Content = listView
			};
		}

		void LoadFiles ()
		{

			IFolder rootFolder = FileSystem.Current.LocalStorage;

			var folderTask = rootFolder.GetFolderAsync (Constants.filledDirectory);
			IFolder folder = folderTask.Result;

			var filesTask = folder.GetFilesAsync ();
			var files = filesTask.Result;

			FilledSurveys =
				(from file in files
			  where file.Name.EndsWith (".json")
			  orderby (file.Name)
			  select file.Name).ToList ();
		}

		

		string ReadFilledSurveyByName (string filename)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;

			var folderTask = rootFolder.GetFolderAsync (Constants.filledDirectory);
			IFolder folder = folderTask.Result;

			var fileTask = folder.GetFileAsync (filename);
			IFile file = fileTask.Result;
			var readTask = file.ReadAllTextAsync ();
			string content;
			content = readTask.Result;
			return content;
		}
	}


}



