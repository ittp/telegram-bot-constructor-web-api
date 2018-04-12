using System.Collections.Generic;
using Api.Models;
using Api.Services;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class EventsRepository
	{
		private readonly IMongoCollection<Event> _events;
		private readonly IMongoCollection<UserEvent> _userEvents;

		public EventsRepository(IMongoDatabase database)
		{
			_events = database.GetCollection<Event>("events");
			_userEvents = database.GetCollection<UserEvent>("userEvents");
		}

		public IEnumerable<Event> GetEvents(string botId)
		{
			return _events.Find(x => x.BotId == botId).ToEnumerable();
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
			return _events.Find(x => x.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
		}

		public UserEvent AddUserEvent(UserEvent userEvent)
		{
			_userEvents.InsertOne(userEvent);

			return GetUserEvent(userEvent.Id.ToString());
		}

		public bool RemoveUserEvent(string id)
		{
			var result = _userEvents.DeleteOne(x => x.Id == new ObjectId(id));

			return result.DeletedCount > 0;
		}

		public UserEvent GetUserEvent(string id)
		{
			return _userEvents.Find(x => x.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
		}

		public IEnumerable<UserEvent> GetUserEvents(string botId)
		{
			return _userEvents.Find(x => x.BotId == botId).ToList();
		}
	}
}