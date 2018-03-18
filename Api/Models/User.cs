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

		public string UserName { get; set; }

		public string BotId { get; set; }

        public string Networking { get; set; }

		public object Transform()
		{
			return new
			{
				id = Id.ToString(),
				telegramId = TelegramId,
				firstName = FirstName,
				lastName = LastName,
				userName = UserName,
				botId = BotId,
                networking = Networking,
			};
		}
	}
}