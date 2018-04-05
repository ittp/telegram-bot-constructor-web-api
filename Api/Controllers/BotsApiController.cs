using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class BotsApiController : Controller
	{
		private readonly BotsRepository _botsRepository;

		public BotsApiController(BotsRepository botsRepository)
		{
			_botsRepository = botsRepository;
		}

		[Route("/api/bots/bot")]
		[HttpGet]
		public JsonResult GetBot(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var botDto = _botsRepository.GetBot(id);

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}


		[Route("/api/bots/networking")]
		[HttpGet]
		public JsonResult GetBotNetworking(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var botDto = _botsRepository.GetBot(id);

			return botDto != null
				? Json(botDto.NetworkingEnabled)
				: Json(false);
		}

		[Route("/api/bots/cognitive")]
		[HttpGet]
		public JsonResult GetBotCognitiveServiceStatus(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var botDto = _botsRepository.GetBot(id);

			return botDto != null
				? Json(botDto.CognitiveServicesEnabled)
				: Json(false);
		}

		[Route("/api/bots/cognitive/set")]
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

		[Route("/api/bots/networking")]
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

		[Route("/api/bots/bot/by-token")]
		[HttpGet]
		public JsonResult GetBotByToken(string token)
		{
			if (string.IsNullOrEmpty(token)) return Json(false);

			var botDto = _botsRepository.GetBotByToken(token);

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/bots/add")]
		[HttpPost]
		public JsonResult AddBot(string name, string token, string message)
		{
			if (string.IsNullOrEmpty(name)) return Json(false);
			if (string.IsNullOrEmpty(token)) return Json(false);

			var botDto = _botsRepository.AddBot(new Bot
			{
				Name = name,
				Token = token,
				NetworkingEnabled = true,
				CognitiveServicesEnabled = true,
				StartMessage = message
			});

			return botDto != null
				? Json(botDto.Transform())
				: Json(false);
		}

		[Route("/api/bots/message/set")]
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

		[Route("/api/bots/message")]
		[HttpGet]
		public JsonResult GetStartMessage(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var startMessage = _botsRepository.GetStartMessage(id);

			return startMessage != null
				? Json(startMessage)
				: Json(false);
		}

		[Route("/api/bots/remove")]
		[HttpPost]
		public JsonResult RemoveBot(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _botsRepository.RemoveBot(id);

			return result ? Json(true) : Json(false);
		}
	}
}