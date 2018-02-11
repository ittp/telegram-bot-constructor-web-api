using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

		[Route("/")]
		[HttpGet]
		public List<string> Index()
		{
			return new List<string>
			{
				"get /api/bot-by-token token",
				"post /api/add-bot name,token",
				"get /api/text-message-answers botId",
				"post /api/add-text-message-answer answer,message,botId",
				"post /api/add-user telegramId,firstName,lastName,userName,botId",
				"get /api/users botId",
				"get /api/user telegramId,botId",
				"post /api/add-inline-key caption,answer,botId",
				"get /api/inline-keys botId",
				"post /api/add-interview name, question, answers, botId",
				"get /api/interviews botId"
			};
		}

		[Route("/api/bot-by-token")]
		[HttpGet]
		public JsonResult GetBotByToken(string token)
		{
			var botDto = _repository.GetBotByToken(token);

			return botDto != null ? Json(botDto.Transform()) : null;
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
			var textMessageAnswersDto = _repository.GetTextMessageAnswers(botId);

			return textMessageAnswersDto != null ? Json(textMessageAnswersDto.Select(x => x.Transform())) : null;
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
			var usersDto = _repository.GetUsers(botId);

			return usersDto != null ? Json(usersDto.Select(x => x.Transform())) : null;
		}

		[Route("/api/user")]
		[HttpGet]
		public JsonResult GetUser(string telegramId, string botId)
		{
			var userDto = _repository.GetUser(telegramId, botId);

			return userDto != null ? Json(userDto.Transform()) : null;
		}

		[Route("/api/add-inline-key")]
		[HttpPost]
		public void AddInlineKey(string caption, string answer, string botId)
		{
			_repository.AddInlineKey(new InlineKey
			{
				Caption = caption,
				Answer = answer,
				BotId = botId
			});
		}

		[Route("/api/inline-keys")]
		[HttpGet]
		public JsonResult GetInlineKeys(string botId)
		{
			var inlineKeysDto = _repository.GetInlineKeys(botId);

			return inlineKeysDto != null ? Json(inlineKeysDto.Select(x => x.Transform())) : null;
		}

		[Route("/api/add-interview")]
		[HttpPost]
		public void AddInterview(string name, string question, List<string> answers, string botId)
		{
			_repository.AddInterview(new Interview
			{
				Name = name,
				Question = question,
				BotId = botId,
				Answers = answers.ToList()
			});
		}

		[Route("/api/interviews")]
		[HttpGet]
		public JsonResult GetInterviews(string botId)
		{
			var interviewsDto = _repository.GetInterviews(botId);

			return interviewsDto != null ? Json(interviewsDto.Select(x => x.Transform())) : null;
		}
	}
}