using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class Interview
    {
        [BsonId] public ObjectId Id { get; set; }

        [BsonElement("name")] public string Name { get; set; }

        [BsonElement("question")] public string Question { get; set; }

        [BsonElement("answers")] public List<string> Answers { get; set; }

        [BsonElement("botId")] public string BotId { get; set; }

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