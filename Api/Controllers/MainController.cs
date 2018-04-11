using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Api.Controllers
{
	public partial class MainController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly UsersRepository _usersRepository;
		private readonly BotsRepository _botsRepository;
		private readonly TextMessageAnswersRepository _textMessageAnswersRepository;
		private readonly InterviewsRepository _interviewsRepository;
		private readonly InterviewAnswersRepository _interviewAnswersRepository;
		private readonly InlineUrlKeysRepository _inlineUrlKeysRepository;
		private readonly InlineKeysRepository _inlineKeysRepository;
		private readonly HttpClient _httpClient;

		public MainController(IConfiguration configuration, UsersRepository usersRepository, BotsRepository botsRepository,
			TextMessageAnswersRepository textMessageAnswersRepository, InterviewsRepository interviewsRepository,
			InterviewAnswersRepository interviewAnswersRepository, InlineUrlKeysRepository inlineUrlKeysRepository,
			InlineKeysRepository inlineKeysRepository)
		{
			_configuration = configuration;
			_usersRepository = usersRepository;
			_botsRepository = botsRepository;
			_textMessageAnswersRepository = textMessageAnswersRepository;
			_interviewsRepository = interviewsRepository;
			_interviewAnswersRepository = interviewAnswersRepository;
			_inlineUrlKeysRepository = inlineUrlKeysRepository;
			_inlineKeysRepository = inlineKeysRepository;
			_httpClient = new HttpClient();
		}

		public IActionResult Index()
		{
			var bots = _botsRepository.GetBots();


			return View(bots);
		}


		public IActionResult TextMessages()
		{
			var bots = _botsRepository.GetBots();


			return View(bots);
		}

		[Route("/bot")]
		public async Task<IActionResult> Bot(string id)
		{
			var bot = _botsRepository.GetBot(id);

			var result = await _httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/check?id={id}");
			var parsedResult = JsonConvert.DeserializeObject<Response>(result);
			var startMessage = _botsRepository.GetStartMessage(id);

			var botViewModel = new BotViewModel
			{
				Bot = bot,
				StartMessage = startMessage,
				Status = Convert.ToBoolean(parsedResult.status)
			};
			
			return View(new BotPageViewModel
			{
				CurrentBot = botViewModel,
				Bots = new List<BotViewModel>
				{
					botViewModel
				}
			});
		}
	}
}