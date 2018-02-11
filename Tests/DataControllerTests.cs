using System.Collections.Generic;
using System.Linq;
using Api.Controllers;
using Api.Models;
using Mongo2Go;
using Xunit;

namespace Tests
{
	public class DataControllerTests
	{
		private readonly Repository _repository;
		private readonly DataController _dataController;

		public DataControllerTests()
		{
			var fakeDb = MongoDbRunner.Start();
			_repository = new Repository(fakeDb.ConnectionString, "TestBase");
			_dataController = new DataController(_repository);
		}

		[Fact]
		public void AddBot_GetBotByToken()
		{
			_dataController.AddBot("name", "token");

			var result = _dataController.GetBotByToken("token");

			Assert.Contains("name" , result.Value.ToString());
		}

		[Fact]
		public void AddTextMessageAnswer_GetTextMessageAnswers()
		{
			_dataController.AddTextMessageAnswer("answer", "message", "botId");

			var result = _dataController.GetTextMessageAnswers("botId");

			Assert.Contains("message",((IEnumerable<object>)result.Value).First().ToString());
		}

		[Fact]
		public void AddUser_GetUsers()
		{
			_dataController.AddUser("telegramId", "firstName", "lastName", "userName", "botId");

			var result = _dataController.GetUsers("botId");

			Assert.Contains("telegramId", ((IEnumerable<object>)result.Value).First().ToString());
		}

		[Fact]
		public void AddUser_GetUser()
		{
			_dataController.AddUser("telegramId", "firstName", "lastName", "userName", "botId");

			var user = _repository.GetUsers("botId").FirstOrDefault();

			var result = _dataController.GetUser(user.Id.ToString(), user.BotId);

			Assert.Contains("telegramId", ((IEnumerable<object>)result.Value).First().ToString());
		}

		[Fact]
		public void AddInlineKey_GetInlineKeys()
		{
			_dataController.AddInlineKey("caption", "answer", "botId");

			var result = _dataController.GetInlineKeys("botId");

			Assert.Contains("caption", ((IEnumerable<object>)result.Value).First().ToString());
		}

//		[Fact]
//		public void AddInterview_GetInterviews()
//		{
//			_dataController.AddInterview("name", "question", new [] {"answer1", "answer2"}, "botId");
//
//			var result = _dataController.GetInterviews("botId");
//
//			Assert.Contains("question", ((IEnumerable<object>)result.Value).First().ToString());
//		}
}
}