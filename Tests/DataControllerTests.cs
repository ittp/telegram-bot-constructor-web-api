using System.Collections.Generic;
using Api.Controllers;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Mongo2Go;
using Tests.Utils;
using Xunit;

namespace Tests
{
	public class DataControllerTests
	{
		private DataController _dataController;
		private MongoDbRunner _fakeDbRunner;

		private void Setup()
		{
			_fakeDbRunner = MongoDbRunner.Start();
			_dataController = new DataController(new Repository(_fakeDbRunner.ConnectionString, "TestBase"));
		}

		private void Dispose()
		{
			_fakeDbRunner.Dispose();
		}

		[Fact]
		public void BotsActions()
		{
			Setup();

			var addResult = _dataController.AddBot("name", "token");
			var id = addResult.GetPropertyOfJsonResult<string>("id");
			var token = addResult.GetPropertyOfJsonResult<string>("token");

			var getResult = _dataController.GetBot(id);
			var getByTokenResult = _dataController.GetBotByToken(token);

			var removeResult = _dataController.RemoveBot(id);
			var afterRemoveResult = _dataController.GetBot(id);

			Assert.Equal("name", getResult.GetPropertyOfJsonResult<string>("name"));
			Assert.Equal("name", getByTokenResult.GetPropertyOfJsonResult<string>("name"));
			Assert.NotStrictEqual(new JsonResult(true), removeResult);
			Assert.NotStrictEqual(new JsonResult(false), afterRemoveResult);

			Dispose();
		}

		[Fact]
		public void BotsActions_WithWrongParams()
		{
			Setup();

			_dataController.AddBot("name", "token");

			var getByTokenResult = _dataController.GetBotByToken("wrong");
			var getResult = _dataController.GetBot("wrong");

			var wrongAddResult = _dataController.AddBot("", "");

			Assert.NotStrictEqual(new JsonResult(false), getByTokenResult);
			Assert.NotStrictEqual(new JsonResult(false), getResult);
			Assert.NotStrictEqual(new JsonResult(false), wrongAddResult);

			Dispose();
		}


		[Fact]
		public void InlineKeysActions()
		{
			Setup();

			var addResult = _dataController.AddInlineKey("caption", "answer", "botId");
			var id = addResult.GetPropertyOfJsonResult<string>("id");

			var getSingleResult = _dataController.GetInlineKey(id);
			var getResult = _dataController.GetInlineKeys("botId");

			var removeResult = _dataController.RemoveInlineKey(id);
			var afterRemoveResult = _dataController.GetInlineKey(id);

			Assert.Equal("caption", addResult.GetPropertyOfJsonResult<string>("caption"));
			Assert.Equal("caption", getSingleResult.GetPropertyOfJsonResult<string>("caption"));
			Assert.Equal("caption",	getResult.GetPropertyOfFirstElementInJsonResult<string>("caption"));
			Assert.NotStrictEqual(new JsonResult(true), removeResult);
			Assert.NotStrictEqual(new JsonResult(false), afterRemoveResult);

			Dispose();
		}

		[Fact]
		public void InlineKeysActions_WithWrongParams()
		{
			Setup();

			_dataController.AddInlineKey("caption", "answer", "botId");

			var singleResult = _dataController.GetInlineKey("wrong");
			var result = _dataController.GetInlineKeys("wrong");

			var wrongAddResult = _dataController.AddInlineKey("", "", "");

			Assert.NotStrictEqual(new JsonResult(false), singleResult);
			Assert.NotStrictEqual(new JsonResult(false), result);
			Assert.NotStrictEqual(new JsonResult(false), wrongAddResult);

			Dispose();
		}

		[Fact]
		public void InterviewAnswersActions()
		{
			Setup();

			var addResult = _dataController.AddInterviewAnswer("interviewId", "userId", "answer", "botId");
			var id = addResult.GetPropertyOfJsonResult<string>("id");

			var getResult = _dataController.GetInterviewAnswers("botId");
			var getSingileResult = _dataController.GetInterviewAnswer(id);

			var removeResult = _dataController.RemoveInterviewAnswer(id);
			var afterRemoveResult = _dataController.GetInterviewAnswer(id);

			Assert.Equal("interviewId", getResult.GetPropertyOfFirstElementInJsonResult<string>("interviewId"));
			Assert.Equal("interviewId", getSingileResult.GetPropertyOfJsonResult<string>("interviewId"));
			Assert.NotStrictEqual(new JsonResult(true), removeResult);
			Assert.NotStrictEqual(new JsonResult(false), afterRemoveResult);

			Dispose();
		}

