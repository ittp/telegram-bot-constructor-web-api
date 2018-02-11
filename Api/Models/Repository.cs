using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Models
{
	public class Repository
	{
		private readonly IMongoCollection<User> _usersCollection;

		public Repository(string token, string dbName)
		{
			var database = new MongoClient(token).GetDatabase(dbName);
			_usersCollection = database.GetCollection<User>("users");
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
	}
}