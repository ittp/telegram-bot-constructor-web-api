using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[AuthenticationAttribute]
	public class EventsController : Controller
	{
		private readonly EventsRepository _eventsRepository;

		public EventsController(EventsRepository eventsRepository)
		{
			_eventsRepository = eventsRepository;
		}

		[Route("/events/add")]
		[HttpPost]
		public IActionResult AddEvent(string botId, string text)
		{
			_eventsRepository.AddEvent(new Event
			{
				BotId = botId,
				Text = text
			});

			return Redirect("/bot?id=" + botId);
		}

		[Route("/user-events/add")]
		[HttpPost]
		public IActionResult AddUserEvent(string botId, string userTelegramId, string text)
		{
			_eventsRepository.AddUserEvent(new UserEvent
			{
				BotId = botId,
				UserTelegramId = userTelegramId,
				Text = text
			});

			return Redirect("/bot?id=" + botId);
		}
	}
}