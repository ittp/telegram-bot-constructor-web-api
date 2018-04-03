using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class InlineUrlKey
	{
		[BsonId]
		public ObjectId Id { get; set; }

		[BsonElement("caption")]
		public string Caption { get; set; }

		[BsonElement("url")]
		public string Url { get; set; }

		[BsonElement("botId")]
		public string BotId { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				caption = Caption,
				url = Url,
				botId = BotId
			};
		}
	}
}