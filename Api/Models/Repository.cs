using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Models
{
	public class Repository
	{
		private readonly IMongoCollection<User> _users;
		private readonly IMongoCollection<Bot> _bots;
		private readonly IMongoCollection<TextMessageAnswer> _textMessageAnswers;
		private readonly IMongoCollection<InlineKey> _inlineKeys;

		public Repository(string token, string dbName)
		{
			var database = new MongoClient(token).GetDatabase(dbName);
			_users = database.GetCollection<User>("users");
			_bots = database.GetCollection<Bot>("bots");
			_textMessageAnswers = database.GetCollection<TextMessageAnswer>("textMessageAnswers");
			_inlineKeys = database.GetCollection<InlineKey>("inlineKeys");
		}

		public Bot GetBotByToken(string token)
		{
			return _bots.Find(x => x.Token == token).FirstOrDefault();
		}

		public void AddBot(Bot bot)
		{
			_bots.InsertOne(bot);
		}

		public IEnumerable<TextMessageAnswer> GetTextMessageAnswers(string botId)
		{
			return _textMessageAnswers.Find(x => x.BotId == botId).ToList();
		}

		public void AddTextMessageAnswer(TextMessageAnswer textMessageAnswer)
		{
			_textMessageAnswers.InsertOne(textMessageAnswer);
		}

		public void AddUser(User user)
		{
			_users.InsertOne(user);
		}

		public IEnumerable<User> GetUsers(string botId)
		{
			return _users.Find(x => x.BotId == botId).ToList();
		}

		public void AddInlineKey(InlineKey inlineKey)
		{
			_inlineKeys.InsertOne(inlineKey);
		}

		public IEnumerable<InlineKey> GetInlineKeys(string botId)
		{
			return _inlineKeys.Find(x => x.BotId == botId).ToList();
		}
	}
}