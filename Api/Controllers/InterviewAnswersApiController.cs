using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class InterviewAnswersApiController : Controller
	{
		private readonly InterviewAnswersRepository _interviewAnswersRepository;

		public InterviewAnswersApiController(InterviewAnswersRepository interviewAnswersRepository)
		{
			_interviewAnswersRepository = interviewAnswersRepository;
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