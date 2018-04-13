using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Api.Services
{
	public class BotsService
	{
		public async static Task<IEnumerable<BotViewModel>> GetBotsViewModels(IConfiguration _configuration,
			SystemUserRepository _botsRepository, string userId)
		{
            var httpClient = new HttpClient();
            
			var bots = _botsRepository.GetUserBots(userId) ?? new List<Bot>();

            var botsViewModels = await Task.WhenAll(bots.Select(async _ =>
			{
				var result = await httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/check?id={_.Id}");
				var parsedResult = JsonConvert.DeserializeObject<Response>(result);
				return new BotViewModel
				{
					Bot = _,
					Status = parsedResult.status
				};
			}));

            return botsViewModels;
		}

        public async static Task<BotViewModel> GetBotViewModel(string id, IConfiguration _configuration, BotsRepository _botsRepository)
		{
            var httpClient = new HttpClient();
            
			var bot = _botsRepository.GetBot(id);

			var botResult = await httpClient.GetStringAsync($"{_configuration["RunnerApiUrl"]}/check?id={id}");
			var botParsedResult = JsonConvert.DeserializeObject<Response>(botResult);

			var botViewModel = new BotViewModel
			{
				Bot = bot,
				Status = Convert.ToBoolean(botParsedResult.status)
			};

            return botViewModel;
		}
	}
}