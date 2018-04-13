using Api.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
	public class AuthController : Controller
	{
		private readonly IConfiguration _configuration;
		private readonly SystemUserRepository _systemUserRepository;

		public AuthController(IConfiguration configuration, SystemUserRepository systemUserRepository)
		{
			_configuration = configuration;
			_systemUserRepository = systemUserRepository;
		}

		[Route("/signup")]
		[HttpPost]
		public RedirectResult SignUp(string login, string password)
		{
			var systemUser = _systemUserRepository.Add(new SystemUser
			{
				Login = login,
				Password = password
			});

			if (systemUser == null) return Redirect("/signup");
			
			HttpContext.Session.SetString("userId", systemUser.Id.ToString());
				
			return Redirect("/bots");

		}
		
		[Route("/signup")]
		[HttpGet]
		public IActionResult SignUp()
		{
			return View();
		}
		
		[Route("/signin")]
		[HttpPost]
		public IActionResult SignIn(string login, string password)
		{
			var systemUser = _systemUserRepository.GetUserByLogin(login);
			if (systemUser == null) return Redirect("/signin");
			if (systemUser.Password != password) return Redirect("/signin");
			
			HttpContext.Session.SetString("userId", systemUser.Id.ToString());

			return Redirect("/bots");
		}
		
		[Route("/signin")]
		[HttpGet]
		public IActionResult SignIn()
		{
			return View();
		}
	}
}