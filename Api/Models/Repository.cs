using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Models
{
	public class Repository
	{
		private readonly IMongoCollection<User> _usersCollection;
		private readonly IMongoCollection<Bot> _botsCollection;
		private readonly IMongoCollection<TextMessageAnswer> _textMessageAnswers;

		public Repository(string token, string dbName)
		{
			var database = new MongoClient(token).GetDatabase(dbName);
			_usersCollection = database.GetCollection<User>("users");
			_botsCollection = database.GetCollection<Bot>("bots");
			_textMessageAnswers = database.GetCollection<TextMessageAnswer>("textMessageAnswers");
		}

		public Bot GetBotByToken(string token) => _botsCollection.Find(x => x.Token == token).FirstOrDefault();

		public void AddBot(Bot bot) => _botsCollection.InsertOne(bot);

		public IEnumerable<TextMessageAnswer> GetTextMessageAnswers(string botId) =>
			_textMessageAnswers.Find(x => x.BotId == botId).ToList();

		public void AddTextMessageAnswer(TextMessageAnswer textMessageAnswer) =>
			_textMessageAnswers.InsertOne(textMessageAnswer);

		public void AddUser(User user) => _usersCollection.InsertOne(user);

		public IEnumerable<User> GetUsers(string botId) => _usersCollection.Find(x => x.BotId == botId).ToList();
	}
}