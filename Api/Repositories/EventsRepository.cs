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

		public EventsRepository(IMongoDatabase database)
		{
			_events = database.GetCollection<Event>("events");
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
	}
}