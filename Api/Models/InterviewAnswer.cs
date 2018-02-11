using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
	public class InterviewAnswer
	{
		[BsonId]
		public ObjectId Id { get; set; }

		public string InterViewId { get; set; }

		public string UserId { get; set; }

		public string Answer { get; set; }

		public string BotId { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				interviewId = InterViewId,
				userId = UserId,
				answer = Answer,
				botId = BotId
			};
		}
	}
}