using System.Linq;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace Api.Controllers
{
	public class DataController: Controller
	{
		private Repository _repository;

		public DataController(Repository repository)
		{
			_repository = repository;
		}

		[Route("/users")]
		[HttpGet]
		public JsonResult GetUsers()
		{
			var mappedUsers = _repository.GetUsers().Select(_=> new
			{
				id = _.Id.ToString(),
				telegramId = _.TelegramId,
				firstName = _.FirstName,
				lastName = _.LastName
			});

			return Json(mappedUsers);
		}

		[Route("/get-user")]
		[HttpGet]
		public JsonResult GetUsers(string id)
		{
			var user = _repository.GetUserById(new ObjectId(id));

			var mappedUser = new
			{
				id = user.Id.ToString(),
				telegramId = user.TelegramId,
				firstName = user.FirstName,
				lastName = user.LastName
			};

			return Json(mappedUser);
		}

		[Route("/add-user")]
		[HttpPost]
		public string AddUser(string telegramId, string firstName, string lastName)
		{
			_repository.AddUser(new User
			{
				TelegramId = telegramId,
				FirstName = firstName,
				LastName = lastName
			});

			return "ok";
		}
	}
}