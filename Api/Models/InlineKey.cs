using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class InlineKey
	{
		[BsonId]
		public ObjectId Id { get; set; }

		public string Caption { get; set; }

		public string Answer { get; set; }

		public string BotId { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				caption = Caption,
				answer = Answer,
				botId = BotId
			};
		}
	}
}