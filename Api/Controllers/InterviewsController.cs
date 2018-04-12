using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    public class InterviewsController : Controller
    {
        private readonly InterviewsRepository _interviewsRepository;
        private readonly IConfiguration _configuration;
        private readonly BotsRepository _botsRepository;

        public InterviewsController(BotsRepository botsRepository,
            IConfiguration configuration, InterviewsRepository interviewsRepository)
        {
            _configuration = configuration;
            _interviewsRepository = interviewsRepository;
            _botsRepository = botsRepository;
        }

        [Route("/interviews/new")]
        [HttpGet]
        public async Task<IActionResult> NewInterview(string botId)
        {
            var bots = await BotsService.GetBotsViewModels(_configuration, _botsRepository);
            var bot = await BotsService.GetBotViewModel(botId, _configuration, _botsRepository);

            return View(new PageViewModel
            {
                CurrentBot = bot,
                Bots = bots
            });
        }

        [Route("/interviews/add")]
        [HttpPost]
        public IActionResult AddInterview(string name, string question, string botId, List<string> answers)
        {
            _interviewsRepository.AddInterview(new Interview
            {
                Name = name,
                Question = question,
                Answers = answers,
                BotId = botId
            });

            return Redirect("/bot?id=" + botId);
        }

        [Route("/interviews/remove")]
        [HttpPost]
        public IActionResult RemoveInterview(string id, string botId)
        {
            _interviewsRepository.RemoveInterview(id);

            return Redirect("/bot?id=" + botId);
        }
    }
}