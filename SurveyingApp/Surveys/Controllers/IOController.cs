using System;
using System.Collections;
using PCLStorage;
using System.Collections.Generic;
using System.Linq;

namespace Surveys
{
	public static class IOController
	{	
		public static async void WriteSurveyResult (Survey surveyScheme, ICollection<QuestionView> currentViews)
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			IFolder folder = await rootFolder.CreateFolderAsync (Constants.filledDirectory,
				CreationCollisionOption.OpenIfExists);

			string fileName = surveyScheme.SurveyId.ToString ().Substring (0, 5) + surveyScheme.SurveyName +  " "  + DateTime.Now.DayOfYear.ToString () + ".json";

			IFile file = await folder.CreateFileAsync (fileName,
				CreationCollisionOption.ReplaceExisting);

			SurveyAnswer SurveyfilledScheme = PutAnswersToSurveyInstance (currentViews);
			string serialized = JSonTranslator.Serialize (SurveyfilledScheme);

			await file.WriteAllTextAsync (serialized);
		}

		private static SurveyAnswer PutAnswersToSurveyInstance (ICollection<QuestionView> currentViews)
		{

			SurveyAnswer answered = new SurveyAnswer ();
			answered.Answers = new List<Answer> ();

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
				answered.Answers.Add (a);
			}
			return answered;
		}

		public static IList<String> ReadFilledFileNames ()
		{
			IFolder rootFolder = FileSystem.Current.LocalStorage;
			var folderTask = rootFolder.CreateFolderAsync (Constants.filledDirectory,CreationCollisionOption.OpenIfExists);
			IFolder folder = folderTask.Result;

			var filesTask = folder.GetFilesAsync ();
			var files = filesTask.Result;

			List <String> filledSurveys =
				(from file in files
					where file.Name.EndsWith (".json")
					orderby (file.Name)
					select file.Name).ToList ();
			return filledSurveys;
		}

		public static string ReadFilledSurveyByName (string filename)
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

		public static string ReadTo()
		{
			return null;
		}
	}




	
}

