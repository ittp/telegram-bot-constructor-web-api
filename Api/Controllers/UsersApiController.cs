using System.Linq;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
	public class UsersApiController: Controller
	{
		private readonly UsersRepository _usersRepository;

		public UsersApiController(UsersRepository usersRepository)
		{
			_usersRepository = usersRepository;
		}

		[Route("/api/add-user")]
		[HttpPost]
		public JsonResult AddUser(string telegramId, string firstName, string lastName, string userName, string botId)
		{
			if (string.IsNullOrEmpty(telegramId)) return Json(false);
			if (string.IsNullOrEmpty(firstName)) return Json(false);
			if (string.IsNullOrEmpty(lastName)) return Json(false);
			if (string.IsNullOrEmpty(userName)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var networking = JsonConvert.SerializeObject(new object());

			var userDto = _usersRepository.AddUser(new User
			{
				BotId = botId,
				FirstName = firstName,
				LastName = lastName,
				TelegramId = telegramId,
				UserName = userName,
				Networking = networking
			});

			return userDto != null
				? Json(userDto.Transform())
				: Json(false);
		}

		[Route("/api/users")]
		[HttpGet]
		public JsonResult GetUsers(string botId)
		{
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var usersDto = _usersRepository.GetUsers(botId);

			return usersDto != null
				? Json(usersDto.Select(x => x.Transform()))
				: Json(false);
		}

		[Route("/api/user")]
		[HttpGet]
		public JsonResult GetUser(string telegramId, string botId)
		{
			if (string.IsNullOrEmpty(telegramId)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);

			var userDto = _usersRepository.GetUser(telegramId, botId);

			return userDto != null
				? Json(userDto.Transform())
				: Json(false);
		}

		[Route("/api/set-networking")]
		[HttpPost]
		public JsonResult SetNetworking(string telegramId, string botId, string networking)
		{
			if (string.IsNullOrEmpty(telegramId)) return Json(false);
			if (string.IsNullOrEmpty(botId)) return Json(false);
			if (string.IsNullOrEmpty(networking)) return Json(false);

			var updatedUserDto = _usersRepository.SetNetworking(telegramId, botId, networking);

			return updatedUserDto != null
				? Json(updatedUserDto.Transform())
				: Json(false);
		}

		[Route("/api/remove-user")]
		[HttpPost]
		public JsonResult RemoveUser(string id)
		{
			if (string.IsNullOrEmpty(id)) return Json(false);

			var result = _usersRepository.RemoveUser(id);

			return result ? Json(true) : Json(false);
		}

	}
}