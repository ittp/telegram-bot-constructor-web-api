using System.Collections.Generic;
using Api.Models;
using Api.Services;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class InterviewAnswersRepository
	{
		private readonly IMongoCollection<InterviewAnswer> _interviewAnswers;

		public InterviewAnswersRepository(IMongoDatabase database)
		{
			_interviewAnswers = database.GetCollection<InterviewAnswer>("interviewsAnswers");
		}

		public InterviewAnswer AddInterviewAnswer(InterviewAnswer interviewAnswer)
		{
			_interviewAnswers.InsertOne(interviewAnswer);

			return GetInterviewAnswer(interviewAnswer.Id.ToString());
		}

		public IEnumerable<InterviewAnswer> GetInterviewAnswers(string botId)
		{
			return _interviewAnswers.Find(x => x.BotId == botId).ToList();
		}

		public bool RemoveInterviewAnswer(string id)
		{
			var deleteResult = _interviewAnswers.DeleteOne(x => x.Id == MongoService.TryCreateObjectId(id));

			return deleteResult.DeletedCount > 0;
		}

		public InterviewAnswer GetInterviewAnswer(string id)
		{
			return _interviewAnswers.Find(x => x.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
		}
	}
}