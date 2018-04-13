using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
	[AuthenticationAttribute]
	public class InlineKeysController : Controller
	{
		private readonly InlineKeysRepository _inlineKeysRepository;
		private readonly IConfiguration _configuration;
		private readonly BotsRepository _botsRepository;
		private readonly SystemUserRepository _systemUserRepository;

		public InlineKeysController(InlineKeysRepository inlineKeysRepository, BotsRepository botsRepository, SystemUserRepository systemUserRepository,
			IConfiguration configuration)
		{
			_inlineKeysRepository = inlineKeysRepository;
			_configuration = configuration;
			_botsRepository = botsRepository;
			_systemUserRepository = systemUserRepository;
		}

		[Route("/inline-keys/new")]
		[HttpGet]
		public async Task<IActionResult> NewInlineKey(string botId)
		{
			var userId = HttpContext.Session.GetString("userId");
			var bots = await BotsService.GetBotsViewModels(_configuration, _systemUserRepository,_botsRepository, userId);
			var bot = await BotsService.GetBotViewModel(botId, _configuration, _botsRepository);

			return View(new PageViewModel
			{
				CurrentBot = bot,
				Bots = bots
			});
		}

		[Route("/inline-keys/add")]
		[HttpPost]
		public IActionResult AddInlineKey(string caption, string answer, string botId)
		{
			_inlineKeysRepository.AddInlineKey(new InlineKey
			{
				Caption = caption,
				Answer = answer,
				BotId = botId
			});

			return Redirect("/bot?id=" + botId);
		}

		[Route("/inline-keys/remove")]
		[HttpPost]
		public IActionResult RemoveInlineKey(string id, string botId)
		{
			_inlineKeysRepository.RemoveInlineKey(id);

			return Redirect("/bot?id=" + botId);
		}
	}
}