using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
	public class InlineKeysController: Controller
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

		[Route("/inline-key")]
		[HttpGet]
		public async Task<IActionResult> InlineKey(string id)
		{
			var inlineKeyDto = _inlineKeysRepository.GetInlineKey(id);
			var bots = await BotsService.GetBotsViewModels(_configuration, _botsRepository);

			return View(new PageViewModel{
				CurrentInlineKey = inlineKeyDto,
				Bots = bots
			});
		}

		// [Route("/inline-keys/add")]
		// [HttpPost]
		// public JsonResult AddInlineKey(string caption, string answer, string botId)
		// {
		// 	if (string.IsNullOrEmpty(caption)) return Json(false);
		// 	if (string.IsNullOrEmpty(answer)) return Json(false);
		// 	if (string.IsNullOrEmpty(botId)) return Json(false);

		// 	var inlineKeyDto = _inlineKeysRepository.AddInlineKey(new InlineKey
		// 	{
		// 		Caption = caption,
		// 		Answer = answer,
		// 		BotId = botId
		// 	});

		// 	return inlineKeyDto != null
		// 		? Json(inlineKeyDto.Transform())
		// 		: Json(false);
		// }}

		// [Route("/inline-keys/remove")]
		// [HttpPost]
		// public JsonResult RemoveInlineKey(string id)
		// {
		// 	if (string.IsNullOrEmpty(id)) return Json(false);

		// 	var result = _inlineKeysRepository.RemoveInlineKey(id);

		// 	return result ? Json(true) : Json(false);
		// }
	}
}