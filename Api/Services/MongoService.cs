using MongoDB.Bson;

namespace Api.Services
{
	public class MongoService
	{
		public static ObjectId? TryCreateObjectId(string id)
		{
			try
			{
				return new ObjectId(id);
			}
			catch
			{
				return null;
			}
		}
	}
}