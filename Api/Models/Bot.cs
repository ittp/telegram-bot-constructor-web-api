using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class Bot
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		public string Token { get; set; }
		public string Name { get; set; }
	}
}