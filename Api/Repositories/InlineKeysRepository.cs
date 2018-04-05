using System.Collections.Generic;
using Api.Models;
using Api.Services;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class InlineKeysRepository
	{
		private readonly IMongoCollection<InlineKey> _inlineKeys;

		public InlineKeysRepository(IMongoDatabase database)
		{
			_inlineKeys = database.GetCollection<InlineKey>("inlineKeys");
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
			return _inlineKeys.Find(x => x.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
		}

		public bool RemoveInlineKey(string id)
		{
			var deleteResult = _inlineKeys.DeleteOne(x => x.Id == MongoService.TryCreateObjectId(id));

			return deleteResult.DeletedCount > 0;
		}
	}
}