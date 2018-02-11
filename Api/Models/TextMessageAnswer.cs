using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class TextMessageAnswer
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		public string Message { get; set; }
		public string Answer { get; set; }
		public string BotToken { get; set; }
	}
}