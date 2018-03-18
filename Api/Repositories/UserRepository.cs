using System;
using Api.Models;
using MongoDB.Driver;

namespace Api.Repositories
{
    public class UserRepository
    {
        private readonly IMongoCollection<User> _users;
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database){
            _database = database;
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
