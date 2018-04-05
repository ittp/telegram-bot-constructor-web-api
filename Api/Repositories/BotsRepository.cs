using System.Collections.Generic;
using Api.Models;
using Api.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class BotsRepository
	{
		private readonly IMongoCollection<Bot> _bots;

		public BotsRepository(IMongoDatabase database)
		{
			_bots = database.GetCollection<Bot>("bots");
		}

		public Bot GetBotByToken(string token)
		{
			return _bots.Find(x => x.Token == token).FirstOrDefault();
		}

		public string GetStartMessage(string id)
		{
			return _bots.Find(x => x.Id == new ObjectId(id)).FirstOrDefault().StartMessage;
		}

		public Bot AddBot(Bot bot)
		{
			_bots.InsertOne(bot);

			return GetBot(bot.Id.ToString());
		}

		public IEnumerable<Bot> GetBots()
		{
			return _bots.Find(new BsonDocument()).ToList();
		}

		public Bot GetBot(string id)
		{
			return _bots.Find(x => x.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
		}

		public Bot SetNetWorkingStatus(string botId, bool networkingStatus)
		{
			var update = Builders<Bot>.Update.Set(x => x.NetworkingEnabled, networkingStatus);

			_bots.UpdateOne(x => x.Id == new ObjectId(botId), update);

			return _bots.Find(x => x.Id == new ObjectId(botId)).FirstOrDefault();
		}

		public Bot SetCognitiveServicesStatus(string botId, bool cognitiveServicesStatus)
		{
			var update = Builders<Bot>.Update.Set(x => x.CognitiveServicesEnabled, cognitiveServicesStatus);

			_bots.UpdateOne(x => x.Id == new ObjectId(botId), update);

			return _bots.Find(x => x.Id == new ObjectId(botId)).FirstOrDefault();
		}

		public Bot SetStartMessage(string botId, string startMessage)
		{
			var update = Builders<Bot>.Update.Set(x => x.StartMessage, startMessage);

			_bots.UpdateOne(x => x.Id == new ObjectId(botId), update);

			return _bots.Find(x => x.Id == new ObjectId(botId)).FirstOrDefault();
		}

		public bool RemoveBot(string id)
		{
			var deleteResult = _bots.DeleteOne(x => x.Id == MongoService.TryCreateObjectId(id));

			return deleteResult.DeletedCount > 0;
		}
	}
}