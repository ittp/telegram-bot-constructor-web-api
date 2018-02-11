using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class Interview
	{
		[BsonId]
		public ObjectId Id { get; set; }

		public string Name { get; set; }

		public string Question { get; set; }

		public List<string> Answers { get; set; }

		public string BotId { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				name = Name,
				question = Question,
				answers = Answers,
				botId = BotId
			};
		}
	}
}