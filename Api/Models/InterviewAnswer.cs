using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class InterviewAnswer
	{
		[BsonId]
		public ObjectId Id { get; set; }

		[BsonElement("interviewId")]
		public string InterviewId { get; set; }

		[BsonElement("userId")]
		public string UserId { get; set; }

		[BsonElement("answer")]
		public string Answer { get; set; }

		[BsonElement("botId")]
		public string BotId { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				interviewId = InterviewId,
				userId = UserId,
				answer = Answer,
				botId = BotId
			};
		}
	}
}