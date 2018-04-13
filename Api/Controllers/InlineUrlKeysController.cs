using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [AuthenticationAttribute]
    public class InlineUrlKeysController: Controller
    {
        private readonly InlineUrlKeysRepository _inlineUrlKeysRepository;
        private readonly IConfiguration _configuration;
        private readonly BotsRepository _botsRepository;

        public InlineUrlKeysController(InlineUrlKeysRepository inlineUrlKeysRepository, BotsRepository botsRepository, IConfiguration configuration)
        {
            _inlineUrlKeysRepository = inlineUrlKeysRepository;
            _configuration = configuration;
            _botsRepository = botsRepository;
        }

        [Route("/inline-url-keys/new")]
        [HttpGet]
        public async Task<IActionResult> NewInlineUrlKey(string botId)
        {
            var bots = await BotsService.GetBotsViewModels(_configuration, _botsRepository);
            var bot = await BotsService.GetBotViewModel(botId, _configuration, _botsRepository);

            return View(new PageViewModel
            {
                CurrentBot = bot,
                Bots = bots
            });
        }

        [Route("/inline-url-keys/add")]
        [HttpPost]
        public IActionResult AddInlineUrlKey(string caption, string url, string botId)
        {
            _inlineUrlKeysRepository.AddInlineUrlKey(new InlineUrlKey
            {
                Caption = caption,
                Url = url,
                BotId = botId
            });

            return Redirect("/bot?id=" + botId);
        }

        [Route("/inline-url-keys/remove")]
        [HttpPost]
        public IActionResult RemoveUrlInlineKey(string id, string botId)
        {
            _inlineUrlKeysRepository.RemoveInlineUrlKey(id);

            return Redirect("/bot?id=" + botId);
        }
    }
}