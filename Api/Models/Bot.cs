using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class Bot
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }

		[BsonElement("Token")]
		public string Token { get; set; }

		[BsonElement("Name")]
		public string Name { get; set; }
	}
}