		[Fact]
		public void InterviewAnswersActions_WithWrongParams()
		{
			Setup();

			_dataController.AddInterviewAnswer("interviewId", "userId", "answer", "botId");

			var result = _dataController.GetInterviewAnswers("wrongId");

			var wrongAddResult = _dataController.AddInterviewAnswer("", "", "", "");

			Assert.NotStrictEqual(new JsonResult(false), result);
			Assert.NotStrictEqual(new JsonResult(false), wrongAddResult);

			Dispose();
		}

		[Fact]
		public void InterviewsActions()
		{
			Setup();

			var addResult = _dataController.AddInterview("name", "question", new List<string> {"answer1", "answer2"}, "botId");
			var id = addResult.GetPropertyOfJsonResult<string>("id");

			var getResult = _dataController.GetInterviews("botId");
			var getSingleResult = _dataController.GetInterview(id);

			var removeResult = _dataController.RemoveInterview(id);
			var afterRemoveResult = _dataController.GetInterview(id);

			Assert.Equal("name", getResult.GetPropertyOfFirstElementInJsonResult<string>("name"));
			Assert.Equal("name", getSingleResult.GetPropertyOfJsonResult<string>("name"));
			Assert.NotStrictEqual(new JsonResult(true), removeResult);
			Assert.NotStrictEqual(new JsonResult(false), afterRemoveResult);

			Dispose();
		}

		[Fact]
		public void InterviewsActions_WithWrongParams()
		{
			Setup();

			_dataController.AddInterview("name", "question", new List<string> {"answer1", "answer2"}, "botId");

			var result = _dataController.GetInterviews("wrong");

			var wrongAddResult = _dataController.AddInterview("", "", null, "");

			Assert.NotStrictEqual(new JsonResult(false), result);
			Assert.NotStrictEqual(new JsonResult(false), wrongAddResult);

			Dispose();
		}

		[Fact]
		public void TextMessageAnswersActions_GetTextMessageAnswers()
		{
			Setup();

			var addResult = _dataController.AddTextMessageAnswer("answer", "message", "botId");
			var id = addResult.GetPropertyOfJsonResult<string>("id");

			var getResult = _dataController.GetTextMessageAnswers("botId");
			var getSingleResult = _dataController.GetTextMessageAnswer(id);

			var removeResult = _dataController.RemoveTextMessageAnswer(id);
			var afterRemoveResult = _dataController.GetTextMessageAnswer(id);

			Assert.Equal("answer", getResult.GetPropertyOfFirstElementInJsonResult<string>("answer"));
			Assert.Equal("answer", getSingleResult.GetPropertyOfJsonResult<string>("answer"));
			Assert.NotStrictEqual(new JsonResult(true), removeResult);
			Assert.NotStrictEqual(new JsonResult(false), afterRemoveResult);

			Dispose();
		}

		[Fact]
		public void TextMessageAnswersActions_WithWrongParams()
		{
			Setup();

			_dataController.AddTextMessageAnswer("answer", "message", "botId");

			var result = _dataController.GetTextMessageAnswers("wrong");

			var wrongAddResult = _dataController.AddTextMessageAnswer("", "", "");

			Assert.NotStrictEqual(new JsonResult(false), result);
			Assert.NotStrictEqual(new JsonResult(false), wrongAddResult);

			Dispose();
		}

		[Fact]
		public void UsersActions()
		{
			Setup();

			var addResult = _dataController.AddUser("telegramId", "firstName", "lastName", "userName", "botId");
			var id = addResult.GetPropertyOfJsonResult<string>("id");

			var getResult = _dataController.GetUsers("botId");
			var getSingleResult = _dataController.GetUser("telegramId", "botId");

			var removeResult = _dataController.RemoveUser(id);
			var afterRemoveResult = _dataController.GetUser("telegramId", "botId");

			Assert.Equal("telegramId", getResult.GetPropertyOfFirstElementInJsonResult<string>("telegramId"));
			Assert.Equal("telegramId", getSingleResult.GetPropertyOfJsonResult<string>("telegramId"));
			Assert.NotStrictEqual(new JsonResult(true), removeResult);
			Assert.NotStrictEqual(new JsonResult(false), afterRemoveResult);

			Dispose();
		}

		[Fact]
		public void UsersActions_WithWrongParams()
		{
			Setup();

			_dataController.AddUser("telegramId", "firstName", "lastName", "userName", "botId");

			var result = _dataController.GetUsers("wrong");
			var singleResult = _dataController.GetUser("wrong", "wrong");

			var wrongAddResult = _dataController.AddUser("", "", "", "", "");

			Assert.NotStrictEqual(new JsonResult(false), result);
			Assert.NotStrictEqual(new JsonResult(false), singleResult);
			Assert.NotStrictEqual(new JsonResult(false), wrongAddResult);

			Dispose();
		}
	}
}