using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class InlineKey
    {
        [BsonId] public ObjectId Id { get; set; }

        [BsonElement("caption")] public string Caption { get; set; }

        [BsonElement("answer")] public string Answer { get; set; }

        [BsonElement("botId")] public string BotId { get; set; }

        public object Transform()
        {
            return new
            {
                id = Id.ToString(),
                caption = Caption,
                answer = Answer,
                botId = BotId
            };
        }
    }
}