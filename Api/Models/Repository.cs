﻿using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Models
{
	public class Repository
	{
		private readonly IMongoCollection<User> _usersCollection;
		private IMongoCollection<Bot> _botsCollection;
		private IMongoCollection<TextMessageAnswer> _textMessageAnswers;

		public Repository(string token, string dbName)
		{
			var database = new MongoClient(token).GetDatabase(dbName);
			_usersCollection = database.GetCollection<User>("users");
			_botsCollection = database.GetCollection<Bot>("bots");
			_textMessageAnswers = database.GetCollection<TextMessageAnswer>("textMessageAnswers");
		}

		public void AddUser(User user)
		{
			_usersCollection.InsertOne(user);
		}

		public IEnumerable<User> GetUsers()
		{
			return _usersCollection.Find(new BsonDocument()).ToList();
		}

		public User GetUserById(ObjectId id)
		{
			return _usersCollection.Find(x => x.Id == id).FirstOrDefault();
		}

		public Bot GetBotByToken(string token)
		{
			return _botsCollection.Find(x => x.Token == token).FirstOrDefault();
		}

		public IEnumerable<TextMessageAnswer> GetTextMessageAnswers(string botId)
		{
			return _textMessageAnswers.Find(x => x.BotId == botId).ToList();
		}
	}
}