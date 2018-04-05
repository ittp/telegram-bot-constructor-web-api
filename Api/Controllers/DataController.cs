using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Api.Controllers
{
	public class DataController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly BotsRepository _botsRepository;
		private readonly EventsRepository _eventsRepository;
		private readonly InlineKeysRepository _inlineKeysRepository;
		private readonly InlineUrlKeysRepository _inlineUrlKeysRepository;
		private readonly InterviewAnswersRepository _interviewAnswersRepository;
		private readonly InterviewsRepository _interviewsRepository;
		private readonly TextMessageAnswersRepository _textMessageAnswersRepository;
		private readonly UsersRepository _usersRepository;

		public DataController(IConfiguration configuration, BotsRepository botsRepository, EventsRepository eventsRepository, UsersRepository usersRepository, TextMessageAnswersRepository textMessageAnswersRepository, InterviewsRepository interviewsRepository, InterviewAnswersRepository interviewAnswersRepository, InlineUrlKeysRepository inlineUrlKeysRepository, InlineKeysRepository inlineKeysRepository)
		{
			_configuration = configuration;
			_botsRepository = botsRepository;
			_eventsRepository = eventsRepository;
			_usersRepository = usersRepository;
			_textMessageAnswersRepository = textMessageAnswersRepository;
			_interviewsRepository = interviewsRepository;
			_interviewAnswersRepository = interviewAnswersRepository;
			_inlineUrlKeysRepository = inlineUrlKeysRepository;
			_inlineKeysRepository = inlineKeysRepository;
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

			var botDto = _botsRepository.GetBot(id);

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/events")]
		[HttpGet]
		public JsonResult GetEvents(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var eventsDto = _eventsRepository.GetEvents(botId);

			return eventsDto != null
				? Json(eventsDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/add-event")]
		[HttpPost]
		public JsonResult AddEvent(string botId, string text)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);
			if (string.IsNullOrEmpty(text)) return Json(false);

			var eventDto = _eventsRepository.AddEvent(new Event
			{
				BotId = botId,
				Text = text
			});

			return eventDto != null
				? Json(eventDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-event")]
		[HttpPost]
		public JsonResult RemoveEvent(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _eventsRepository.RemoveEvent(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/bot-networking")]
		[HttpGet]
		public JsonResult GetBotNetworking(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var botDto = _botsRepository.GetBot(id);

			return botDto != null
				? Json(botDto.NetworkingEnabled)
				: Json(false);
		}

		[Route("/api/bot-cognitive")]
		[HttpGet]
		public JsonResult GetBotCognitiveServiceStatus(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var botDto = _botsRepository.GetBot(id);

			return botDto != null
				? Json(botDto.CognitiveServicesEnabled)
				: Json(false);
		}

		[Route("/api/bot-cognitive")]
		[HttpPost]
		public JsonResult SetBotCognitiveServiceStatus(string id, bool? status)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);
			if (status == null) return Json(false);

			var botDto = _botsRepository.SetCognitiveServicesStatus(id, status ?? false);

			return botDto != null
				? Json(botDto)
				: Json(false);
		}

		[Route("/api/bot-networking")]
		[HttpPost]
		public JsonResult SetBotNetworking(string id, bool? networkingEnabled)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);
			if (networkingEnabled == null) return Json(false);

			var botDto = _botsRepository.SetNetWorkingStatus(id, networkingEnabled ?? false);

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/bots")]
		[HttpGet]
		public JsonResult GetBots()
		{
			var botsDto = _botsRepository.GetBots();

			return botsDto != null
				? Json(botsDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/bot-by-token")]
		[HttpGet]
		public JsonResult GetBotByToken(string token)
		{
			if (string.IsNullOrEmpty(token)) return Json(false);

			var botDto = _botsRepository.GetBotByToken(token);

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

			var botDto = _botsRepository.AddBot(new Bot
			{
				Name = name,
				Token = token,
				NetworkingEnabled = true,
				CognitiveServicesEnabled = true,
				StartMessage = "Hello"
			});

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/set-start-message")]
		[HttpPost]
		public JsonResult SetStartMessage(string id, string message)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);
			if (string.IsNullOrEmpty(message)) return Json(false);

			var botDto = _botsRepository.SetStartMessage(id, message);

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/get-start-message")]
		[HttpGet]
		public JsonResult GetStartMessage(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var startMessage = _botsRepository.GetStartMessage(id);

			return startMessage != null
				? Json(startMessage)
				: Json(false);
		}

		[Route("/api/remove-bot")]
		[HttpPost]
		public JsonResult RemoveBot(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _botsRepository.RemoveBot(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/text-message-answers")]
		[HttpGet]
		public JsonResult GetTextMessageAnswers(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var textMessageAnswersDto = _textMessageAnswersRepository.GetTextMessageAnswers(botId);

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

			var textMessageAnswerDto = _textMessageAnswersRepository.AddTextMessageAnswer(new TextMessageAnswer
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

			var textMessageAnswerDto = _textMessageAnswersRepository.GetTextMessageAnswer(id);

			return textMessageAnswerDto != null
				? Json(textMessageAnswerDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-text-message-answer")]
		[HttpPost]
		public JsonResult RemoveTextMessageAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _textMessageAnswersRepository.RemoveTextMessageAnswer(id);

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

			var userDto = _usersRepository.AddUser(new User
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

			var usersDto = _usersRepository.GetUsers(botId);

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

			var userDto = _usersRepository.GetUser(telegramId, botId);

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

			var updatedUserDto = _usersRepository.SetNetworking(telegramId, botId, networking);

			return updatedUserDto != null
				? Json(updatedUserDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-user")]
		[HttpPost]
		public JsonResult RemoveUser(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _usersRepository.RemoveUser(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/add-inline-key")]
		[HttpPost]
		public JsonResult AddInlineKey(string caption, string answer, string botId)
		{
			if (string.IsNullOrEmpty(caption)) return Json(false);
			if (string.IsNullOrEmpty(answer)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeyDto = _inlineKeysRepository.AddInlineKey(new InlineKey
			{
				Caption = caption,
				Answer = answer,
				BotId = botId
			});

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}

		[Route("/api/add-inline-url-key")]
		[HttpPost]
		public JsonResult AddUrlKey(string caption, string url, string botId)
		{
			if (string.IsNullOrEmpty(caption)) return Json(false);
			if (string.IsNullOrEmpty(url)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeyDto = _inlineUrlKeysRepository.AddInlineUrlKey(new InlineUrlKey
			{
				Caption = caption,
				Url = url,
				BotId = botId
			});

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}

		[Route("/api/inline-url-keys")]
		[HttpGet]
		public JsonResult GetInlineUrlKeys(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeysDto = _inlineUrlKeysRepository.GetUrlInlineUrlKeys(botId);

			return inlineKeysDto != null
				? Json(inlineKeysDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/inline-url-key")]
		[HttpGet]
		public JsonResult GetInlineUrlKey(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var inlineKeyDto = _inlineUrlKeysRepository.GetUrlInlineUrlKey(id);

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}

		[Route("/api/inline-keys")]
		[HttpGet]
		public JsonResult GetInlineKeys(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeysDto = _inlineKeysRepository.GetInlineKeys(botId);

			return inlineKeysDto != null
				? Json(inlineKeysDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/inline-key")]
		[HttpGet]
		public JsonResult GetInlineKey(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var inlineKeyDto = _inlineKeysRepository.GetInlineKey(id);

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-inline-key")]
		[HttpPost]
		public JsonResult RemoveInlineKey(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _inlineKeysRepository.RemoveInlineKey(id);

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

			var interviewDto = _interviewsRepository.AddInterview(new Interview
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

			var interviewsDto = _interviewsRepository.GetInterviews(botId);

			return interviewsDto != null
				? Json(interviewsDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/interview")]
		[HttpGet]
		public JsonResult GetInterview(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var interviewDto = _interviewsRepository.GetInterview(id);

			return interviewDto != null
				? Json(interviewDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-interview")]
		[HttpPost]
		public JsonResult RemoveInterview(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _interviewsRepository.RemoveInterview(id);

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

			var interviewAnswerDto = _interviewAnswersRepository.AddInterviewAnswer(new InterviewAnswer
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

			var interviewAnswersDto = _interviewAnswersRepository.GetInterviewAnswers(botId);

			return interviewAnswersDto != null
				? Json(interviewAnswersDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/interview-answer")]
		[HttpGet]
		public JsonResult GetInterviewAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var interviewAnswerDto = _interviewAnswersRepository.GetInterviewAnswer(id);

			return interviewAnswerDto != null
				? Json(interviewAnswerDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-interview-answer")]
		[HttpPost]
		public JsonResult RemoveInterviewAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _interviewAnswersRepository.RemoveInterviewAnswer(id);

			return result ? Json(true) : Json(false);
		}
	}
}