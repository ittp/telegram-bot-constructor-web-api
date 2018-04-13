using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [AuthenticationAttribute]
    public class TextMessageAnswersController : Controller
    {
        private readonly TextMessageAnswersRepository _textMessageAnswersRepository;
        private readonly IConfiguration _configuration;
        private readonly BotsRepository _botsRepository;

        public TextMessageAnswersController(TextMessageAnswersRepository textMessageAnswersRepository, BotsRepository botsRepository, IConfiguration configuration)
        {
            _textMessageAnswersRepository = textMessageAnswersRepository;
            _configuration = configuration;
            _botsRepository = botsRepository;
        }

        [Route("/text-message-answers/new")]
        [HttpGet]
        public async Task<IActionResult> NewInlineKey(string botId)
        {
            var bots = await BotsService.GetBotsViewModels(_configuration, _botsRepository);
            var bot = await BotsService.GetBotViewModel(botId, _configuration, _botsRepository);

            return View(new PageViewModel
            {
                CurrentBot = bot,
                Bots = bots
            });
        }

        [Route("/text-message-answers/add")]
        [HttpPost]
        public IActionResult AddInlineKey(string message, string answer, string botId)
        {
            _textMessageAnswersRepository.AddTextMessageAnswer(new TextMessageAnswer
            {
                Message = message,
                Answer = answer,
                BotId = botId
            });

            return Redirect("/bot?id=" + botId);
        }

        [Route("/text-message-answers/remove")]
        [HttpPost]
        public IActionResult RemoveInlineKey(string id, string botId)
        {
            _textMessageAnswersRepository.RemoveTextMessageAnswer(id);

            return Redirect("/bot?id=" + botId);
        }
    }
}