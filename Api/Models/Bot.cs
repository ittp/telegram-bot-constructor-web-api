using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class Bot
	{
		[BsonId]
		public ObjectId Id { get; set; }

		public string Token { get; set; }

		public string Name { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				token = Token,
				name = Name
			};
		}
	}
}