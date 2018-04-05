using System.Collections.Generic;
using Api.Models;
using Api.Services;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class UsersRepository
	{
		private readonly IMongoCollection<User> _users;

		public UsersRepository(IMongoDatabase database)
		{
			_users = database.GetCollection<User>("users");
		}

		public bool RemoveUser(string id)
		{
			var deleteResult = _users.DeleteOne(x => x.Id == MongoService.TryCreateObjectId(id));

			return deleteResult.DeletedCount > 0;
		}

		public User GetUser(string telegramId, string botId)
		{
			return _users.Find(x => x.BotId == botId && x.TelegramId == telegramId).FirstOrDefault();
		}

		public User SetNetworking(string telegramId, string botId, string networking)
		{
			var update = Builders<User>.Update.Set(x => x.Networking, networking);

			_users.UpdateOne(x => x.BotId == botId && x.TelegramId == telegramId, update);

			return _users.Find(x => x.BotId == botId && x.TelegramId == telegramId).FirstOrDefault();
		}

		public User AddUser(User user)
		{
			_users.InsertOne(user);

			return GetUser(user.TelegramId, user.BotId);
		}

		public IEnumerable<User> GetUsers(string botId)
		{
			return _users.Find(x => x.BotId == botId).ToList();
		}

	}
}