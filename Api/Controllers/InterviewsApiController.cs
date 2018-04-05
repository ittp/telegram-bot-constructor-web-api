using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
	public class InterviewsApiController: Controller
	{
		private readonly InterviewsRepository _interviewsRepository;

		public InterviewsApiController(InterviewsRepository interviewsRepository)
		{
			_interviewsRepository = interviewsRepository;
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
	}
}