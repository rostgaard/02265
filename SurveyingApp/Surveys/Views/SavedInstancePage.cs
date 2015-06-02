using System;

using Xamarin.Forms;

namespace Surveys
{
	/// <summary>
	/// Page with serialized view of the survey.
	/// </summary>
	public class SavedInstancePage : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Surveys.SavedInstancePage"/> class.
		/// </summary>
		/// <param name="fileContent">File content.</param>
		/// <param name="fileName">File name.</param>
		public SavedInstancePage (string fileContent, string fileName = "")
		{
			Title = fileName;
			Content = new ScrollView
			{
				Content = new Label {
					Text = fileContent
				}
			};
		}
	}
}


