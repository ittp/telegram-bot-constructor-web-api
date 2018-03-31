using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Models
{
    public class Repository
    {
        private readonly IMongoCollection<Bot> _bots;
        private readonly IMongoCollection<InlineKey> _inlineKeys;
        private readonly IMongoCollection<InterviewAnswer> _interviewAnswers;
        private readonly IMongoCollection<Event> _events;
        private readonly IMongoCollection<Interview> _interviews;
        private readonly IMongoCollection<TextMessageAnswer> _textMessageAnswers;
        private readonly IMongoCollection<User> _users;

        public Repository(string token, string dbName)
        {
            var database = new MongoClient(token).GetDatabase(dbName);
            _users = database.GetCollection<User>("users");
            _bots = database.GetCollection<Bot>("bots");
            _textMessageAnswers = database.GetCollection<TextMessageAnswer>("textMessageAnswers");
            _inlineKeys = database.GetCollection<InlineKey>("inlineKeys");
            _interviews = database.GetCollection<Interview>("interviews");
            _interviewAnswers = database.GetCollection<InterviewAnswer>("interviewsAnswers");
            _events = database.GetCollection<Event>("events");
        }

        public IEnumerable<Event> GetEvents(string botId)
        {
            return _events.Find(x => x.BotId == botId).ToEnumerable();
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

        public Event AddEvent(Event e)
        {
            _events.InsertOne(e);

            return GetEvent(e.Id.ToString());
        }

        public bool RemoveEvent(string id)
        {
            var result = _events.DeleteOne(x => x.Id == new ObjectId(id));

			return result.DeletedCount > 0;
        }

        public Event GetEvent(string id)
        {
            return _events.Find(x => x.Id == TryCreateObjectId(id)).FirstOrDefault();
        }

        public IEnumerable<Bot> GetBots()
        {
            return _bots.Find(new BsonDocument()).ToList();
        }

        public Bot GetBot(string id)
        {
            return _bots.Find(x => x.Id == TryCreateObjectId(id)).FirstOrDefault();
        }



        public IEnumerable<TextMessageAnswer> GetTextMessageAnswers(string botId)
        {
            return _textMessageAnswers.Find(x => x.BotId == botId).ToList();
        }

        public TextMessageAnswer AddTextMessageAnswer(TextMessageAnswer textMessageAnswer)
        {
            _textMessageAnswers.InsertOne(textMessageAnswer);

            return GetTextMessageAnswer(textMessageAnswer.Id.ToString());
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

        public InlineKey AddInlineKey(InlineKey inlineKey)
        {
            _inlineKeys.InsertOne(inlineKey);

            return GetInlineKey(inlineKey.Id.ToString());
        }

        public IEnumerable<InlineKey> GetInlineKeys(string botId)
        {
            return _inlineKeys.Find(x => x.BotId == botId).ToList();
        }

        public InlineKey GetInlineKey(string id)
        {
            return _inlineKeys.Find(x => x.Id == TryCreateObjectId(id)).FirstOrDefault();
        }

        public Interview AddInterview(Interview interview)
        {
            _interviews.InsertOne(interview);

            return GetInterview(interview.Id.ToString());
        }

        public IEnumerable<Interview> GetInterviews(string botId)
        {
            return _interviews.Find(x => x.BotId == botId).ToList();
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

        public Bot SetNetWorkingStatus(string botId, bool networkingStatus)
        {
            var update = Builders<Bot>.Update.Set(x => x.NetworkingEnabled, networkingStatus);

            _bots.UpdateOne(x => x.Id == new ObjectId(botId), update);

            return _bots.Find(x => x.Id == new ObjectId(botId)).FirstOrDefault();
        }
        
        public Bot SetStartMessage(string botId, string startMessage)
        {
            var update = Builders<Bot>.Update.Set(x => x.StartMessage, startMessage);

            _bots.UpdateOne(x => x.Id == new ObjectId(botId), update);

            return _bots.Find(x => x.Id == new ObjectId(botId)).FirstOrDefault();
        }

        public InterviewAnswer AddInterviewAnswer(InterviewAnswer interviewAnswer)
        {
            _interviewAnswers.InsertOne(interviewAnswer);

            return GetInterviewAnswer(interviewAnswer.Id.ToString());
        }

        public IEnumerable<InterviewAnswer> GetInterviewAnswers(string botId)
        {
            return _interviewAnswers.Find(x => x.BotId == botId).ToList();
        }

        public bool RemoveBot(string id)
        {
            var deleteResult = _bots.DeleteOne(x => x.Id == TryCreateObjectId(id));

            return deleteResult.DeletedCount > 0;
        }

        public bool RemoveInlineKey(string id)
        {
            var deleteResult = _inlineKeys.DeleteOne(x => x.Id == TryCreateObjectId(id));

            return deleteResult.DeletedCount > 0;
        }

        public bool RemoveInterview(string id)
        {
            var deleteResult = _interviews.DeleteOne(x => x.Id == TryCreateObjectId(id));

            return deleteResult.DeletedCount > 0;
        }

        public bool RemoveInterviewAnswer(string id)
        {
            var deleteResult = _interviewAnswers.DeleteOne(x => x.Id == TryCreateObjectId(id));

            return deleteResult.DeletedCount > 0;
        }

        public bool RemoveTextMessageAnswer(string id)
        {
            var deleteResult = _textMessageAnswers.DeleteOne(x => x.Id == TryCreateObjectId(id));

            return deleteResult.DeletedCount > 0;
        }

        public bool RemoveUser(string id)
        {
            var deleteResult = _users.DeleteOne(x => x.Id == TryCreateObjectId(id));

            return deleteResult.DeletedCount > 0;
        }

        public Interview GetInterview(string id)
        {
            return _interviews.Find(x => x.Id == TryCreateObjectId(id)).FirstOrDefault();
        }

        public InterviewAnswer GetInterviewAnswer(string id)
        {
            return _interviewAnswers.Find(x => x.Id == TryCreateObjectId(id)).FirstOrDefault();
        }

        public TextMessageAnswer GetTextMessageAnswer(string id)
        {
            return _textMessageAnswers.Find(x => x.Id == TryCreateObjectId(id)).FirstOrDefault();
        }

        private static ObjectId? TryCreateObjectId(string id)
        {
            try
            {
                return new ObjectId(id);
            }
            catch
            {
                return null;
            }
        }
    }
}