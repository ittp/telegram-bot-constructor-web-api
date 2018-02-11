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

			var result = _dataController.GetBotByToken("token").Value;

			Assert.Contains("name" , result.ToString());
		}

		[Fact]
		public void AddTextMessageAnswer_GetTextMessageAnswers()
		{
			_dataController.AddTextMessageAnswer("answer", "message", "botId");

			var result = _dataController.GetTextMessageAnswers("botId").Value;

			Assert.Contains("message",((IEnumerable<object>)result).First().ToString());
		}

		[Fact]
		public void AddUser_GetUsers()
		{
			_dataController.AddUser("telegramId", "firstName", "lastName", "userName", "botId");

			var result = _dataController.GetUsers("botId").Value;

			Assert.Contains("telegramId", ((IEnumerable<object>)result).First().ToString());
		}
}
}