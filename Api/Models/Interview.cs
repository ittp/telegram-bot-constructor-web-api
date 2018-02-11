using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class Interview
	{
		[BsonElement("_id")]
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		public string Question { get; set; }
		public string BotToken { get; set; }
		public List<string> Answers { get; set; }
	}
}