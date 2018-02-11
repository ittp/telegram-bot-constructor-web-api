using Api.Models;
using Microsoft.AspNetCore.Mvc;

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
			var users = _repository.GetUsers();

			return Json(users);
		}
	}
}