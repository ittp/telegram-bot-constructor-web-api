using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
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

		public async Task<IActionResult> Index()
		{
			var botsViewModels = await BotsService.GetBotsViewModels(_configuration, _botsRepository);

			return View(new PageViewModel
			{
				Bots = botsViewModels
			});
		}

		[Route("/bots/start")]
		public async Task<RedirectResult> Start(string id)
		{
			await _httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/start?id={id}");

			return Redirect($"/bot?id={id}");
		}

		[Route("/bots/stop")]
		public async Task<RedirectResult> Stop(string id)
		{
			await _httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/stop?id={id}");

			return Redirect($"/bot?id={id}");
		}
		
		[Route("/bots/new")]
		public async Task<IActionResult> NewBot(string name, string token, string message)
		{
			var botsViewModels = await BotsService.GetBotsViewModels(_configuration, _botsRepository);

			return View(new PageViewModel
			{
				Bots = botsViewModels.ToArray()
			});
		}
		
		[Route("/bots/add")]
		[HttpPost]
		public async Task<IActionResult> AddBot(string name, string token, string message)
		{
			var botDto = _botsRepository.AddBot(new Bot
			{
				Name = name,
				Token = token,
				NetworkingEnabled = true,
				CognitiveServicesEnabled = true,
				StartMessage = message
			});
			
			return Redirect($"/bot?id={botDto.Id}");
		}

		[Route("/bot")]
		public async Task<IActionResult> Bot(string id)
		{
			var botsViewModels = await BotsService.GetBotsViewModels(_configuration, _botsRepository);
			var botViewModel = await BotsService.GetBotViewModel(id, _configuration, _botsRepository);

			var textMessages = _textMessageAnswersRepository.GetTextMessageAnswers(id);
			var inlineKeys = _inlineKeysRepository.GetInlineKeys(id);
			var inlineUrlKeys = _inlineUrlKeysRepository.GetUrlInlineUrlKeys(id);
			var interviews = _interviewsRepository.GetInterviews(id);

			return View(new PageViewModel
			{
				CurrentBot = botViewModel,
				Bots = botsViewModels,
				TextMessages = textMessages,
				InlineKeys = inlineKeys,
				InlineUrlKeys = inlineUrlKeys,
				Interviews = interviews
			});
		}
	}
}