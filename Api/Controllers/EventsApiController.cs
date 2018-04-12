using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class EventsApiController : Controller
	{
		private readonly EventsRepository _eventsRepository;

		public EventsApiController(EventsRepository eventsRepository)
		{
			_eventsRepository = eventsRepository;
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

		[Route("/api/user-events")]
		[HttpGet]
		public JsonResult GetUserEvents(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var eventsDto = _eventsRepository.GetUserEvents(botId);

			return eventsDto != null
				? Json(eventsDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/events/add")]
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

		[Route("/api/events/remove")]
		[HttpPost]
		public JsonResult RemoveEvent(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _eventsRepository.RemoveEvent(id);

			return result ? Json(true) : Json(false);
		}

		[Route("/api/user-events/remove")]
		[HttpPost]
		public JsonResult RemoveUserEvent(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _eventsRepository.RemoveUserEvent(id);

			return result ? Json(true) : Json(false);
		}

	}
}