using System.Collections.Generic;
using Api.Models;
using Api.Services;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class TextMessageAnswersRepository
	{
		private readonly IMongoCollection<TextMessageAnswer> _textMessageAnswers;

		public TextMessageAnswersRepository(IMongoDatabase database)
		{
			_textMessageAnswers = database.GetCollection<TextMessageAnswer>("textMessageAnswers");
		}

		public TextMessageAnswer GetTextMessageAnswer(string id)
		{
			return _textMessageAnswers.Find(x => x.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
		}

		public IEnumerable<TextMessageAnswer> GetTextMessageAnswers(string botId)
		{
			return _textMessageAnswers.Find(x => x.BotId == botId).ToList();
		}

		public TextMessageAnswer AddTextMessageAnswer(TextMessageAnswer textMessageAnswer)
		{
			_textMessageAnswers.InsertOne(textMessageAnswer);

			return GetTextMessageAnswer(textMessageAnswer.Id.ToString());
		}

		public bool RemoveTextMessageAnswer(string id)
		{
			var deleteResult = _textMessageAnswers.DeleteOne(x => x.Id == MongoService.TryCreateObjectId(id));

			return deleteResult.DeletedCount > 0;
		}
	}
}