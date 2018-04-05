using System.Collections.Generic;
using Api.Models;
using Api.Services;
using MongoDB.Driver;

namespace Api.Repositories
{
	public class InterviewsRepository
	{
		private readonly IMongoCollection<Interview> _interviews;

		public InterviewsRepository(IMongoDatabase database)
		{
			_interviews = database.GetCollection<Interview>("interviews");
		}

		public Interview AddInterview(Interview interview)
		{
			_interviews.InsertOne(interview);

			return GetInterview(interview.Id.ToString());
		}

		public IEnumerable<Interview> GetInterviews(string botId)
		{
			return _interviews.Find(x => x.BotId == botId).ToList();
		}

		public Interview GetInterview(string id)
		{
			return _interviews.Find(x => x.Id == MongoService.TryCreateObjectId(id)).FirstOrDefault();
		}

		public bool RemoveInterview(string id)
		{
			var deleteResult = _interviews.DeleteOne(x => x.Id == MongoService.TryCreateObjectId(id));

			return deleteResult.DeletedCount > 0;
		}

	}
}