using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    public class InlineKeysController : Controller
    {
        private readonly InlineKeysRepository _inlineKeysRepository;
        private readonly IConfiguration _configuration;
        private readonly BotsRepository _botsRepository;

        public InlineKeysController(InlineKeysRepository inlineKeysRepository, BotsRepository botsRepository, IConfiguration configuration)
        {
            _inlineKeysRepository = inlineKeysRepository;
            _configuration = configuration;
            _botsRepository = botsRepository;
        }

        [Route("/inline-keys/new")]
        [HttpPost]
        public async Task<IActionResult> NewInlineKey(string caption, string answer, string botId)
        {
            var bots = await BotsService.GetBotsViewModels(_configuration, _botsRepository);
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
            var inlineKeyDto = _inlineKeysRepository.AddInlineKey(new InlineKey
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
            var result = _inlineKeysRepository.RemoveInlineKey(id);

            return Redirect("/bot?id=" + botId);
        }
    }
}