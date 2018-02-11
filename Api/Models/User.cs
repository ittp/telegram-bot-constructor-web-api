using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	[BsonIgnoreExtraElements]
	public class User
	{
		[BsonId]
		public ObjectId Id { get; set; }

		public string TelegramId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}