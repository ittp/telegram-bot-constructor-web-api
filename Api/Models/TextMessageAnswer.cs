using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class TextMessageAnswer
	{
		[BsonId]
		public ObjectId Id { get; set; }

		public string Message { get; set; }

		public string Answer { get; set; }

		public string BotId { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				message = Message,
				answer = Answer,
				botId = BotId
			};
		}
	}
}