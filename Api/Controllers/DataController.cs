using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders.BinaryEncoders;

namespace Api.Controllers
{
	public class DataController : Controller
	{
		private readonly Repository _repository;

		public DataController(Repository repository)
		{
			_repository = repository;
		}

		[Route("/api/bot-by-token")]
		[HttpGet]
		public JsonResult GetBotByToken(string token)
		{
			return new JsonResult(_repository.GetBotByToken(token).Transform());
		}

		[Route("/api/add-bot")]
		[HttpPost]
		public void AddBot(string name, string token)
		{
			_repository.AddBot(new Bot
			{
				Name = name,
				Token = token
			});
		}

		[Route("/api/text-message-answers")]
		[HttpGet]
		public JsonResult GetTextMessageAnswers(string botId)
		{
			return new JsonResult(_repository.GetTextMessageAnswers(botId).Select(x => x.Transform()));
		}

		[Route("/api/add-text-message-answer")]
		[HttpPost]
		public void AddTextMessageAnswer(string answer, string message, string botId)
		{
			_repository.AddTextMessageAnswer(new TextMessageAnswer
			{
				Answer = answer,
				Message = message,
				BotId = botId
			});
		}

		[Route("/api/add-user")]
		[HttpPost]
		public void AddUser(string telegramId, string firstName, string lastName, string userName, string botId)
		{
			_repository.AddUser(new User
			{
				BotId = botId,
				FirstName = firstName,
				LastName = lastName,
				TelegramId = telegramId,
				UserName = userName
			});
		}

		[Route("/api/users")]
		[HttpGet]
		public JsonResult GetUsers(string botId)
		{
			return new JsonResult(_repository.GetUsers(botId).Select(x => x.Transform()));
		}
	}
}