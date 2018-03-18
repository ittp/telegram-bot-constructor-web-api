using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Api.Controllers
{
	public class DataController : Controller
	{
		private readonly Repository _repository;
		private readonly IConfiguration _configuration;

		public DataController(Repository repository, IConfiguration configuration)
		{
			_repository = repository;
			_configuration = configuration;
		}

		[Route("/")]
		[HttpGet]
		public string Index()
		{
			return _configuration["Version"];
		}

		[Route("/api/bot")]
		[HttpGet]
		public JsonResult GetBot(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var botDto = _repository.GetBot(id);

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/bots")]
		[HttpGet]
		public JsonResult GetBots()
		{
			var botsDto = _repository.GetBots();

			return botsDto != null
				? Json(botsDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/bot-by-token")]
		[HttpGet]
		public JsonResult GetBotByToken(string token)
		{
			if (string.IsNullOrEmpty(token)) return Json(false);

			var botDto = _repository.GetBotByToken(token);

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/add-bot")]
		[HttpPost]
		public JsonResult AddBot(string name, string token)
		{
			if (string.IsNullOrEmpty(name)) return Json(false);
			if (string.IsNullOrEmpty(token)) return Json(false);

			var botDto = _repository.AddBot(new Bot
			{
				Name = name,
				Token = token
			});

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-bot")]
		[HttpPost]
		public JsonResult RemoveBot(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _repository.RemoveBot(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/text-message-answers")]
		[HttpGet]
		public JsonResult GetTextMessageAnswers(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var textMessageAnswersDto = _repository.GetTextMessageAnswers(botId);

			return textMessageAnswersDto != null
				? Json(textMessageAnswersDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/add-text-message-answer")]
		[HttpPost]
		public JsonResult AddTextMessageAnswer(string answer, string message, string botId)
		{
			if (string.IsNullOrEmpty(answer)) return Json(false);
			if (string.IsNullOrEmpty(message)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var textMessageAnswerDto = _repository.AddTextMessageAnswer(new TextMessageAnswer
			{
				Answer = answer,
				Message = message,
				BotId = botId
			});

			return textMessageAnswerDto != null
				? Json(textMessageAnswerDto.Transform())
				: Json(false);
		}

		[Route("/api/text-message-answer")]
		[HttpGet]
		public JsonResult GetTextMessageAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var textMessageAnswerDto = _repository.GetTextMessageAnswer(id);

			return textMessageAnswerDto != null
				? Json(textMessageAnswerDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-text-message-answer")]
		[HttpPost]
		public JsonResult RemoveTextMessageAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _repository.RemoveTextMessageAnswer(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/add-user")]
		[HttpPost]
		public JsonResult AddUser(string telegramId, string firstName, string lastName, string userName, string botId)
		{
			if (string.IsNullOrEmpty(telegramId)) return Json(false);
			if (string.IsNullOrEmpty(firstName)) return Json(false);
			if (string.IsNullOrEmpty(lastName)) return Json(false);
			if (string.IsNullOrEmpty(userName)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

            var networking = JsonConvert.SerializeObject(new object());

			var userDto = _repository.AddUser(new User
			{
				BotId = botId,
				FirstName = firstName,
				LastName = lastName,
				TelegramId = telegramId,
				UserName = userName,
                Networking = networking
			});

			return userDto != null
				? Json(userDto.Transform())
				: Json(false);
		}

		[Route("/api/users")]
		[HttpGet]
		public JsonResult GetUsers(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var usersDto = _repository.GetUsers(botId);

			return usersDto != null
				? Json(usersDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/user")]
		[HttpGet]
		public JsonResult GetUser(string telegramId, string botId)
		{
			if (string.IsNullOrEmpty(telegramId)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var userDto = _repository.GetUser(telegramId, botId);

			return userDto != null
				? Json(userDto.Transform())
				: Json(false);
		}

        [Route("/api/set-networking")]
        [HttpPost]
        public JsonResult SetNetworking(string telegramId, string botId, string networking)
        {
            if (string.IsNullOrEmpty(telegramId)) return Json(false);
            if (string.IsNullOrEmpty(botId)) return Json(false);
            if (string.IsNullOrEmpty(networking)) return Json(false);

            var updatedUserDto = _repository.SetNetworking(telegramId, botId, networking);

            return updatedUserDto != null
                ? Json(updatedUserDto.Transform())
                : Json(false);
        }

		[Route("/api/remove-user")]
		[HttpPost]
		public JsonResult RemoveUser(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _repository.RemoveUser(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/add-inline-key")]
		[HttpPost]
		public JsonResult AddInlineKey(string caption, string answer, string botId)
		{
			if (string.IsNullOrEmpty(caption)) return Json(false);
			if (string.IsNullOrEmpty(answer)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeyDto = _repository.AddInlineKey(new InlineKey
			{
				Caption = caption,
				Answer = answer,
				BotId = botId
			});

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}

		[Route("/api/inline-keys")]
		[HttpGet]
		public JsonResult GetInlineKeys(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeysDto = _repository.GetInlineKeys(botId);

			return inlineKeysDto != null
				? Json(inlineKeysDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/inline-key")]
		[HttpGet]
		public JsonResult GetInlineKey(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var inlineKeyDto = _repository.GetInlineKey(id);

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-inline-key")]
		[HttpPost]
		public JsonResult RemoveInlineKey(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _repository.RemoveInlineKey(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/add-interview")]
		[HttpPost]
		public JsonResult AddInterview(string name, string question, string answers, string botId)
		{
			if (string.IsNullOrEmpty(answers)) return Json(false);
			if (string.IsNullOrEmpty(name)) return Json(false);
			if (string.IsNullOrEmpty(question)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var parsedAnswers = JsonConvert.DeserializeObject<List<string>>(answers);

			var interviewDto = _repository.AddInterview(new Interview
			{
				Name = name,
				Question = question,
				BotId = botId,
				Answers = parsedAnswers
			});

			return interviewDto != null
				? Json(interviewDto.Transform())
				: Json(false);
		}

		[Route("/api/interviews")]
		[HttpGet]
		public JsonResult GetInterviews(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var interviewsDto = _repository.GetInterviews(botId);

			return interviewsDto != null
				? Json(interviewsDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/interview")]
		[HttpGet]
		public JsonResult GetInterview(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var interviewDto = _repository.GetInterview(id);

			return interviewDto != null
				? Json(interviewDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-interview")]
		[HttpPost]
		public JsonResult RemoveInterview(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _repository.RemoveInterview(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/add-interview-answer")]
		[HttpPost]
		public JsonResult AddInterviewAnswer(string interviewId, string userId, string answer, string botId)
		{
			if (string.IsNullOrEmpty(interviewId)) return Json(false);
			if (string.IsNullOrEmpty(userId)) return Json(false);
			if (string.IsNullOrEmpty(answer)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var interviewAnswerDto = _repository.AddInterviewAnswer(new InterviewAnswer
			{
				Answer = answer,
				InterviewId = interviewId,
				UserId = userId,
				BotId = botId
			});

			return interviewAnswerDto != null
				? Json(interviewAnswerDto.Transform())
				: Json(false);
		}

		[Route("/api/interview-answers")]
		[HttpGet]
		public JsonResult GetInterviewAnswers(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var interviewAnswersDto = _repository.GetInterviewAnswers(botId);

			return interviewAnswersDto != null
				? Json(interviewAnswersDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/interview-answer")]
		[HttpGet]
		public JsonResult GetInterviewAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var interviewAnswerDto = _repository.GetInterviewAnswer(id);

			return interviewAnswerDto != null
				? Json(interviewAnswerDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-interview-answer")]
		[HttpPost]
		public JsonResult RemoveInterviewAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _repository.RemoveInterviewAnswer(id);

			return result ? Json(true) : Json(false);
		}
	}
}