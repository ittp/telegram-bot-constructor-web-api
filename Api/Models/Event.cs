using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class Event
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("text")]
        public string Text { get; set; }

		[BsonElement("botId")]
		public string BotId { get; set; }

        public object Transform()
        {
            return new
            {
                id = Id.ToString(),
                text = Text,
                botId = BotId
            };
        }
    }
}