using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class TextMessageAnswersApiController: Controller
	{
		private readonly TextMessageAnswersRepository _textMessageAnswersRepository;

		public TextMessageAnswersApiController(TextMessageAnswersRepository textMessageAnswersRepository)
		{
			_textMessageAnswersRepository = textMessageAnswersRepository;
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

		[Route("/api/text-message-answers/add")]
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

		[Route("/api/text-message-answers/text-message-answer")]
		[HttpGet]
		public JsonResult GetTextMessageAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var textMessageAnswerDto = _textMessageAnswersRepository.GetTextMessageAnswer(id);

			return textMessageAnswerDto != null
				? Json(textMessageAnswerDto.Transform())
				: Json(false);
		}

		[Route("/api/text-message-answers/remove")]
		[HttpPost]
		public JsonResult RemoveTextMessageAnswer(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _textMessageAnswersRepository.RemoveTextMessageAnswer(id);

			return result ? Json(true) : Json(false);
		}

	}
}