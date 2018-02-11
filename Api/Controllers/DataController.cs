using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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

		[Route("/api/text-message-answers")]
		[HttpGet]
		public JsonResult GetTextMessageAnswers(string botId)
		{
			return new JsonResult(_repository.GetTextMessageAnswers(botId).Select(x => x.Transform()));
		}

	}
}