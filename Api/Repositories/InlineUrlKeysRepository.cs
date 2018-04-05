using System.Collections.Generic;
using Api.Models;
using Api.Services;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class InlineUrlKeysRepository
	{
		private readonly IMongoCollection<InlineUrlKey> _inlineUrlKeys;

		public InlineUrlKeysRepository(IMongoDatabase database)
		{
			_inlineUrlKeys = database.GetCollection<InlineUrlKey>("inlineUrlKeys");
		}

		public InlineUrlKey AddInlineUrlKey(InlineUrlKey inlineUrlKey)
		{
			_inlineUrlKeys.InsertOne(inlineUrlKey);

			return GetUrlInlineUrlKey(inlineUrlKey.Id.ToString());
		}

		public InlineUrlKey GetUrlInlineUrlKey(string botId)
		{
			return _inlineUrlKeys.Find(x => x.Id == MongoService.TryCreateObjectId(botId)).FirstOrDefault();
		}

		public IEnumerable<InlineUrlKey> GetUrlInlineUrlKeys(string botId)
		{
			return _inlineUrlKeys.Find(x => x.BotId == botId).ToList();
		}


	}
}