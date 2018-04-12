using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class UserEvent
	{
		[BsonId]
		public ObjectId Id { get; set; }

		[BsonElement("text")]
		public string Text { get; set; }

		[BsonElement("botId")]
		public string BotId { get; set; }

		[BsonElement("userTelegramId")]
		public string UserTelegramId { get; set; }


		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				text = Text,
				botId = BotId,
				userTelegramId = UserTelegramId
			};
		}
	}
}