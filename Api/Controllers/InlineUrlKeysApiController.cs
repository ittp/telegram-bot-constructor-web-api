using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	public class InlineUrlKeysApiController: Controller
	{
		private readonly InlineUrlKeysRepository _inlineUrlKeysRepository;

		public InlineUrlKeysApiController(InlineUrlKeysRepository inlineUrlKeysRepository)
		{
			_inlineUrlKeysRepository = inlineUrlKeysRepository;
		}

		[Route("/api/add-inline-url-key")]
		[HttpPost]
		public JsonResult AddUrlKey(string caption, string url, string botId)
		{
			if (string.IsNullOrEmpty(caption)) return Json(false);
			if (string.IsNullOrEmpty(url)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeyDto = _inlineUrlKeysRepository.AddInlineUrlKey(new InlineUrlKey
			{
				Caption = caption,
				Url = url,
				BotId = botId
			});

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}

		[Route("/api/inline-url-keys")]
		[HttpGet]
		public JsonResult GetInlineUrlKeys(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var inlineKeysDto = _inlineUrlKeysRepository.GetUrlInlineUrlKeys(botId);

			return inlineKeysDto != null
				? Json(inlineKeysDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/inline-url-key")]
		[HttpGet]
		public JsonResult GetInlineUrlKey(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var inlineKeyDto = _inlineUrlKeysRepository.GetUrlInlineUrlKey(id);

			return inlineKeyDto != null
				? Json(inlineKeyDto.Transform())
				: Json(false);
		}
	}
}