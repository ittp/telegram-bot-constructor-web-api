using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<IActionResult> Index()
		{
			var bots = _botsRepository.GetBots();

			var botsViewModels = await Task.WhenAll(bots.Select(async _ =>
			{
				var result = await _httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/check?id={_.Id}");
				var parsedResult = JsonConvert.DeserializeObject<Response>(result);
				return new BotViewModel
				{
					Bot = _,
					Status = parsedResult.status
				};
			}));

			return View(new PageViewModel
			{
				Bots = botsViewModels.ToArray()
			});
		}

		[Route("/bot")]
		public async Task<IActionResult> Bot(string id)
		{
			var bot = _botsRepository.GetBot(id);

			var currentBotResult = await _httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/check?id={id}");
			var currentBotParsedResult = JsonConvert.DeserializeObject<Response>(currentBotResult);
			var textMessages = _textMessageAnswersRepository.GetTextMessageAnswers(id);
			var inlineKeys = _inlineKeysRepository.GetInlineKeys(id);
			var inlineUrlKeys = _inlineUrlKeysRepository.GetUrlInlineUrlKeys(id);
			var interviews = _interviewsRepository.GetInterviews(id);

			var bots = _botsRepository.GetBots();

			var botsViewModels = await Task.WhenAll(bots.Select(async _ =>
			{
				var result = await _httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/check?id={_.Id}");
				var parsedResult = JsonConvert.DeserializeObject<Response>(result);
				return new BotViewModel
				{
					Bot = _,
					Status = parsedResult.status
				};
			}));

			var botViewModel = new BotViewModel
			{
				Bot = bot,
				Status = Convert.ToBoolean(currentBotParsedResult.status),
				TextMessages = textMessages,
				InlineKeys = inlineKeys,
				InlineUrlKeys = inlineUrlKeys,
				Interviews = interviews
			};

			return View(new PageViewModel
			{
				CurrentBot = botViewModel,
				Bots = botsViewModels
			});
		}
	}
}