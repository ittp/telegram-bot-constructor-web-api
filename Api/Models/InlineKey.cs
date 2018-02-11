using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class InlineKey
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		public string Caption { get; set; }
		public string Answer { get; set; }
		public string BotToken { get; set; }
	}
}