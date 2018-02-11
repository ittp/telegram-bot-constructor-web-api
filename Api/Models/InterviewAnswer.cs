using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class InterviewAnswer
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		public ObjectId InterViewId { get; set; }
		public ObjectId UserId { get; set; }
		public string Answer { get; set; }
		public string BotToken { get; set; }
	}
}