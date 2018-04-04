using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models
{
    public class Bot
    {
        [BsonId] public ObjectId Id { get; set; }

        [BsonElement("token")] public string Token { get; set; }

        [BsonElement("name")] public string Name { get; set; }

        public bool NetworkingEnabled { get; set; }

        public bool CognitiveServicesEnabled { get; set; }

        public string StartMessage { get; set; }

        public object Transform()
        {
            return new
            {
                id = Id.ToString(),
                token = Token,
                name = Name,
                networkingEnabled = NetworkingEnabled,
                cognitiveServicesEnabled = CognitiveServicesEnabled,
                startMessage = StartMessage
            };
        }
    }
}