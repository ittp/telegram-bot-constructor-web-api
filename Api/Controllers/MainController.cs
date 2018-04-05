using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
	public class MainController : Controller
	{
		private readonly IConfiguration _configuration;

		public MainController(IConfiguration configuration, UsersRepository usersRepository,
			TextMessageAnswersRepository textMessageAnswersRepository, InterviewsRepository interviewsRepository,
			InterviewAnswersRepository interviewAnswersRepository, InlineUrlKeysRepository inlineUrlKeysRepository,
			InlineKeysRepository inlineKeysRepository)
		{
			_configuration = configuration;
		}

		[Route("/")]
		[HttpGet]
		public string Index()
		{
			return _configuration["Version"];
		}
	}
}