using System.Collections.Generic;
using Api.Models;
using Api.Services;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Api.Repositories
{
    public class SystemUserRepository
    {
        private readonly IMongoCollection<SystemUser> _users;

        public SystemUserRepository(IMongoDatabase database)
        {
            _users = database.GetCollection<SystemUser>("systemUsers");
        }

        public SystemUser Add(SystemUser user)
        {
            var usersFind = _users.Find(_ => _.Login == user.Login);

            if (usersFind.Count() > 0)
            {
                return null;
            }

            _users.InsertOne(user);
            
            return GetUser(user.Id.ToString());
        }

        public SystemUser GetUser(string id)
        {
            return _users.Find(_ => _.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
        }
        
        public IEnumerable<string> GetUserBots(string id)
        {
            return _users.Find(_ => _.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault().Bots;
        }

        public SystemUser GetUserByLogin(string login)
        {
            return _users.Find(_ => _.Login == login).FirstOrDefault();
        }

        public void AddBotToUser(string userId, string botId)
        {
            var user = _users.Find(_ => _.Id == MongoService.TryCreateObjectId(userId)).FirstOrDefault();
            if (user.Bots == null) user.Bots = new List<string>();
            user.Bots.Add(botId);
            
            var builder = Builders<SystemUser>.Update.Set(_=>_.Bots, user.Bots);

            _users.UpdateOne(_ => _.Id == MongoService.TryCreateObjectId(userId), builder);        
        }
    }


    public class SystemUser
    {
        [BsonId] 
        public ObjectId Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<string> Bots { get; set; }

        public object Transform()
        {
            return new
            {
                id = Id.ToString(),
                login = Login,
                assword = Password,
                bot = Bots
            };
        }
    }
}