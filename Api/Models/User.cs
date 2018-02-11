using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class User
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		public string TelegramId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
}