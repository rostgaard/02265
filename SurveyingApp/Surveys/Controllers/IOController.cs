using System;
using System.Collections;
using PCLStorage;
using System.Collections.Generic;
using System.Linq;
using XLabs.Platform.Services.IO;
using System.Threading.Tasks;

namespace Surveys
{
	// TODO refactor the less common operations to reduce number of functions
	/// <summary>
	/// IO controller for most common (platform-independant) file operations. Some more specifi cases made their way here as well.
	/// </summary>
	public static class IOController
	{	
		/// <summary>
		/// Writes the survey result.
		/// </summary>
		/// <param name="surveyScheme">Survey scheme.</param>
		/// <param name="currentViews">Current views.</param>
		public static async void WriteSurveyResult (Survey surveyScheme, ICollection<QuestionView> currentViews)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync (Constants.filledFolder,
				CreationCollisionOption.OpenIfExists);

			string fileName = surveyScheme.SurveyId.ToString ().Substring (0, 5) + "_" + surveyScheme.SurveyName +  "_"  + DateTime.Now.DayOfYear.ToString () + ".json";

			IFile file = await folder.CreateFileAsync (fileName,
				CreationCollisionOption.ReplaceExisting);

			SurveyAnswer SurveyfilledScheme = PutAnswersToSurveyInstance (currentViews, surveyScheme);
			string serialized = JSonTranslator.Serialize (SurveyfilledScheme);

			await file.WriteAllTextAsync (serialized);
		}


		/// <summary>
		/// Puts the answers to survey instance.
		/// </summary>
		/// <returns>The answers to survey instance.</returns>
		/// <param name="currentViews">Current views.</param>
		/// <param name="surveyScheme">Survey scheme.</param>
		private static SurveyAnswer PutAnswersToSurveyInstance (ICollection<QuestionView> currentViews, Survey surveyScheme)
		{
			UserData user = new UserData ();
			user.Username = "John";
			user.ID = "test_ID";

			SurveyAnswer sa = new SurveyAnswer (surveyScheme, user);
			sa.Answers = new List<Answer> ();

			foreach (QuestionView qv in currentViews) {
				Answer a = new Answer ();
				a.QuestionRef = new QuestionReference ();
				Question temporaryQuestion = qv.question.Question;

				a.QuestionRef.Question = new Question {
					QuestionId = temporaryQuestion.QuestionId
				};

				foreach (AnswerOption ao in qv.answers) {
					a.AnsweredOption = new AnswerOption {
						Content = ao.Content
					};
				}
				sa.Answers.Add (a);
			}
			return sa;
		}

		/// <summary>
		/// Reads the file names in directory.
		/// </summary>
		/// <returns>The file names in directory.</returns>
		/// <param name="directory">Directory. Preferabely member of Constants.</param>
		public static IList<String> ReadFileNamesInDirectory (string directory)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var folderTask = rootFolder.CreateFolderAsync (directory, CreationCollisionOption.OpenIfExists);
			IFolder folder = folderTask.Result;

			var filesTask = folder.GetFilesAsync ();
			var files = filesTask.Result;

			List <String> fileNames =
				(from file in files
					select file.Name).ToList ();
			return fileNames;
		}

		/// <summary>
		/// Opens file in a specified directory.
		/// </summary>
		/// <returns>The file content. Null if not present.</returns>
		/// <param name="filename">Filename.</param>
		/// <param name="directory">Directory. Preferabely member of Constants. Created if not present.</param>
		public static string ReadFile (string filename, string directory)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;

			var folderTask = rootFolder.CreateFolderAsync (directory,CreationCollisionOption.OpenIfExists);
			IFolder folder = folderTask.Result;

			var fileTask = folder.GetFileAsync (filename);
			IFile file = fileTask.Result;
			string content = null;
			if (file != null) {
				var readTask = file.ReadAllTextAsync ();
				content = readTask.Result;
			}
			return content;
		}

		/// <summary>
		/// Reads the file names.
		/// </summary>
		/// <returns>The file names.</returns>
		/// <param name="directory">Directory. Preferabely member of Constants.</param>
		public static List<String> ReadFileNames (string directory)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var folderTask = rootFolder.CreateFolderAsync (directory, CreationCollisionOption.OpenIfExists);
			IFolder folder = folderTask.Result;

			var filesTask = folder.GetFilesAsync ();
			IList<IFile> files = filesTask.Result;

			List<string> names = new List<string> ();

			foreach (IFile f in files)			{
				names.Add (f.Name); 
			};
			return names;
		}

		/// <summary>
		/// Reads the files.
		/// </summary>
		/// <returns>The files.</returns>
		/// <param name="directory">Directory. Preferabely member of constants.</param>
		public static List<String> ReadFiles (string directory)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var folderTask = rootFolder.CreateFolderAsync (directory, CreationCollisionOption.OpenIfExists);
			IFolder folder = folderTask.Result;

			var filesTask = folder.GetFilesAsync ();
			IList<IFile> files = filesTask.Result;

			List<string> contents = new List<string> ();

			foreach (IFile f in files)	{
				var contentTask = f.ReadAllTextAsync ();
				contents.Add (contentTask.Result); 
			};
			return contents;
		}
			
		/// <summary>
		/// Saves the file.
		/// </summary>
		/// <param name="fileContent">File content.</param>
		/// <param name="fileName">File name.</param>
		/// <param name="directory">Directory. Preferabely member of Constants.</param>
		public static void SaveFile (string fileContent, string fileName, string directory)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var folderTask = rootFolder.CreateFolderAsync (directory,
				CreationCollisionOption.OpenIfExists);
			IFolder folder = folderTask.Result;

			var fileTask = folder.CreateFileAsync (fileName,
				CreationCollisionOption.ReplaceExisting);

			IFile file = fileTask.Result;

			file.WriteAllTextAsync (fileContent);
		}

		/// <summary>
		/// Saves the survey definition.
		/// </summary>
		/// <param name="s">The Survey definition to be saved. Used by ConnectionController.</param>
		public static async void SaveSurveyDefinition (Survey s)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync (Constants.schemasFolder,
				CreationCollisionOption.OpenIfExists);

			string fileName = s.SurveyId.ToString ().Substring (0, 5) + "_" + s.SurveyName + ".json";

			IFile file = await folder.CreateFileAsync (fileName,
				CreationCollisionOption.ReplaceExisting);

			string serialized = JSonTranslator.Serialize (s);
			await file.WriteAllTextAsync (serialized);
		}

		/// <summary>
		/// Reads all schemas.
		/// </summary>
		/// <returns>The all schemas.</returns>
		public static List<Survey> ReadAllSchemas()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var folderTask = rootFolder.CreateFolderAsync (Constants.schemasFolder, CreationCollisionOption.OpenIfExists);
			IFolder folder = folderTask.Result;

			var filesTask = folder.GetFilesAsync ();
			IList<IFile> files = filesTask.Result;

			List<Survey> surveys = new List<Survey> ();

			foreach (IFile f in files)			{
				var fileContentTaks = f.ReadAllTextAsync ();
				string fileContent = fileContentTaks.Result;
				Survey s = JSonTranslator.Deserialize<Survey> (fileContent);
				surveys.Add (s);
			};
			return surveys;
		}
	}




	
}

