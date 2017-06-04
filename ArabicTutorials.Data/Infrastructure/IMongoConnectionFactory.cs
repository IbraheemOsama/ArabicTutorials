using MongoDB.Driver;

namespace ArabicTutorials.Data.Infrastructure
{
    public interface IMongoConnectionFactory
    {
        MongoClient GetClient();
    }
}