using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class InlineKeysApiController: Controller
    {
        private readonly InlineKeysRepository _inlineKeysRepository;

        public InlineKeysApiController(InlineKeysRepository inlineKeysRepository)
        {
            _inlineKeysRepository = inlineKeysRepository;
        }

        [Route("/api/inline-keys/add")]
        [HttpPost]
        public JsonResult AddInlineKey(string caption, string answer, string botId)
        {
            if (string.IsNullOrEmpty(caption)) return Json(false);
            if (string.IsNullOrEmpty(answer)) return Json(false);
            if (string.IsNullOrEmpty(botId)) return Json(false);

            var inlineKeyDto = _inlineKeysRepository.AddInlineKey(new InlineKey
            {
                Caption = caption,
                Answer = answer,
                BotId = botId
            });

            return inlineKeyDto != null
                ? Json(inlineKeyDto.Transform())
                : Json(false);
        }

        [Route("/api/inline-keys")]
        [HttpGet]
        public JsonResult GetInlineKeys(string botId)
        {
            if (string.IsNullOrEmpty(botId)) return Json(false);

            var inlineKeysDto = _inlineKeysRepository.GetInlineKeys(botId);

            return inlineKeysDto != null
                ? Json(inlineKeysDto.Select(x => x.Transform()))
                : Json(false);
        }

        [Route("/api/inline-keys/inline-key")]
        [HttpGet]
        public JsonResult GetInlineKey(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json(false);

            var inlineKeyDto = _inlineKeysRepository.GetInlineKey(id);

            return inlineKeyDto != null
                ? Json(inlineKeyDto.Transform())
                : Json(false);
        }

        [Route("/api/inline-keys/remove")]
        [HttpPost]
        public JsonResult RemoveInlineKey(string id)
        {
            if (string.IsNullOrEmpty(id)) return Json(false);

            var result = _inlineKeysRepository.RemoveInlineKey(id);

            return result ? Json(true) : Json(false);
        }
    }
}