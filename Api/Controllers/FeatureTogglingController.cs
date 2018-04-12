using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Api.Controllers
{
	public class FeatureTogglingController : Controller
	{
		private readonly BotsRepository _botsRepository;

		public FeatureTogglingController(BotsRepository botsRepository)
		{
			_botsRepository = botsRepository;
		}


		[Route("/networking")]
		public async Task<RedirectResult> SetNetWorking(string id, bool status)
		{
			_botsRepository.SetNetWorkingStatus(id, status);

			return Redirect($"/bot?id={id}");
		}

		[Route("/congnitiveservices")]
		public async Task<RedirectResult> SetCongnitiveServices(string id, bool status)
		{
			_botsRepository.SetCognitiveServicesStatus(id, status);

			return Redirect($"/bot?id={id}");
		}

	}
}